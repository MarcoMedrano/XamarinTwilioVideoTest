using Android.App;
using Android.Widget;
using Android.OS;
using Com.Twilio.Video;
using System.Collections.Generic;
using System;

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
            button.Click += (s, e) => { button.Text = $"{++count} clicks!"; };
        }

        public Room ConnectToRoom(string roomName)
        {
            var audioOptions = new AudioOptions.Builder().Build();
            localAudioTrack = LocalAudioTrack.Create(this, true, audioOptions);

            var accessToken = string.Empty;
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
                throw new NotImplementedException();
            }

            public void OnConnected(Room p0)
            {
                throw new NotImplementedException();
            }

            public void OnConnectFailure(Room p0, TwilioException p1)
            {
                throw new NotImplementedException();
            }

            public void OnDisconnected(Room p0, TwilioException p1)
            {
                throw new NotImplementedException();
            }

            public void OnParticipantConnected(Room p0, Participant p1)
            {
                throw new NotImplementedException();
            }

            public void OnParticipantDisconnected(Room p0, Participant p1)
            {
                throw new NotImplementedException();
            }

            public void OnRecordingStarted(Room p0)
            {
                throw new NotImplementedException();
            }

            public void OnRecordingStopped(Room p0)
            {
                throw new NotImplementedException();
            }
        }

    }
}

