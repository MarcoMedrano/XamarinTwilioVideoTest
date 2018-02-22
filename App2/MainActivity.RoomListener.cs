using Android.Widget;
using Com.Twilio.Video;
using System;

namespace App2
{
    public partial class MainActivity
    {
        private class RoomListener : Java.Lang.Object, Room.IListener
        {
            private MainActivity mainActivity;
            public RoomListener()
            {

            }

            public RoomListener(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            public void Dispose()
            {
                Console.WriteLine("RoomListener.Dispose");
            }

            public void OnConnected(Room room)
            {
                Console.WriteLine("RoomListener.OnConnected");
                //var participant = room.Participants[0];
            }

            public void OnConnectFailure(Room p0, TwilioException p1)
            {
                Console.WriteLine("RoomListener.OnConnectFailure");
            }

            public void OnDisconnected(Room p0, TwilioException p1)
            {
                Console.WriteLine("RoomListener.OnDisconnected");
            }

            public void OnParticipantConnected(Room room, Participant participant)
            {
                Console.WriteLine("RoomListener.OnParticipantConnected");
                var remoteParticipantIdentity = participant.Identity;
                Toast.MakeText(this.mainActivity, "RemoteParticipant " + remoteParticipantIdentity + " joined", ToastLength.Short).Show();

                participant.SetListener(new ParticipantListener(this.mainActivity));
            }

            public void OnParticipantDisconnected(Room p0, Participant p1)
            {
                Console.WriteLine("RoomListener.OnParticipantDisconnected");
            }

            public void OnRecordingStarted(Room p0)
            {
                Console.WriteLine("RoomListener.OnRecordingStarted");
            }

            public void OnRecordingStopped(Room p0)
            {
                Console.WriteLine("RoomListener.OnRecordingStopped");
            }
        }

    }
}

