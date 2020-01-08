#if ENABLE_ADMOB
using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UtilityScripts
{
    public class AdmobHandler : UniqueSingleton<AdmobHandler>
    {
        [Header( "Admob Identications" )]
        public string idsApp = "ca-app-pub-5545711692730045~6411072599";
        public string idsBanner = "ca-app-pub-5545711692730045/2112734930";
        public string idsInterstitial = "ca-app-pub-5545711692730045/3635190855";
        public string idsRewarded = "ca-app-pub-5545711692730045/3252047477";
        public string IOS_idsApp = "ca-app-pub-5545711692730045~6411072599";
        public string IOS_idsBanner = "ca-app-pub-5545711692730045/2112734930";
        public string IOS_idsInterstitial = "ca-app-pub-5545711692730045/3635190855";
        public string IOS_idsRewarded = "ca-app-pub-5545711692730045/3252047477";
        private const string idsTestBannerId = "ca-app-pub-3940256099942544/6300978111";
        private const string idsTestInterstitial = "ca-app-pub-3940256099942544/1033173712";
        private const string idsTestRewarded = "ca-app-pub-3940256099942544/5224354917";
        public bool testMode;
        public int interstitialCounter;
        public int interstitialShowCount;
        public const string keyPremium = "#premium";
        public bool IsPremiumUser { private set; get; }
        private InterstitialAd interstitialAd;
        private BannerView bannerView;
        private RewardedAd rewardedAd;
        private Action<bool> onRewardedCallback;

        protected override void Awake ()
        {
            base.Awake();
            MobileAds.Initialize( idsApp );
            MobileAds.SetiOSAppPauseOnBackground( true );


#if UNITY_IOS || UNITY_EDITOR
            idsApp = IOS_idsApp;
            idsBanner = IOS_idsBanner;
            idsInterstitial = IOS_idsInterstitial;
            idsRewarded = IOS_idsRewarded;
#endif

            if ( testMode )
            {
                idsBanner = idsTestBannerId;
                idsInterstitial = idsTestInterstitial;
                idsRewarded = idsTestRewarded;
            }

            IsPremiumUser = PlayerPrefs.HasKey( keyPremium );

            if ( !IsPremiumUser )
            {
                this.RequestBanner();
                this.RequestInterstitialAd();
                this.RequestRewardBasedVideo();
            }
        }

        public void ActivatePremium ()
        {
            IsPremiumUser = true;

            if ( bannerView != null )
                bannerView.Destroy();

            if ( interstitialAd != null )
                interstitialAd.Destroy();
        }

        public void HideBanner ()
        {
            if ( bannerView != null )
                bannerView.Hide();
        }

        public void ShowBanner ()
        {
            if ( bannerView != null )
                bannerView.Show();
        }

        public void ShowInterstitial ()
        {
            if ( interstitialAd.IsLoaded() )
            {
                interstitialCounter++;
                if ( interstitialCounter >= interstitialShowCount )
                {
                    interstitialCounter = 0;
                    interstitialAd.Show();
                }
            }
            else
            {
                interstitialCounter++;
                RequestInterstitialAd();
            }
        }

        public bool ShowRewardBasedVideo (Action<bool> onRewardedCallback)
        {
            this.onRewardedCallback = onRewardedCallback;
            if ( rewardedAd.IsLoaded() )
            {
                rewardedAd.Show();
                return true;
            }
            else
            {
                RequestRewardBasedVideo();
                return false;
            }
        }

        private void RequestBanner ()
        {
            if ( bannerView != null )
            {
                bannerView.Destroy();
            }

            bannerView = new BannerView( idsBanner , AdSize.Banner , AdPosition.Bottom );
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd( request );
        }

        public void RequestInterstitialAd ()
        {
            if ( interstitialAd != null )
            {
                interstitialAd.Destroy();
            }

            interstitialAd = new InterstitialAd( idsInterstitial );
            interstitialAd.OnAdClosed += OnInterstitialAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            interstitialAd.LoadAd( request );
        }

        public void RequestRewardBasedVideo ()
        {
            if ( rewardedAd != null )
            {
                rewardedAd = null;
            }

            rewardedAd = new RewardedAd( idsRewarded );
            rewardedAd.OnAdClosed += OnRewardedAdClosed;
            rewardedAd.OnAdFailedToLoad += OnRewardedAdClosed;
            rewardedAd.OnUserEarnedReward += OnRewardEarned;

            AdRequest request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd( request );
        }

        private void OnRewardEarned ( object sender , Reward e )
        {
            onRewardedCallback.SafeInvokeDelete( true );
        }

        private void OnRewardedAdClosed ( object sender , EventArgs e )
        {
            onRewardedCallback.SafeInvokeDelete( false );
            RequestRewardBasedVideo();
        }

        private void OnInterstitialAdClosed ( object sender , EventArgs e )
        {
            RequestInterstitialAd();
        }

        // Returns an ad request with custom ad targeting.
        private AdRequest CreateAdRequest ()
        {
            return new AdRequest.Builder()
                .AddTestDevice( AdRequest.TestDeviceSimulator )
                .AddTestDevice( "0123456789ABCDEF0123456789ABCDEF" )
                .AddKeyword( "game" )
                .SetGender( Gender.Male )
                .SetBirthday( new DateTime( 1985 , 1 , 1 ) )
                .TagForChildDirectedTreatment( false )
                .AddExtra( "color_bg" , "9B30FF" )
                .Build();
        }
    }
}
#endif