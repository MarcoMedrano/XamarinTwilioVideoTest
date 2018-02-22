using System;
using Com.Twilio.Video;

namespace App2
{
    public partial class MainActivity
    {
        private class ParticipantListener : Java.Lang.Object, Participant.IListener
        {
            private MainActivity mainActivity;
            public ParticipantListener()
            {

            }
            public ParticipantListener(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            public void Dispose()
            {
                Console.WriteLine("ParticipantListener.Dispose");
            }

            public void OnAudioTrackAdded(Participant p0, AudioTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnAudioTrackAdded");
            }

            public void OnAudioTrackDisabled(Participant p0, AudioTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnAudioTrackDisabled");
            }

            public void OnAudioTrackEnabled(Participant p0, AudioTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnAudioTrackEnabled");
            }

            public void OnAudioTrackRemoved(Participant p0, AudioTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnAudioTrackRemoved");
            }

            public void OnVideoTrackAdded(Participant participant, VideoTrack videoTrack)
            {
                try
                {
                    Console.WriteLine("ParticipantListener.OnVideoTrackAdded");
                    this.mainActivity.primaryVideoView.SetMirror(true);
                    videoTrack.AddRenderer(this.mainActivity.primaryVideoView);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ParticipantListener.OnVideoTrackAdded " + e.Message);
                }
            }

            public void OnVideoTrackDisabled(Participant p0, VideoTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnVideoTrackDisabled");
            }

            public void OnVideoTrackEnabled(Participant p0, VideoTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnVideoTrackEnabled");
            }

            public void OnVideoTrackRemoved(Participant p0, VideoTrack p1)
            {
                Console.WriteLine("ParticipantListener.OnVideoTrackRemoved");
            }
        }
    }
}