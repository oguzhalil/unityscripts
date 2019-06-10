//using GoogleMobileAds.Api;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;

//public class AdmobHandler : MonoBehaviour
//{
//    public static AdmobHandler Instance { get; private set; }

//    void Awake ()
//    {
//        if ( Instance != null )
//            Destroy( gameObject );
//        else
//        {
//            Instance = this;
//            DontDestroyOnLoad( gameObject );
//        }
//    }

//    const string appId = "ca-app-pub-5545711692730045~6411072599"; // real app id

//    string bannerId = "ca-app-pub-5545711692730045/2112734930";
//    string interstitialId = "ca-app-pub-5545711692730045/3635190855";
//    string rewardedVideoId = "ca-app-pub-5545711692730045/3252047477";

//    string testBannerId = "ca-app-pub-3940256099942544/6300978111"; // test id
//    string testInterstitialId = "ca-app-pub-3940256099942544/1033173712"; // test id
//    string testRewardedVideoId = "ca-app-pub-3940256099942544/5224354917"; // test id

//    private BannerView bannerView;
//    private InterstitialAd interstitial;

//    public bool test;

//    public RewardBasedVideoAd rewardedVideo { get { return RewardBasedVideoAd.Instance; } }

//    public string placementId = "rewardedVideo";

//    public string unityGameId = "3178505";
//    //private static int interstitialcounter;

//    int count;

//    public const string premium = "premium";

//    bool premiumUser;

//    private void OnEnable ()
//    {
//        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
//    }

//    private void SceneManager_sceneLoaded ( Scene arg0 , LoadSceneMode arg1 )
//    {
//        if ( !PabloGames.Bridge.fullbanner )
//            return;

//        StartCoroutine( this.AsyncTask( () =>
//        {
//            if ( bannerView == null )
//                return;

//            if ( arg0.buildIndex > 1 )
//            {
//                bannerView.SetPosition( AdPosition.Top );
//                //bannerView.LoadAd( new AdRequest.Builder().Build() );
//            }
//            else
//            {
//                bannerView.SetPosition( AdPosition.Bottom );
//                //bannerView.LoadAd( new AdRequest.Builder().Build() );
//            }

//            //if ( arg0.buildIndex > 1 )
//            //{
//            //    bannerView.Destroy();
//            //    RequestBanner( AdPosition.Top );
//            //}
//            //else
//            //{
//            //    bannerView.Destroy();
//            //    RequestBanner( AdPosition.Bottom );
//            //}
//        } ) );
//    }

//    private void OnDisable ()
//    {
//        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
//    }

//    void Start ()
//    {
//        MobileAds.Initialize( appId );

//        if ( test )
//        {
//            bannerId = testBannerId;
//            interstitialId = testInterstitialId;
//            rewardedVideoId = testRewardedVideoId;
//        }

//        premiumUser = PlayerPrefs.HasKey( premium );

//        //bannerView.Hide();
//        //bannerView.Show();
//        if ( !premiumUser )
//        {
//            this.RequestBanner();
//            this.RequestInterstitialAd();
//            this.RequestRewardBasedVideo();
//        }

//        Advertisement.Initialize( unityGameId , test );

//    }

//    public void ActivatePremium ()
//    {
//        premiumUser = true;

//        if ( bannerView != null )
//            bannerView.Destroy();

//        if ( interstitial != null )
//            interstitial.Destroy();
//    }

//    IEnumerator Delay ( float time , Action action )
//    {
//        yield return new WaitForSeconds( time );
//        action();
//    }

//    public void HideBanner ()
//    {
//        if ( bannerView != null && !PabloGames.Bridge.fullbanner )
//            bannerView.Hide();
//    }

//    public void ShowBanner ()
//    {
//        if ( bannerView != null )
//            bannerView.Show();
//    }

//    public void ShowInterstitial ( float? time = null )
//    {
//        if ( premiumUser )
//            return;

//        if ( time == null )
//        {
//            interstitial.Show();
//            RequestInterstitialAd();
//        }
//        else
//        {
//            StartCoroutine( Delay( time.Value , () =>
//            {
//                interstitial.Show();
//                RequestInterstitialAd();
//            } ) );
//        }
//    }

//    [ContextMenu( "UnityAds" )]
//    public void TestUnityAds ()
//    {
//        ShowAdUnityAds();
//    }

//    public string placementIdInter = "Interstitial";

//    public void ShowAdUnityAds ()
//    {
//        if ( premiumUser )
//            return;

//        StartCoroutine( ShowAdWhenReady() );
//    }

//    private IEnumerator ShowAdWhenReady ()
//    {
//        while ( !Advertisement.IsReady( placementIdInter ) )
//        {
//            yield return new WaitForSeconds( 0.25f );
//        }

//        Advertisement.Show( placementIdInter );

//        //ad = Monetization.GetPlacementContent( placementId ) as ShowAdPlacementContent;

//        //if ( ad != null )
//        //{
//        //    ad.Show();
//        //}
//    }

//    public void ShowRewardBasedVideo ()
//    {
//        if ( this.rewardedVideo.IsLoaded() )
//            this.rewardedVideo.Show();
//    }

//    #region Request

//    private void RequestBanner ()
//    {
//        bannerView = new BannerView( bannerId , AdSize.Banner , AdPosition.Bottom );

//        AdRequest request = new AdRequest.Builder().Build();

//        bannerView.LoadAd( request );
//    }

//    public void RequestInterstitialAd ()
//    {
//        interstitial = new InterstitialAd( interstitialId );

//        AdRequest request = new AdRequest.Builder().Build();

//        interstitial.LoadAd( request );
//    }

//    public void RequestRewardBasedVideo ()
//    {
//        AdRequest request = new AdRequest.Builder().Build();

//        this.rewardedVideo.LoadAd( request , rewardedVideoId );
//    }

//    #endregion Request

//}

//public static class HelperAd
//{
//    public static IEnumerator AsyncTask ( this MonoBehaviour monoBehaviour , Action action )
//    {
//        yield return null;
//        action();
//    }
//}

