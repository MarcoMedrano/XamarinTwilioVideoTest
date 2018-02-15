using Android.App;
using Android.Widget;
using Android.OS;
using Com.Twilio.Video;


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

        //public void ConnectToRoom(string roomName)
        //{
        //    localAudioTrack = LocalAudioTrack.Create(this, true, LOCAL_AUDIO_TRACK_NAME);

        //    var accessToken = string.Empty;
        //    ConnectOptions connectOptions = new ConnectOptions.Builder(accessToken)
        //      .RoomName(roomName)
        //      .AudioTracks(localAudioTracks)
        //      .VideoTracks(localVideoTracks)
        //      .Build();
        //    var room = Video.Connect(context, connectOptions, this);
        //}

    }
}

