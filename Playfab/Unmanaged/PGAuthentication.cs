//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PlayFab;
//using PlayFab.ClientModels;
//using System;
//using EboxGames;

//namespace PabloGames
//{
//    public class Authentication
//    {
//        private Action successCallback;
//        private Action failureCallback;

//        private PlayServices playServices;

//        public string googleId;

//        const string prefKey = "#ap";

//        public LoginResult loginResult;

//        public Authentication ()
//        {
//            successCallback = null;
//            playServices = Bridge.gameServices;
//            googleId = string.Empty;
//        }

//        ~Authentication ()
//        {
//            successCallback = null;
//            failureCallback = null;
//            playServices = null;
//            googleId = null;
//            loginResult = null;
//        }

//        public void LinkGoogle ()
//        {
//            playServices.SignIn( ( authCode ) =>
//            {
//                PlayFabClientAPI.LinkGoogleAccount(
//                    new LinkGoogleAccountRequest() { ServerAuthCode = authCode , ForceLink = true } ,
//                    ( link ) => { Storage.Write( prefKey , new AuthParams( AuthParams.Type.Google ) ); } ,
//                    ( error ) => { } );

//            } , null , false );
//        }

//        public void Run ( Action successCallback , Action failureCallback )
//        {
//            this.successCallback = successCallback;
//            this.failureCallback = failureCallback;

//            bool storedLogin = PlayerPrefs.HasKey( prefKey );

//            Debug.Log( GetType().Name + ".cs " + " stored login : " + storedLogin );
//            if ( storedLogin )
//            {
//                AuthParams authParams = null;

//                if ( Storage.Read( prefKey , ref authParams , true ) )
//                {
//                    AuthParams.Type accountType = authParams.Info;

//                    if ( accountType == AuthParams.Type.Google )
//                    {
//                        playServices.SignIn(
//                            ( authCode ) => { PlayFabClientAPI.LoginWithGoogleAccount( GetGoogleRequest( authCode ) , OnSuccess , OnFailed ); } ,
//                            () => { OnFailed( new PlayFabError() { ErrorMessage = "Authentication Account Type Google : GPGS Error" } ); } , false );
//                    }
//                    else if ( accountType == AuthParams.Type.Anonymous )
//                    {
//                        PlayFabClientAPI.LoginWithCustomID( GetRequest() , ( result ) =>
//                        {
//                            playServices.SignIn(

//                                ( authCode ) =>
//                                {
//                                    PlayFabClientAPI.LinkGoogleAccount( new LinkGoogleAccountRequest() { ServerAuthCode = authCode , ForceLink = true } ,
//                                        ( linkResult ) => { Storage.Write( prefKey , new AuthParams( AuthParams.Type.Google ) ); OnSuccess( result ); } , OnFailed );
//                                } ,

//                            () => { OnSuccess( result ); } , false );
//                        }
//                        , OnFailed );

//                        //PlayFabClientAPI.LoginWithCustomID( GetRequest() , OnSuccess , OnFailed );
//                        //playServices.ReSignIn(
//                        //( authCode ) => { PlayFabClientAPI.LinkGoogleAccount( new LinkGoogleAccountRequest() { ServerAuthCode = authCode , ForceLink = true } , ( linkResult ) => { OnSuccess( result ); } , OnFailed ); } ,
//                        //() => { OnSuccess( result ); } , false );
//                        //, OnFailed );


//                    }
//                    else
//                    {
//                        Debug.LogError( "Stored Login Type is wrong af" );
//                        // Handle The Error
//                    }
//                }
//                else
//                    storedLogin = false;
//            }

//            if ( !storedLogin )
//            {
//                playServices.SignIn(
//                           ( authCode ) => { PlayFabClientAPI.LoginWithGoogleAccount( GetGoogleRequest( authCode ) , OnSuccess , OnFailed ); Storage.Write( prefKey , new AuthParams( AuthParams.Type.Google ) , true ); } ,
//                           () => { PlayFabClientAPI.LoginWithCustomID( GetRequest() , OnSuccess , OnFailed ); Storage.Write( prefKey , new AuthParams( AuthParams.Type.Anonymous ) , true ); } , false );

//                //GoogleAuth();

//                //stıre loggin
//            }
//        }

//        void Google ( string authCode )
//        {

//        }

//        void Anonymous ()
//        {

//        }

//        public void LinkWithGoogleId ( string id )
//        {

//        }

//        public void RunWithCustomId ( Action successCallback )
//        {
//            this.successCallback = successCallback;

//            PlayFabClientAPI.LoginWithCustomID( GetRequest() , OnSuccess , OnFailed );
//        }

//        void UpdateDisplayName ( string name )
//        {
//            loginResult.InfoResultPayload.PlayerProfile.DisplayName = name;

//            PlayFabClientAPI.UpdateUserTitleDisplayName(
//                new UpdateUserTitleDisplayNameRequest() { DisplayName = name } ,
//                ( r ) => { } ,
//                ( e ) => { } );
//        }

//        void OnSuccess ( LoginResult loginResult )
//        {
//            this.loginResult = loginResult;

//            //loginResult.InfoResultPayload.AccountInfo.GoogleInfo.
//            //loginResult.InfoResultPayload.AccountInfo.GoogleInfo.GoogleId

//            Bridge.fullbanner = false;

//            Bridge.hour = 6.0d;

//            if ( loginResult.InfoResultPayload.TitleData != null )
//            {
//                if ( loginResult.InfoResultPayload.TitleData.ContainsKey( "banner_full" ) )
//                {
//                    string value = loginResult.InfoResultPayload.TitleData [ "banner_full" ];

//                    if ( String.Equals( value , "on" ) )
//                        Bridge.fullbanner = true;
//                }

//                if ( loginResult.InfoResultPayload.TitleData.ContainsKey( "daily_time" ) )
//                {
//                    string value = loginResult.InfoResultPayload.TitleData [ "daily_time" ];

//                    double d;

//                    if ( double.TryParse( value , out d ) )
//                    {
//                        Bridge.hour = d;
//                    }
//                }
//            }

//            if ( !loginResult.NewlyCreated && ( loginResult.InfoResultPayload.PlayerProfile == null || loginResult.InfoResultPayload.UserInventory == null ) )
//            {
//                Debug.LogError( "User is not newly created and player profile and user inventory is null." );
//                Report.FatalError();
//                return;
//            }
//            else if ( loginResult.NewlyCreated && loginResult.InfoResultPayload.PlayerProfile == null )
//                loginResult.InfoResultPayload.PlayerProfile = new PlayerProfileModel();

//            string displayName = loginResult.InfoResultPayload.PlayerProfile.DisplayName;

//            if ( string.IsNullOrEmpty( displayName ) )
//            {
//                string googleName = Social.localUser.userName;

//                if ( !string.IsNullOrEmpty( googleName ) )
//                    UpdateDisplayName( googleName );
//                else
//                {
//                    string playerName = "CTS" + UnityEngine.Random.Range( 11111 , 99999 ).ToString();
//                    PlayerPrefs.SetString( "PlayerName" , playerName );
//                    UpdateDisplayName( playerName );
//                }
//            }

//            if ( successCallback != null )
//                successCallback.Invoke();

//            //if ( loginResult.NewlyCreated ) // new user
//            //{
//            Debug.Log( GetType().Name + ".cs " + " user logged with id " + loginResult.PlayFabId + " with ticket " + loginResult.SessionTicket );
//            //}


//            // loginResult.InfoResultPayload.UserInventory
//            // loginResult.InfoResultPayload.PlayerStatistics
//            // loginResult.InfoResultPayload.UserVirtualCurrency
//        }

//        void OnFailed ( PlayFabError error )
//        {
//            Debug.Log( GetType().Name + ".cs " + " " + error );

//            if ( failureCallback != null )
//                failureCallback();

//            // Acknowlegde the user 
//        }

//        LoginWithCustomIDRequest GetRequest ()
//        {
//            LoginWithCustomIDRequest  request = new LoginWithCustomIDRequest();

//            request.TitleId = PlayFabSettings.TitleId;
//            request.CreateAccount = true;
//            request.CustomId = SystemInfo.deviceUniqueIdentifier;
//            request.InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() { GetUserInventory = true , GetPlayerStatistics = true , GetUserVirtualCurrency = true , GetPlayerProfile = true , GetTitleData = true };

//            return request;
//        }

//        LoginWithGoogleAccountRequest GetGoogleRequest ( string authCode )
//        {
//            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest();
//            request.TitleId = PlayFabSettings.TitleId;
//            request.ServerAuthCode = authCode;
//            request.CreateAccount = true;
//            request.InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() { GetUserInventory = true , GetPlayerStatistics = true , GetUserVirtualCurrency = true , GetPlayerProfile = true , GetTitleData = true };

//            return request;
//        }

//        private void OnApplicationFocus ( bool focus ) // ??
//        {
//            if ( focus ) // are we eligible for db operations
//            {

//            }
//        }
//    }

//    public class AuthParams
//    {
//        public int type = 0;
//        public enum Type { None = 0, Anonymous = 1, Google = 2 }

//        public AuthParams ( Type type )
//        {
//            this.type = ( int ) type;
//        }

//        public Type Info { get { return ( Type ) type; } }
//    }
//}