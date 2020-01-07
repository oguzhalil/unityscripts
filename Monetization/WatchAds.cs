using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace UtilityScripts
{
    public class WatchAds : UniqueSingleton<WatchAds>
    {
        public string m_rewardedVideoId = "";
        public string m_testRewardedVideoId = "ca-app-pub-3940256099942544/5224354917";
        public bool m_bTest;
        private RewardBasedVideoAd rewardedVideo => RewardBasedVideoAd.Instance;
        private Action<bool> m_action;

        private void Start ()
        {
            RequestRewardBasedVideo();
            rewardedVideo.OnAdRewarded += Ev_OnAdRewarded;
            rewardedVideo.OnAdClosed += Ev_OnAdClosed;
        }

        private void OnDestroy ()
        {
            rewardedVideo.OnAdRewarded -= Ev_OnAdRewarded;
            rewardedVideo.OnAdClosed -= Ev_OnAdClosed;
        }

        private void Ev_OnAdClosed ( object sender , EventArgs e )
        {
            m_action.SafeInvokeDelete( false );
            RequestRewardBasedVideo();
        }

        private void Ev_OnAdRewarded ( object sender , Reward e )
        {
            m_action.SafeInvokeDelete( true );
        }

        public void Run ( Action<bool> action )
        {
            Logger.Info( $"WatchAds.cs Run is called. RewardedVideo IsLoaded() = {rewardedVideo.IsLoaded()}" );

            if ( rewardedVideo.IsLoaded() )
            {
                rewardedVideo.Show();
            }
            else
            {
                action.SafeInvokeDelete( false );
            }
        }

        public void RequestRewardBasedVideo ()
        {
            AdRequest request = new AdRequest.Builder().Build();
            string id = m_bTest ? m_testRewardedVideoId : m_rewardedVideoId;
            rewardedVideo.LoadAd( request , id );
        }

        void Run ()
        {
            Debug.Log( GetType().Name + ".cs " + " enabled is called." );

            //if ( Admob.rewardedVideo.IsLoaded() )
            //{
            //    btnCoinReward.gameObject.SetActive( true );
            //}
            //else
            //{
            //    Admob.RequestRewardBasedVideo();

            //    Admob.rewardedVideo.OnAdLoaded -= RewardedVideo_OnAdLoaded;
            //    Admob.rewardedVideo.OnAdLoaded += RewardedVideo_OnAdLoaded;


            //    Debug.Log( GetType().Name + ".cs " + " registered for ad loaded." );

            //    btnCoinReward.gameObject.SetActive( false );
            //}
        }

        //private void RewardedVideo_OnAdLoaded ( object sender , EventArgs e )
        //{
        //    Debug.Log( GetType().Name + ".cs " + " ad is loaded ready to show! " + Admob.rewardedVideo.IsLoaded() );

        //    StartCoroutine( this.AsyncTask( Run ) );
        //}

        //private void RewardedVideo_OnAdClosed ( object sender , EventArgs e )
        //{
        //    StartCoroutine( this.AsyncTask( Unrewarded ) );
        //}

        //private void RewardedVideo_OnAdFailedToLoad ( object sender , GoogleMobileAds.Api.AdFailedToLoadEventArgs e )
        //{
        //    StartCoroutine( this.AsyncTask( Unrewarded ) );
        //}

        //private void RewardedVideo_OnAdRewarded ( object sender , GoogleMobileAds.Api.Reward e )
        //{

        //    StartCoroutine( this.AsyncTask( Rewarded ) );
        //}

        //void OnPressedAd ()
        //{
        //    Report.Run( "Watch Ads" , "Watch Ads To Get Free Coins!" , "WATCH" , Report.IconSprite.COIN ,
        //        () =>
        //        {
        //            Admob.rewardedVideo.OnAdRewarded += RewardedVideo_OnAdRewarded;
        //            Admob.rewardedVideo.OnAdClosed += RewardedVideo_OnAdClosed;

        //            Admob.ShowRewardBasedVideo();

        //            btnCoinReward.gameObject.SetActive( false );
        //        } );
        //}

        //void Rewarded ()
        //{
        //    Debug.LogError( "Rewarded" );
        //    PlayFabClientAPI.PurchaseItem( new PlayFab.ClientModels.PurchaseItemRequest() { CatalogVersion = "1.0" , ItemId = "vc_reward" , VirtualCurrency = "VC" } ,
        //    ( result ) =>
        //    {

        //        float value = uint.Parse( result.Items.Where( x => x.ItemId == "vc_reward" ).First().ItemClass );
        //        PGMenuFeatures.Instance.AddCoin( value );

        //        Report.Run( "Congratulations!" , "You got your free coins!" , "OKAY" , Report.IconSprite.COIN );
        //    } ,
        //    ( error ) => { Debug.LogError( "AdsReward encountered with an error." + error ); } );

        //    Unbind();
        //}

        //void Unrewarded ()
        //{
        //    Unbind();
        //}

        //void Unbind ()
        //{
        //    StopAllCoroutines();

        //    Admob.rewardedVideo.OnAdRewarded -= RewardedVideo_OnAdRewarded;
        //    Admob.rewardedVideo.OnAdClosed -= RewardedVideo_OnAdClosed;

        //    // TODO update coin.
        //    //charSelect.updateCoin( 500 );
        //    Run();
        //}


    }
}
