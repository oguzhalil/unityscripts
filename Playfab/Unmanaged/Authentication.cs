using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

namespace EboxGames
{
    public class Authentication : LauncherTask
    {
        public static LoginResult m_PlayfabUser;
        private bool m_bIsDone = false;
        private bool m_bIsError = false;
        public const string m_sPrefsKey = "#login_method";

        public override void Run ()
        {
            m_bRunning = true;
            bool bStoredLogin = PlayerPrefs.HasKey( m_sPrefsKey );

            if ( bStoredLogin )
            {
                Logger.Info( $"There is stored login. Trying to login." );
                Params authParams = null;

                if ( Storage.Read( m_sPrefsKey , ref authParams , true ) )
                {
                    Params.Type accountType = authParams.Info;

                    if ( accountType == Params.Type.Google )
                    {
                        InitializeGPGS();
                        PlayGamesPlatform.Instance.Authenticate( result =>
                        {
                            if ( result )
                            {
                                string authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                                PlayFabClientAPI.LoginWithGoogleAccount( GetGoogleRequest( authCode ) ,
                                ( playfabUser ) =>
                                {
                                    m_PlayfabUser = playfabUser;
                                    Database.SetUser( playfabUser );
                                    m_bIsDone = true;
                                    Logger.Info( $"Logged in with google playfab id is {playfabUser.PlayFabId}" );
                                } ,
                                ( error ) =>
                                {
                                    m_bIsError = true;
                                    Debug.LogError( error.GenerateErrorReport() );
                                } );

                            }
                        } );
                    }
                    else if ( accountType == Params.Type.Guest )
                    {
                        PlayFabClientAPI.LoginWithCustomID( GetGuestRequest()
                        , playfabUser =>
                          {
                              m_PlayfabUser = playfabUser;
                              Database.SetUser( playfabUser );
                              m_bIsDone = true;
                              Logger.Info( $"Logged in as guest playfab id is {playfabUser.PlayFabId}" );
                          }
                         , error =>
                         {
                             m_bIsError = true;
                             Logger.Info( error.GenerateErrorReport() );
                         } );
                    }
                    else
                    {
                        Debug.LogError( $"Store login parameters are wrong. param: {authParams.Info}" );
                    }
                }
                else
                {
                    bStoredLogin = false;
                }
            }

            if ( !bStoredLogin ) // create new account
            {
                InitializeGPGS();
                PlayGamesPlatform.Instance.Authenticate( result =>
                {
                    if ( result )
                    {
                        string authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                        PlayFabClientAPI.LoginWithGoogleAccount( GetGoogleRequest( authCode ) ,
                        ( playfabUser ) =>
                        {
                            m_PlayfabUser = playfabUser;
                            Database.SetUser( playfabUser );
                            m_bIsDone = true;
                            Logger.Info( $"Logged in with google playfab id is {playfabUser.PlayFabId}" );
                            Storage.Write( m_sPrefsKey , new Params( Params.Type.Google ).ToString() , true );
                        } ,
                        ( error ) =>
                        {
                            m_bIsError = true;
                            Debug.LogError( error.GenerateErrorReport() );
                        } );

                    }
                } );
            }
        }

        private void InitializeGPGS ()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestServerAuthCode( false )
                .AddOauthScope( "profile" )
                .Build();
            PlayGamesPlatform.InitializeInstance( config );
            PlayGamesPlatform.DebugLogEnabled = false;
            PlayGamesPlatform.Activate();
        }

        void UpdateDisplayName ( string name )
        {
            //loginResult.InfoResultPayload.PlayerProfile.DisplayName = name;

            PlayFabClientAPI.UpdateUserTitleDisplayName(
                new UpdateUserTitleDisplayNameRequest() { DisplayName = name } ,
                ( r ) => { } ,
                ( e ) => { } );
        }

        void OnSuccess ( LoginResult loginResult )
        {
            //this.loginResult = loginResult;

            //loginResult.InfoResultPayload.AccountInfo.GoogleInfo.
            //loginResult.InfoResultPayload.AccountInfo.GoogleInfo.GoogleId

            //Bridge.fullbanner = false;

            //Bridge.hour = 6.0d;

            //if ( loginResult.InfoResultPayload.TitleData != null )
            //{
            //    if ( loginResult.InfoResultPayload.TitleData.ContainsKey( "banner_full" ) )
            //    {
            //        string value = loginResult.InfoResultPayload.TitleData [ "banner_full" ];

            //        if ( String.Equals( value , "on" ) )
            //            Bridge.fullbanner = true;
            //    }

            //    if ( loginResult.InfoResultPayload.TitleData.ContainsKey( "daily_time" ) )
            //    {
            //        string value = loginResult.InfoResultPayload.TitleData [ "daily_time" ];

            //        double d;

            //        if ( double.TryParse( value , out d ) )
            //        {
            //            Bridge.hour = d;
            //        }
            //    }
            //}

            //if ( !loginResult.NewlyCreated && ( loginResult.InfoResultPayload.PlayerProfile == null || loginResult.InfoResultPayload.UserInventory == null ) )
            //{
            //    Debug.LogError( "User is not newly created and player profile and user inventory is null." );
            //    Report.FatalError();
            //    return;
            //}
            //else if ( loginResult.NewlyCreated && loginResult.InfoResultPayload.PlayerProfile == null )
            //    loginResult.InfoResultPayload.PlayerProfile = new PlayerProfileModel();

            //string displayName = loginResult.InfoResultPayload.PlayerProfile.DisplayName;

            //if ( string.IsNullOrEmpty( displayName ) )
            //{
            //    string googleName = Social.localUser.userName;

            //    if ( !string.IsNullOrEmpty( googleName ) )
            //        UpdateDisplayName( googleName );
            //    else
            //    {
            //        string playerName = "CTS" + UnityEngine.Random.Range( 11111 , 99999 ).ToString();
            //        PlayerPrefs.SetString( "PlayerName" , playerName );
            //        UpdateDisplayName( playerName );
            //    }
            //}

            //if ( successCallback != null )
            //    successCallback.Invoke();

            ////if ( loginResult.NewlyCreated ) // new user
            ////{
            //Debug.Log( GetType().Name + ".cs " + " user logged with id " + loginResult.PlayFabId + " with ticket " + loginResult.SessionTicket );
            ////}


            // loginResult.InfoResultPayload.UserInventory
            // loginResult.InfoResultPayload.PlayerStatistics
            // loginResult.InfoResultPayload.UserVirtualCurrency
        }

        void OnFailed ( PlayFabError error )
        {
            Debug.Log( GetType().Name + ".cs " + " " + error );

            //if ( failureCallback != null )
            //    failureCallback();

            // Acknowlegde the user 
        }

        LoginWithCustomIDRequest GetGuestRequest ()
        {
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest();

            request.TitleId = PlayFabSettings.TitleId;
            request.CreateAccount = true;
            request.CustomId = SystemInfo.deviceUniqueIdentifier;
            request.InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() { GetUserInventory = true , GetPlayerStatistics = true , GetUserVirtualCurrency = true , GetPlayerProfile = true , GetTitleData = true };

            return request;
        }

        LoginWithGoogleAccountRequest GetGoogleRequest ( string authCode )
        {
            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest();
            request.TitleId = PlayFabSettings.TitleId;
            request.ServerAuthCode = authCode;
            request.CreateAccount = true;
            request.InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() { GetUserInventory = true , GetPlayerStatistics = true , GetUserVirtualCurrency = true , GetPlayerProfile = true , GetTitleData = true };

            return request;
        }

        private void OnApplicationFocus ( bool focus ) // ??
        {
            if ( focus ) // are we eligible for db operations
            {

            }
        }

        public override bool IsDone ()
        {
            return m_bIsDone;
        }

        public override bool IsError ()
        {
            return m_bIsError;
        }

        [System.Serializable]
        public class Params
        {
            public int type = 0;
            public enum Type { None = 0, Guest = 1, Google = 2 }

            public Params ( Type type )
            {
                this.type = ( int ) type;
            }

            public Type Info { get { return ( Type ) type; } }

            public override string ToString ()
            {
                return JsonUtility.ToJson( this );
            }
        }
    }

   
}