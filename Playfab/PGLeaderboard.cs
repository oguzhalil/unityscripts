//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PlayFab;
//using PlayFab.ClientModels;

//namespace PabloGames
//{
//    public class PGLeaderboard : MonoBehaviour
//    {
//        public LadderUser prefabLadderUser;

//        public Transform monthlyParent;
//        public Transform topParent;

//        public LadderUser localMonth;
//        public LadderUser localTop;

//        public RankIcon[] rankIcons;

//        public Sprite privateIcon;

//        public Sprite GetIcon ( int position )
//        {
//            if ( rankIcons.Length > position )
//                return rankIcons [ position ].sprite;

//            return privateIcon;
//        }

//        private void Awake ()
//        {

//        }

//        public void ListMonth ()
//        {

//            for ( int i = 1; i < monthlyParent.childCount; i++ )
//                Destroy( monthlyParent.GetChild( i ).gameObject );

//            PlayFabClientAPI.GetLeaderboard(
//                new PlayFab.ClientModels.GetLeaderboardRequest() { StatisticName = "Montly" , MaxResultsCount = 50 } ,
//                ( result ) =>
//                {
//                    foreach ( var user in result.Leaderboard )
//                    {
//                        LadderUser ladderUser = Instantiate( prefabLadderUser , monthlyParent );
//                        ladderUser.UpdateUser( user.DisplayName , user.StatValue.ToString() , ( 1 + user.Position ).ToString() , GetIcon( user.Position ) );
//                    }
//                } ,
//                ( e ) =>
//                {
//                    Debug.LogError( e.GenerateErrorReport() );

//                } );
//            PlayFabClientAPI.GetLeaderboardAroundPlayer(
//                new PlayFab.ClientModels.GetLeaderboardAroundPlayerRequest() { MaxResultsCount = 1 , StatisticName = "Montly" } ,
//                ( r ) =>
//                {
//                    foreach ( var user in r.Leaderboard )
//                    {
//                        localMonth.UpdateUser( user.DisplayName , user.StatValue.ToString() , ( 1 + user.Position ).ToString() , GetIcon( user.Position ) );
//                        localMonth.gameObject.SetActive( true );
//                    }
//                } ,
//                ( e ) =>
//                {
//                    Debug.LogError( e.GenerateErrorReport() );
//                } );
//        }

//        public void ListTop ()
//        {
//            for ( int i = 1; i < topParent.childCount; i++ )
//                Destroy( topParent.GetChild( i ).gameObject );

//            PlayFabClientAPI.GetLeaderboard(
//               new PlayFab.ClientModels.GetLeaderboardRequest() { StatisticName = "HighScore" , MaxResultsCount = 50 } ,
//               ( result ) =>
//               {
//                   foreach ( var user in result.Leaderboard )
//                   {
//                       LadderUser ladderUser = Instantiate( prefabLadderUser , topParent );
//                       ladderUser.UpdateUser( user.DisplayName , user.StatValue.ToString() , ( 1 + user.Position ).ToString() , GetIcon( user.Position ) );
//                   }
//               } ,
//               ( e ) =>
//               {
//                   Debug.LogError( e.GenerateErrorReport() );
//               } );
//            PlayFabClientAPI.GetLeaderboardAroundPlayer(
//                new PlayFab.ClientModels.GetLeaderboardAroundPlayerRequest() { MaxResultsCount = 1 , StatisticName = "HighScore" } ,
//                ( result ) =>
//                {
//                    foreach ( var user in result.Leaderboard )
//                    {
//                        localTop.UpdateUser( user.DisplayName , user.StatValue.ToString() , ( 1 + user.Position ).ToString() , GetIcon( user.Position ) );
//                        localTop.gameObject.SetActive( true );
//                    }
//                } ,
//                ( e ) =>
//                {
//                    Debug.LogError( e.GenerateErrorReport() );
//                } );
//        }

//        [System.Serializable]
//        public class RankIcon
//        {
//            public Sprite sprite;
//            public int position;
//        }
//    }
//}
