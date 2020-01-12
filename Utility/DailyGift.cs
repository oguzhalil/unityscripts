//using PlayFab;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;

//namespace PabloGames
//{
//    public class DailyGift : MonoBehaviour
//    {
//        public Button btnDefault;

//        const string key = "#daily";

//        public double Hour => Bridge.hour;

//        public Text labelTime;

//        public PabloGames.LocalNotification.LocalNotification localNotification = new PabloGames.LocalNotification.LocalNotification();

//        public bool test;

//        private void Start ()
//        {
//            btnDefault.onClick.AddListener( BtnDailyGift );

//            if(test)
//            {
//                Bridge.authentication.Run( Test , null );
//            }
//            else
//                Run();

//            //Run(); // executes main logic
//        }

//        void Test()
//        {
//            PlayerPrefs.DeleteAll();

//            PlayFabServerAPI.GetTime( new PlayFab.ServerModels.GetTimeRequest() , ( res ) =>
//            {
//                Debug.Log( "Get Server Time UTC " + res.Time );
//                Debug.Log( "Curr" + DateTime.UtcNow );
//                long ticks = res.Time.Ticks;

//                TimeSpan t = TimeSpan.FromTicks( System.DateTime.UtcNow.Ticks - ticks);

//                Debug.Log( "Elapsed secs" + t.TotalSeconds );

//                Bridge.serverTimestamp = ticks;

//                Run();
//            }
//        , ( e ) => { Debug.LogError( "Get Servertime eneded up with er" + e ); } );
//        }

//        void Run ()
//        {
//            if ( Ready )
//            {
//                // Show Claim Button

//                labelTime.text = "Ready!";

//                btnDefault.interactable = true;

//                //long ticks = DateTime.Now.Ticks;

//                //long nextTicks = TimeSpan.FromHours( hour ).Ticks;

//                //PlayerPrefs.SetString( key , ticks.ToString() );

//                //DateTime scheduled = new DateTime( ticks + nextTicks );

//                //localNotification.Notify( "Beat The Puppet" , "Your daily gift is ready! \n Get Your Free Coins." , "" , scheduled );

//                //Debug.Log( " Daily Gift is scheduled after  " + scheduled );

//                //gameManage_.updateCoin( 250 );

//            }
//            else
//            {
//                StartCoroutine( CountDowner( Seconds ) );
//                btnDefault.interactable = false;
//            }
//        }

//        void ReportWrapper()
//        {
//            // called only if there is a gift...

//            long ticks = Bridge.serverTimestamp.Value;

//            long nextTicks = TimeSpan.FromHours( Hour ).Ticks;

//            PlayerPrefs.SetString( key , ticks.ToString() );

//            DateTime scheduled = DateTime.Now.AddTicks( nextTicks );

//            localNotification.Notify( "Counter Terrorist Critical: Strike War" , "Your daily gift is ready! \n Get Your Free Coins." , "" , scheduled );

//            Debug.Log( " Daily Gift is scheduled after  " + scheduled );

//            PlayFabClientAPI.PurchaseItem( new PlayFab.ClientModels.PurchaseItemRequest() { CatalogVersion = "1.0" , ItemId = "vc_dg" , VirtualCurrency = "VC" } ,
//           ( result ) =>
//           {
//               float value = uint.Parse(result.Items.Where( x => x.ItemId == "vc_dg" ).First().ItemClass);
//               print( value );
//               PGMenuFeatures.Instance.AddCoin( value );
//           } ,
//           ( error ) => { Debug.LogError( "Daily Gift  encountered with an error." ); } );

//            //gameManage_.updateCoin( 250 );

//            Run();
//        }

//        void BtnDailyGift (  )
//        {
//            Report.Run( "Get Free Coin" , "Get Your Free Coin Everyday" , "CLAIM" , Report.IconSprite.COIN , ReportWrapper );
//        }

//        IEnumerator CountDowner ( int remainingSeconds )
//        {
//            remainingSeconds += ( int ) Time.time;

//            while ( true )
//            {
//                // 1000

//                yield return null;

//                int seconds = remainingSeconds - (int)Time.time;

//                if ( seconds <= 0 )
//                {
//                    Bridge.serverTimestamp = null;

//                    // TODO should be revisited
//                    PlayFab.PlayFabServerAPI.GetTime( new PlayFab.ServerModels.GetTimeRequest() ,
//                        (r)=> { Bridge.serverTimestamp = r.Time.Ticks; Run(); } ,
//                        (err)=> { Debug.LogError( err.GenerateErrorReport() ); } );
//                    break;
//                }
//                else
//                    labelTime.text = ( seconds / 3600 ).ToString( "D2" ) + ":" + ( ( seconds / 60 ) % 60 ).ToString( "D2" ) + ":" + ( seconds % 60 ).ToString( "D2" );
//            }
//        }

//        public bool Ready
//        {
//            get
//            {
//                if ( Bridge.serverTimestamp == null )
//                    return false;

//                if ( !PlayerPrefs.HasKey( key ) )
//                    return true;

//                long ticks = long.Parse( PlayerPrefs.GetString( key ));

//                long now = Bridge.serverTimestamp.Value;

//                long passedTime = now - ticks;

//                if ( passedTime <= 0 )
//                    return false;

//                TimeSpan timeSpan = TimeSpan.FromTicks( passedTime );

//                if ( timeSpan.TotalHours >= Hour )
//                    return true;

//                return false;
//            }
//        }

//        public int Seconds
//        {
//            get
//            {
//                long now = Bridge.serverTimestamp == null ?  0 : Bridge.serverTimestamp.Value ;

//                if ( Bridge.serverTimestamp == null )
//                    now = DateTime.UtcNow.AddHours( Hour ).Ticks;

//                long ticks = long.Parse( PlayerPrefs.GetString( key  , DateTime.UtcNow.AddHours( Hour ).Ticks.ToString()));

//                long passedTime = now - ticks;

//                long remainingTicks  = TimeSpan.FromHours( Hour ).Ticks - passedTime;

//                TimeSpan timeSpan = TimeSpan.FromTicks( remainingTicks );

//                return ( int ) timeSpan.TotalSeconds;
//            }
//        }
//    }
//}
