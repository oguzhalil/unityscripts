//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using EboxGames;
//using UnityEngine.UI;
//using DentedPixel;
//using PlayFab;
//using PlayFab.ClientModels;

//namespace PabloGames
//{
//    public class PGMenuFeatures : Singleton<PGMenuFeatures>
//    {
//        public Text labelCoin;

//        public float from;

//        public GameObject signInButton;
//        public GameObject nameTag;

//        public Text nickName;

//        private void Start ()
//        {
//            Bridge.UpdateProperties(); // Update Properties for photon network!

//            Bridge.PostLeaderboard(); // Post Leaderboard

//            // may be false keep it at the bottom
//            //AddCoin( Bridge.authentication.loginResult == null ? 0 : Bridge.authentication.loginResult.InfoResultPayload.UserVirtualCurrency [ "VC" ] );
//            UpdateCoin();
//        }


//        private void Update ()
//        {
//            if ( Social.localUser.authenticated )
//            {
//                signInButton.SetActive( false );
//                nameTag.SetActive( true );

//                if ( Bridge.authentication.loginResult != null )
//                    nickName.text = Bridge.authentication.loginResult.InfoResultPayload.PlayerProfile.DisplayName;
//            }
//            else
//            {
//                nameTag.SetActive( false );
//                signInButton.SetActive( true );

//            }
//        }

//        public void OnPressedSignIn ()
//        {
//            PabloGames.Bridge.authentication.LinkGoogle();
//        }

//        public void OnPressedAchievements ()
//        {
//            Social.ShowAchievementsUI();
//        }

//        public void OnPressedMoreGames()
//        {
//            Application.OpenURL( "https://play.google.com/store/apps/developer?id=PabloGame" );
//        }

//        public void OnPressedRateUs ()
//        {
//            //@NOTE: Application.identifier works for android may be differ for IOS
//            Application.OpenURL( "market://details?id=" + Application.identifier );
//        }

//        public void UpdateLeaderboard ()
//        {
//            //PlayFab.PlayFabClientAPI.GetLeaderboard( new PlayFab.ClientModels.GetLeaderboardRequest() { } )
//        }

//        public void OnDiscord ()
//        {
//            Application.OpenURL( "https://discord.gg/59tFThR" );
//        }

//        public void OnFacebook ()
//        {
//            Application.OpenURL( "https://www.facebook.com/CounterCriticalStrike/" );
//        }

//        public void AddCoin ( float value )
//        {
//            float to = from + value;

//            LeanTween.value( from , to , 1f )
//                .setOnUpdate( LTUpdate )
//                .setOnComplete( () => { LTComplete( to ); } );

//            from += value;
//        }

//        void LTComplete ( float value )
//        {
//            int coin = (int)value;
//            labelCoin.text = coin.ToString();
//            UpdateCoin();
//        }

//        void LTUpdate ( float value )
//        {
//            int coin = (int)value;
//            labelCoin.text = coin.ToString();
//        }

//        public void UpdateCoin()
//        {
//            PlayFab.PlayFabClientAPI.GetUserInventory( 
//                new PlayFab.ClientModels.GetUserInventoryRequest() { } ,
//                (result) => { this.from = result.VirtualCurrency [ "VC" ]; labelCoin.text = this.from.ToString(); } ,
//                (error)=> { Debug.LogError( error.GenerateErrorReport() ); } );
//        }


//    }
//}
