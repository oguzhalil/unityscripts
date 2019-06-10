//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using System;
//using EboxGames;

//namespace EboxGames
//{
//    public class Launcher : MonoBehaviour
//    {
//        public Text loadingLabel;
//        public Transform loadingIcon;
//        public bool rotate;

//        public int sceneIndex;
//        private AsyncOperation asyncOperation;
//        public bool allowSceneActivation;
//        public bool tutorialPlayed;
//        public bool passTutorial;
//        public ExposeElement loadingIndicator;
//        public bool Test;
//        public const string tutorialKey = "#tut";
//        public string strLoading;

//        public LoginPrompt loginPrompt;

//        public enum EligibleForPlay
//        {
//            None = 0,
//            PlayerInfo = 2,
//            Catalog = 4,
//            TitleData = 8,
//        }

//        int eligibledForPlay = 0;

//        [ContextMenu( "List" )]
//        public void ListObject ()
//        {
//            var all = FindObjectsOfTypeAll( typeof( GameObject ) );

//            foreach ( GameObject a in all )
//            {
//                if ( a.transform.parent == null )
//                {
//                    print( a.name );
//                }
//            }
//        }

//        public enum Flag
//        {
//            None = 0, // 
//            One = 1, // 2_0  | 0000 0001
//            Two = 2, // 2_1 | 0000 0010
//            Four = 4, // 2_2 | 0000 0100
//            Eight = 8, // 2_3 | 0000 1000
//            Sixteen = 16, // 2_4 | 0001 0000
//            ThirtyTwo = 32, // 2_5 | 0010 000
//            SixtyFour = 64, // 2_6 | 0100 0000
//            OneTwentyEight = 128, // 2_7 | 1000 0000
//        }

//        enum Dependencies
//        {
//            Scene = 1,
//            Authentication = 2,
//            Catalog = 4,
//            TitleData = 8
//        }

//        int dependencies = 0;

//        void Awake ()
//        {
//            if ( passTutorial )
//                PlayerPrefs.SetInt( tutorialKey , 1 );

//            tutorialPlayed = System.Convert.ToBoolean( PlayerPrefs.GetInt( tutorialKey , 0 ) );

//            Application.targetFrameRate = 60;
//            QualitySettings.vSyncCount = 1;

//            if ( !tutorialPlayed && !Test )
//            {
//                SceneManager.LoadScene( 2 );
//            }


//            /*
//             * 
//             * https://www.tutorialspoint.com/csharp/csharp_bitwise_operators.htm
//            byte a = 60;
//            byte b = 12;

//            int c = a | b;
//            Debug.Log(c + " " + (a ^ b) + " " +(~b));

//            Flag container = (Flag.One | Flag.Two);

//            if( (container & Flag.One) != 0 );
//            {
//                print("Container contains " + Flag.One);
//            }

//             */

//            /* Func<int, bool> func = integer => { return true; }

//             bool result = func(0);

//             print(result);*/


//        }

//        bool FromInt ( int a )
//        {
//            return System.Convert.ToBoolean( a );
//        }

//        IEnumerator Start ()
//        {
//            //loadingIndicator = UIBox.GetExposedElement( "loadingIcon" );
//            yield return new WaitForEndOfFrame();

//            strLoading = Localization.Instance.GetText( "loading" ).ToUpper();

//            // load scene
//            asyncOperation = SceneManager.LoadSceneAsync( sceneIndex );
//            asyncOperation.allowSceneActivation = false;

//            // authenticate
//            AuthParams authParams = null;

//            bool hasLogin = Storage.Read<AuthParams>( Authentication.keyStoredLogin , ref authParams , true );

//            if ( Test )
//            {
//                hasLogin = true;
//            }

//            if ( hasLogin )
//            {
//                if ( Test )
//                    Database.authentication.RunForDesktop( AuthResult );
//                else
//                    Database.authentication.Run( AuthResult , authParams );
//            }
//            else
//            {
//                // play tutorial
//                // than authenticate
//                //Action stopLoading = Process.Loading();

//                loginPrompt.Activate( ( type =>
//                {
//                    Database.authentication.Run( ( b , e ) => { AuthResult( b , e ); /*stopLoading();*/ } , new AuthParams() { iType = ( int ) type } );
//                } ) );
//            }

//            yield return new WaitUntil( () => ( asyncOperation.progress >= .9f || asyncOperation.isDone ) );

//            dependencies += ( int ) Dependencies.Scene;

//            yield return new WaitUntil( () => dependencies == (
//            ( int ) Dependencies.Authentication |
//            ( int ) Dependencies.Scene |
//            ( int ) Dependencies.Catalog |
//            ( int ) Dependencies.TitleData ) );

//            asyncOperation.allowSceneActivation = allowSceneActivation;
//        }

//        void AuthResult ( bool authenticated , DBError error )
//        {
//            if ( authenticated )
//            {
//                dependencies += ( int ) Dependencies.Authentication;

//                PlayFabClientAPI.GetCatalogItems( new PlayFab.ClientModels.GetCatalogItemsRequest() { CatalogVersion = "01" } ,
//                    result =>
//                    {
//                        Database.SetCatalogItems( result );
//                        dependencies += ( int ) Dependencies.Catalog;
//                    } ,
//                    catalogError =>
//                    {
//                        Debug.LogError( GetType().Name + ".cs " + catalogError.GenerateErrorReport() + " method AuthResult " );
//                    } );

//                Database.gameData.Run( ( value ) =>
//                {
//                    if ( value )
//                    {
//                        dependencies += ( int ) Dependencies.TitleData;
//                    }
//                    else
//                    {
//                        Debug.LogError( GetType().Name + " title data could not be fetched." );
//                    }
//                } );
//            }
//            else
//            {
//                loginPrompt.Activate( ( type =>
//                {
//                    Database.authentication.Run( ( b , e ) => { AuthResult( b , e ); /*stopLoading();*/ } , new AuthParams() { iType = ( int ) type } );
//                } ) );
//            }


//            //if ( authenticated )
//            //{
//            //    Database.localPlayerInfo.Run( ( success ) =>
//            //     {
//            //         if ( success )
//            //         {
//            //             OnProcessCompleted( EligibleForPlay.PlayerInfo );
//            //         }
//            //         else
//            //         {

//            //         }
//            //     } );

//            //    PlayFabClientAPI.GetCatalogItems( new PlayFab.ClientModels.GetCatalogItemsRequest()
//            //    {
//            //        CatalogVersion = "01"
//            //    } ,
//            //    res =>
//            //    {
//            //        OnProcessCompleted( EligibleForPlay.Catalog );

//            //    } ,
//            //    ee =>
//            //    {
//            //        Debug.LogError( GetType().Name + ".cs get store items ended up with error " + ee.GenerateErrorReport() );
//            //    }
//            //    );

//            //    Database.gameData.Run( ( success ) =>
//            //     {
//            //         if ( success )
//            //         {

//            //             OnProcessCompleted( EligibleForPlay.TitleData );
//            //         }
//            //         else
//            //         {

//            //         }
//            //     } );

//            //PlayFabClientAPI.GetTitleData(new PlayFab.ClientModels.GetTitleDataRequest()
//            //{
//            //    Keys = null // leaves null to get all keys
//            //},
//            //res =>
//            //{

//            //    Dictionary<string, string> pairs = res.Data;

//            //    string s = pairs["attack_defend"];

//            //    var o = JsonConvert.DeserializeObject<Dictionary<string, RoomProperties>>(s);

//            //    print(o["beginner"].ToString());

//            //    foreach (var pair in pairs)
//            //    {
//            //        Debug.LogFormat("key : {0} value : {1}", pair.Key, pair.Value);
//            //    }


//            //    OnProcessCompleted(EligibleForPlay.TitleData);

//            //},
//            //error =>
//            //{
//            //    Debug.LogError(GetType().Name + ".cs get store items ended up with error " + error.GenerateErrorReport());
//            //}
//            //);

//            //}
//            //else
//            //{
//            //    StartCoroutine( Start() );
//            //}
//        }

//        void OnProcessCompleted ( EligibleForPlay enumParam )
//        {
//            eligibledForPlay += ( int ) enumParam;

//            //print(eligibledForPlay);

//            if ( eligibledForPlay == (
//                ( int ) EligibleForPlay.Catalog |
//                ( int ) EligibleForPlay.PlayerInfo |
//                ( int ) EligibleForPlay.TitleData ) )
//            {
//                //print("Total" + eligibledForPlay);
//                //SceneManager.LoadScene(2, LoadSceneMode.Additive);

//                asyncOperation.allowSceneActivation = true;
//            }
//        }

//        int dots = 0;
//        int maxDots = 4;

//        float dotInterval = .2f;
//        float dotTimer;

//        void Update ()
//        {
//            if ( rotate )
//                loadingIndicator.transform.Rotate( -Vector3.forward * 90f * Time.deltaTime );


//            if ( Time.time > dotTimer )
//            {
//                loadingLabel.text = strLoading + new string( '.' , dots );

//                dots++;

//                if ( dots > maxDots )
//                    dots = 0;

//                dotTimer = Time.time + dotInterval;
//            }
//        }






//    }
//}
