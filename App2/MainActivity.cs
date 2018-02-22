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
    public partial class MainActivity : Activity
    {
        private const string LOCAL_AUDIO_TRACK_NAME = "mic";
        private const string LOCAL_VIDEO_TRACK_NAME = "camera";

        private int count = 0;
        private LocalAudioTrack localAudioTrack;
        private LocalVideoTrack localVideoTrack;
        private readonly RoomListener roomListener;

        private Com.Twilio.Video.VideoView primaryVideoView;

        public MainActivity()
        {
            this.roomListener = new RoomListener(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("OnCreate");
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            this.primaryVideoView = FindViewById<Com.Twilio.Video.VideoView>(Resource.Id.primary_video_view);
            var editTextRoomName = FindViewById<EditText>(Resource.Id.editText1);
            var buttonGetToken = FindViewById<Button>(Resource.Id.button2);

            buttonGetToken.Click += async (s, e) => {
                var json = await GetToken("https://sosmarco.astutesolutions.org/Token");
                Console.WriteLine("Token " + json["token"]);
                this.ConnectToRoom(editTextRoomName.Text.Trim(), json["token"]);
            };

        }

        private async Task<JsonValue> GetToken(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";

                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                        return jsonDoc;
                    }
                }
            }catch (Exception e)
            {
                Console.WriteLine("Error requestiontoken " + e);
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                return null;
            }
        }

        public Room ConnectToRoom(string roomName, string accessToken)
        {
            try
            {
                Console.WriteLine("Trying to connect to room " + roomName);
                localAudioTrack = LocalAudioTrack.Create(this, true);
                CameraCapturer cameraCapturer = new CameraCapturer(this, CameraCapturer.CameraSource.FrontCamera);

                // Create a video track
                LocalVideoTrack localVideoTrack = LocalVideoTrack.Create(this, true, cameraCapturer);

                //primaryVideoView.SetMirror(true);
                //localVideoTrack.AddRenderer(primaryVideoView);

                ConnectOptions connectOptions = new ConnectOptions.Builder(accessToken)
                  .RoomName(roomName)
                  .AudioTracks(new List<LocalAudioTrack> { localAudioTrack })
                  .VideoTracks(new List<LocalVideoTrack> { localVideoTrack })
                  .Build();

                return Video.Connect(this, connectOptions, this.roomListener);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: trying to connect to room " + e.Message );
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                return null;
            }
        }
    }
}

