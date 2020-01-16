#if ENABLE_GPGS

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PabloGames
{

    public class PlayServices
    {
        public PlayServices ()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        //.EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(<callback method>)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(<callback method>)
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        //.RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        .RequestServerAuthCode( false )
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        //.RequestIdToken()
        // 
        .AddOauthScope( "profile" )
        .Build();

            PlayGamesPlatform.InitializeInstance( config );
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = false;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
        }

        public void SignIn ( Action<string> success , Action error , bool silent )
        {
            if ( PlayGamesPlatform.Instance.localUser.authenticated )
            {
                PlayGamesPlatform.Instance.GetAnotherServerAuthCode( true , ( authCode ) =>
                {
                    if ( string.IsNullOrEmpty( authCode ) )
                        error();
                    else
                        success( authCode );
                }
                );
            }
            else
                PlayGamesPlatform.Instance.Authenticate( ( result , errMsg ) =>
                {
                    if ( result )
                    {
                        string authCode = PlayGamesPlatform.Instance.GetServerAuthCode();

                        if ( string.IsNullOrEmpty( authCode ) )
                        {
                            Debug.LogError( GetType().Name + ".cs authCode parameters is null or empty." );
                            result = false;
                            goto Failure;
                        }

                        success( authCode );
                    }

                Failure:
                    if ( !result )
                    {
                        Debug.LogError( "Google Play Authentication failed with error " + errMsg );
                        if ( error != null )
                            error();
                    }

                } , silent );
        }
    }
}
#endif