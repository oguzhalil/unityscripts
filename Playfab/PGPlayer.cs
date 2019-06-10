//using UnityEngine;
//using Photon;

//using Hashtable = ExitGames.Client.Photon.Hashtable;

//// Attached To Player.prefab (CounterStrikeClone/Prefabs/Player.prefab)
//public class PGPlayer : Photon.PunBehaviour
//{
//    public const string scoreKey = "#score_";

//    public const string tempKey = "#temp_Score";

//    public override void OnPhotonPlayerPropertiesChanged ( object [] playerAndUpdatedProps )
//    {
//        if ( playerAndUpdatedProps == null || playerAndUpdatedProps.Length < 2 )
//        {
//            Debug.LogError( "playerAndUpdateProps is null or length is less than 2" );
//            return;
//        }

//        PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;

//        if ( !player.IsLocal )
//        {
//            //Debug.Log( GetType().Name + ".cs " + " Player is not local returning." );
//            return;
//        }

//        Hashtable props = playerAndUpdatedProps[1] as Hashtable;

//        // 1
//        // 3
//        // 10
//        // 10 -> disregard this
//        // 3
//        // 4
//        // 15
//        if ( props.ContainsKey( "Kills" ) )
//        {
//            int kills = (int) props["Kills"];

//            // a  = kill
//            // b = previousKills
//            // c = kill - previous
//            // if ( c < 0 ) c = b - a // because kill could be resetted!

//            int totalKill = PlayerPrefs.GetInt( scoreKey , 0 );

//            int previousKills = PlayerPrefs.GetInt(tempKey , 0);

//            // diff
//            int diff = kills - previousKills;

//            if(diff < 0)
//                diff = previousKills - diff;

//            PlayerPrefs.SetInt( scoreKey , totalKill + diff );

//            PlayerPrefs.SetInt( tempKey , kills ); // copy of previous kill
//        }


//    }
//}
