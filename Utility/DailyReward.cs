using UnityEngine;
using System;

namespace EboxGames
{
    public class DailyReward
    {
        const string time = "#_dr";

        public long? ticks;

        public RewardType type;

        public void Init ()
        {
            long lastTime = 0;

            if ( Storage.Read( time , ref lastTime , true ) )
            {
                //DateTime dateTime = new DateTime( lastTime );
                //DateTime.UtcNow.Ticks - lastTime
                TimeSpan timeSpan =  new TimeSpan( DateTime.UtcNow.Ticks - lastTime );

                if ( timeSpan.TotalHours >= 24 )
                {
                    if ( type == RewardType.DayByDy )
                    {
                        if ( timeSpan.TotalHours <= 48 )
                        {
                            // he didnt break to cycle
                        }
                    }
                    else if ( type == RewardType.Fixed )
                    {

                    }

                    // its reward time!
                }
            }
            else
            {
                // first time
            }
        }

        public void Check () // Is There Reward
        {

        }

        public void Get () // Get Reward Values
        {

        }

        public void Save ()
        {
            Storage.Write( time , DateTime.UtcNow.Ticks , true );
        }
    }

    public enum RewardType
    {
        DayByDy = 0,
        Fixed = 1
    }
}
