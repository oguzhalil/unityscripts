using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityScripts
{
    public class EventManager<T>
    {
        public Dictionary<T , Action<BaseEvent>> pairs = new Dictionary<T , Action<BaseEvent>>();
        private List<T> locked = new List<T>();
        //public static readonly LinkedList<EventHandler<BaseEvent>> linkedList = new LinkedList<EventHandler<BaseEvent>>();

        //static event EventHandler<BaseEvent> OnMatched;

        public EventManager ()
        {
            Array elements = Enum.GetValues( typeof( T ) );

            foreach ( var element in elements )
            {
                pairs.Add( ( T ) element , delegate { } );
            }
        }

        public void UnLock( T e )
        {
            locked.Remove( e );
        }

        public void Raise ( T e , bool bLock = false , BaseEvent a = null )
        {
            if ( bLock )
            {
                if ( locked.Contains( e ) ) // if event raise return, event must be resetted
                    return;

                locked.Add( e );
            }

            pairs [ e ].Invoke( a );
        }

        public void Register ( T e , Action<BaseEvent> handler )
        {
            pairs [ e ] += handler;
        }

        public void Unregister ( T e , Action<BaseEvent> handler )
        {
            pairs [ e ] -= handler;
        }

        public void Clear ()
        {
            Array elements = Enum.GetValues( typeof( T ) );

            foreach ( var element in elements )
            {
                T _event = ( T ) element;
                pairs [ _event ] = delegate { };
                continue;
                //EventHandler<BaseEvent> value = pairs[_event];

                //foreach (var del in pairs[_event].GetInvocationList())
                //{
                //    value -= nul;
                //    Debug.Log("Removed " + del.Method.Name);
                //}

                //foreach (var del in pairs[)
                //{
                //    value -= (EventHandler<BaseEvent>)del;
                //    Debug.Log("Removed " + del.Method.Name);
                //}
            }

            //foreach (var pair in pairs)
            //{
            //    //Debug.Log("Removed " + pair.Key);

            //    EventHandler<BaseEvent> value = pair.Value;

            //    foreach (var del in value.GetInvocationList())
            //    {
            //        value -= (EventHandler<BaseEvent>)del;
            //        Debug.Log("Removed " + del.Method.Name);
            //    }
            //}

        }

        //public static void Link(Gameplay_Events first, Gameplay_Events second)
        //{
        //    linkedList.AddFirst(events[first]);
        //    linkedList.AddLast(events[second]);
        //}

        /*
         Events
         Event_OnMatched : called when users are ready to play.
            -> Set Room Timer (if shooting_race)
            -> Instantiate the players
                  Event_OnInstantion : called when players are instantiated
                     -> Assign ui elements associated with player
                     -> Set players positions depending on mode
                     -> Assign player controller to their owners
            -> Start the whistle countdown (3 sec. as default)
        Event_OnWhistle : called when whistle countdown is over
            -> Allow local user to give input
            -> Start the round countdown if( shooting_race )
        Event_OnScoreShooting_Race(perfect) : called when local user is scored
            -> Send rpc to other client

        Event_OnScoreAttack_Defender(perfect,scoredPlayer) : called when users is scored onMasterClient
            -> Send rpc to other client 
            -> Check score if score exceeds win score invoke Event_OnWin(scoredPlayer)

        Event_OnRoundCountdownExceeds : called when round timer is zero
            -> Don't allow local user to give input.

        Event_OnBallTouchesGround : called when ball touches ground
            -> for shooting_race if round time <= 0 
            -> Send rpc to other client PostScore(score) ( on Local if(remoteScore > localScore lose vice versa -- == draw )

        Event_OnWin(player) : called when a user is won

         2-) OnBeginWhistle : begin countdown  is completed start the game
         4-) OnScore : 

        */

        //public static void Register(Gameplay_Events _event, EventHandler<BaseEvent> action)
        //{
        //    events.Add(_event, action);
        //}

        //public static void Add(Gameplay_Events _event, EventHandler<BaseEvent> action)
        //{
        //    events[_event] += action;
        //}

        //public static void Remove(Gameplay_Events _event, EventHandler<BaseEvent> action)
        //{
        //    events[_event] -= action;
        //}
    }

    public class BaseEvent
    {
    }

    public class ParkTimerUpd : BaseEvent
    {
        public float time;
    }
}