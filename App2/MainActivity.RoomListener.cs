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

