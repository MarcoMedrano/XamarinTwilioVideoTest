using Android.App;
using Android.Widget;
using Android.OS;
using Com.Twilio.Video;
using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Json;

namespace App2
{
    [Activity(Label = "App2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private const string LOCAL_AUDIO_TRACK_NAME = "mic";
        private const string LOCAL_VIDEO_TRACK_NAME = "camera";

        private int count = 0;
        private LocalAudioTrack localAudioTrack;
        private LocalVideoTrack localVideoTrack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var button = FindViewById<Button>(Resource.Id.button1);
            button.Text = RoomState.Connected.Name();
            button.Click += (s, e) => {
                button.Text = $"{++count} clicks!";
                Console.WriteLine("CAT " + count);
            };

            var buttonGetToken = FindViewById<Button>(Resource.Id.button2);
            buttonGetToken.Click += async (s, e) => {
                var json = await FetchWeatherAsync("https://sosmarco.astutesolutions.org/Token");
                Console.WriteLine("Token " + json["token"]);
                this.ConnectToRoom("a", json["token"]);
            };

        }

        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";

                // Send the request to the server and wait for the response:
                using (WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                        // Return the JSON document:
                        return jsonDoc;
                    }
                }
            }catch (Exception e)
            {
                Console.WriteLine("Error requestiontoken " + e);
                return null;
            }
        }

        public Room ConnectToRoom(string roomName, string accessToken)
        {
            var audioOptions = new AudioOptions.Builder().Build();
            localAudioTrack = LocalAudioTrack.Create(this, true, audioOptions);


            ConnectOptions connectOptions = new ConnectOptions.Builder(accessToken)
              .RoomName(roomName)
              .AudioTracks(new List<LocalAudioTrack>{ localAudioTrack })
              //.VideoTracks(localVideoTracks)
              .Build();

            return Video.Connect(this, connectOptions, new RoomListener(this));
        }

        private class RoomListener : Java.Lang.Object, Room.IListener
        {
            private MainActivity mainActivity;

            public RoomListener(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            public IntPtr Handle => throw new NotImplementedException();

            public void Dispose()
            {
                Console.WriteLine("Dispose");
            }

            public void OnConnected(Room p0)
            {
                Console.WriteLine("OnConnected");
            }

            public void OnConnectFailure(Room p0, TwilioException p1)
            {
                Console.WriteLine("OnConnectFailure");
            }

            public void OnDisconnected(Room p0, TwilioException p1)
            {
                Console.WriteLine("OnDisconnected");
            }

            public void OnParticipantConnected(Room p0, Participant p1)
            {
                Console.WriteLine("OnParticipantConnected");
            }

            public void OnParticipantDisconnected(Room p0, Participant p1)
            {
                Console.WriteLine("OnParticipantDisconnected");
            }

            public void OnRecordingStarted(Room p0)
            {
                Console.WriteLine("OnRecordingStarted");
            }

            public void OnRecordingStopped(Room p0)
            {
                Console.WriteLine("OnRecordingStopped");
            }
        }

    }
}

