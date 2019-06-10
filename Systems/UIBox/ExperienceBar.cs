//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Facebook.Unity;
//using PlayFab;
//using PlayFab.ClientModels;

//namespace BasketballMaster
//{
//    public class ExperienceBar : MonoBehaviour
//    {
//        public Text labelLevel;
//        public Image imgSlider;
//        public Image imgProfile;

//        public bool remotePlayer;

//        IEnumerator Start ()
//        {
//            imgProfile.gameObject.SetActive( false );

//            if ( remotePlayer )
//                yield break;

//            yield return new WaitUntil( () => FB.IsLoggedIn && Database.facebookManager.sprProfilePicture != null );

//            Sprite picture = Database.facebookManager.sprProfilePicture;

//            imgProfile.sprite = picture;
//            imgProfile.preserveAspect = true;
//            imgProfile.gameObject.SetActive( true );


//        }

//        void OnEnable ()
//        {
//            labelLevel.text = ( 1 + Database.GetLevel ).ToString(); // + 1 because level starting from zero!
//            imgSlider.fillAmount = ( float ) Database.experience / ( float ) Database.GetNextLevelExperience();

//            if ( !PlayFabClientAPI.IsClientLoggedIn() )
//                return;

//            PlayFabClientAPI.GetPlayerStatistics(
//                new GetPlayerStatisticsRequest() { StatisticNames = Database.GetStatisticsName() } ,
//                result =>
//                {
//                    var stats = result.Statistics;

//                    foreach ( var stat in stats )
//                    {
//                        switch ( stat.StatisticName )
//                        {
//                            case "experience":
//                                Database.experience = stat.Value;
//                                Photon.Pun.PhotonNetwork.LocalPlayer.SetCustomProperties(
//                                    new ExitGames.Client.Photon.Hashtable() { { "experience" , stat.Value } } );
//                                break;
//                            default:
//                                break;
//                        }

//                        //Debug.Log( GetType().Name + ".cs " + $"{stat.StatisticName} { stat.Value}" );
//                    }

//                    labelLevel.text = ( 1 + Database.GetLevel ).ToString(); // + 1 because level starting from zero!
//                    imgSlider.fillAmount = ( float ) Database.experience / ( float ) Database.GetNextLevelExperience();

//                } ,
//                error => { Debug.LogError( error.GenerateErrorReport() ); } );
//        }
//    }
//}
