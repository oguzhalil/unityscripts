using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GooglePlayGames.BasicApi;

//namespace PabloGames
//{
//    public class GameLauncher : MonoBehaviour
//    {
//        public int levelIndex;

//        public Text loading;

//        private AsyncOperation sceneOperation;

//        public Authentication authentication;

//        public PlayServices playServices;

//        public bool allowSceneActivation;

//        public Image progress;

//        enum Dependencies
//        {
//            Scene = 1,
//            Authentication = 2,
//            ServerTime = 4,
//        }

//        public int dependencies = 0;

//        void Initialize ()
//        {
//            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith( task =>
//            {
//                var dependencyStatus = task.Result;
//                if ( dependencyStatus == Firebase.DependencyStatus.Available )
//                {
//                    // Create and hold a reference to your FirebaseApp,
//                    // where app is a Firebase.FirebaseApp property of your application class.
//                    //   app = Firebase.FirebaseApp.DefaultInstance;

//                    // Set a flag here to indicate whether Firebase is ready to use by your app.
//                    Debug.Log( "Firebase.DependencyStatus.Available" );
//                }
//                else
//                {
//                    UnityEngine.Debug.LogError( System.String.Format(
//                      "Could not resolve all Firebase dependencies: {0}" , dependencyStatus ) );
//                    // Firebase Unity SDK is not safe to use here.
//                }
//            } );

//            dependencies = 0;
//            authentication = Bridge.authentication;
//            playServices = Bridge.gameServices;

//            sceneOperation = null;
//        }

//        IEnumerator Start ()
//        {
//            yield return new WaitForEndOfFrame();

//            Initialize();

//            yield return new WaitForEndOfFrame();

//            Bridge.SOCWeapons = Resources.LoadAll<SOCWeapon>( "weapons" );

//            sceneOperation = SceneManager.LoadSceneAsync( levelIndex );

//            sceneOperation.allowSceneActivation = false;

//            authentication.Run( AuthSuccess , AuthFail );

//            PlayFab.PlayFabServerAPI.GetTime( new PlayFab.ServerModels.GetTimeRequest() , SvTimeSuccess , SvTimeFailed );

//            yield return new WaitUntil( () => ( sceneOperation.progress >= .9f || sceneOperation.isDone ) );

//            dependencies += ( int ) Dependencies.Scene;

//            yield return new WaitUntil( () => dependencies == ( ( int ) Dependencies.Authentication | ( int ) Dependencies.Scene | ( int ) Dependencies.ServerTime ) );

//            sceneOperation.allowSceneActivation = allowSceneActivation;
//        }

//        void AuthSuccess ()
//        {
//            PlayFab.PlayFabClientAPI.GetCatalogItems(
//                new PlayFab.ClientModels.GetCatalogItemsRequest() { CatalogVersion = "1.0" } ,
//                ( result ) => { Bridge.catalogItems = result.Catalog; Bridge.UpdateSOCWeapon(); dependencies += ( int ) Dependencies.Authentication; } ,
//                ( r ) => AuthFail() );



//            Debug.Log( GetType().Name + ".cs " + " authentication is successful." );
//        }

//        void AuthFail ()
//        {
//            Report.FatalError();

//            Debug.Log( GetType().Name + ".cs " + " no internet connection" );
//        }

//        void SvTimeSuccess ( PlayFab.ServerModels.GetTimeResult result )
//        {
//            Bridge.serverTimestamp = result.Time.Ticks;

//            dependencies += ( int ) Dependencies.ServerTime;

//            Debug.Log( GetType().Name + ".cs " + " server time received." );
//        }

//        void SvTimeFailed ( PlayFab.PlayFabError error )
//        {
//            Bridge.serverTimestamp = null;

//            dependencies += ( int ) Dependencies.ServerTime;

//            Debug.LogError( "GetServerTime failed with an error " + error );
//        }


//        #region Dot Animation

//        int dots = 0;

//        float dotInterval = .2f;

//        int maxDots = 4;

//        float dotTimer;

//        float timer;
//        int searchTime;


//        void OnEnable ()
//        {
//            dotTimer = Time.time + dotInterval;
//        }

//        void Update ()
//        {
//            if ( Time.time > dotTimer )
//            {
//                loading.text = "Loading" + new string( '.' , dots );

//                dots++;

//                if ( dots > maxDots )
//                    dots = 0;

//                dotTimer = Time.time + dotInterval;
//            }

//            // Loading bar with dependencies progress.

//            int auth = ( int ) Dependencies.Authentication;

//            int svTime = (int) Dependencies.ServerTime;

//            if ( sceneOperation != null )
//                progress.fillAmount = ( sceneOperation.progress / 0.9f ) * .6f + ( ( dependencies & auth ) / auth ) * .2f + ( ( dependencies & svTime ) / svTime ) * .2f;

//        }

//        #endregion

//    }
//}
