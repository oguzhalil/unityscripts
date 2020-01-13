using UnityEngine;
using System;

namespace UtilityScript
{
    public class OfflineTimer
    {
        public string key;
        public long seconds;
        public long ticks;

        public bool TimeUp
        {
            get
            {
                TimeSpan timeSpan = new TimeSpan( DateTime.UtcNow.Ticks - ticks );
                return timeSpan.TotalSeconds >= seconds;
            }
        }

        public long LeftSeconds
        {
            get
            {
                return ( long ) ( new TimeSpan( DateTime.UtcNow.Ticks - ticks ).TotalSeconds );
            }
        }

        public OfflineTimer ( string key , long seconds )
        {
            this.key = key;
            this.seconds = seconds;
            string oldTicks = string.Empty;
            Storage.Read( key , ref oldTicks , true );
            ticks = long.Parse( oldTicks );

            if ( ticks == 0 )
            {
                ticks = DateTime.UtcNow.Ticks;
                Set();
            }
        }

        public void Set ()
        {
            Storage.Write( key , DateTime.UtcNow.Ticks );
            PlayerPrefs.Save();
        }
    }
}
