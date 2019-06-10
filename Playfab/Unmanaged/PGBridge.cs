//using PlayFab;
//using PlayFab.ClientModels;
//using PlayFab.Internal;
//using PlayFab.SharedModels;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace PabloGames
//{
//    // Unmanaged static class functional across all game
//    public static class Bridge
//    {
//        public static SOCWeapon [] SOCWeapons;
//        public static readonly Authentication authentication;
//        public static readonly PlayServices gameServices;
//        public static long? serverTimestamp = null;
//        public static List<CatalogItem> catalogItems;
//        public static bool fullbanner;
//        public static double hour;

//        public static void UpdateSOCWeapon ()
//        {
//            if ( SOCWeapons == null || catalogItems == null )
//            {
//                Report.FatalError();
//                return;
//            }

//            for ( int i = 0; i < catalogItems.Count; i++ )
//            {
//                CatalogItem catalogItem = catalogItems[i];

//                // catalogItem.ItemClass -> matches with PGCustomWeapon weapon keys keyAk47 etc.
//                int index = PGCustomWeapon.KeyToIndex(catalogItem.ItemClass);

//                SOCWeapon socWeapon = SOCWeapons[index];

//                socWeapon.weapons [ 0 ].purchased = true;  // default item is always purchased.

//                foreach ( var weapon in socWeapon.weapons )
//                {
//                    if ( string.Equals( catalogItem.ItemId , weapon.name ) ) // if catalog item name matches with weapon name
//                    {
//                        weapon.price = System.Convert.ToInt32( catalogItem.VirtualCurrencyPrices [ "VC" ] ); // update price

//                        if ( !weapon.purchased && authentication.loginResult.InfoResultPayload.UserInventory != null) // if weapon not purchased
//                            weapon.purchased = authentication.loginResult.InfoResultPayload.UserInventory.Any( x => x.ItemId == catalogItem.ItemId ); // if inventory contains the weapon is purchased
//                    }
//                    else // not matches -> continue
//                        continue;
//                }
//            }
//        }

//        static List<StatisticUpdate> GetStatistics ()
//        {
//            return new List<StatisticUpdate> {
//                    new StatisticUpdate { StatisticName = "HighScore", Value = PlayerPrefs.GetInt( PGPlayer.scoreKey ) },
//                    new StatisticUpdate { StatisticName = "Montly", Value = PlayerPrefs.GetInt( PGPlayer.scoreKey ) },
//            };
//        }

//        public static void PostLeaderboard ()
//        {
//            if ( PlayerPrefs.HasKey( PGPlayer.scoreKey ) )
//                PlayFabClientAPI.UpdatePlayerStatistics( new UpdatePlayerStatisticsRequest() { Statistics = GetStatistics() } ,
//                    result => {
//                        PlayerPrefs.DeleteKey( PGPlayer.tempKey );
//                        PlayerPrefs.DeleteKey( PGPlayer.scoreKey );
//                    } ,
//                    error => { Debug.LogError( error.GenerateErrorReport()); } );
//        }


//        public static object [] InstantionData ()
//        {
//            int akId = PlayerPrefs.GetInt("ak47" , 0);
//            int m4Id = PlayerPrefs.GetInt("m4a1" , 0);
//            int famasId = PlayerPrefs.GetInt("famas" , 0);

//            return new object [] { akId , m4Id , famasId };
//        }

//        public static void UpdateProperties ()
//        {
//            var v = new ExitGames.Client.Photon.Hashtable();
//            v.Add( PGCustomWeapon.keyAug , PlayerPrefs.GetInt( PGCustomWeapon.keyAug , 0 ) );
//            v.Add( PGCustomWeapon.keyAk47 , PlayerPrefs.GetInt( PGCustomWeapon.keyAk47 , 0 ) );
//            v.Add( PGCustomWeapon.keyFamas , PlayerPrefs.GetInt( PGCustomWeapon.keyFamas , 0 ) );
//            v.Add( PGCustomWeapon.keyM4a1 , PlayerPrefs.GetInt( PGCustomWeapon.keyM4a1 , 0 ) );
//            PhotonNetwork.player.SetCustomProperties( v );
//        }

//        static Bridge ()
//        {
//            gameServices = new PlayServices(); // first

//            authentication = new Authentication();
//        }
//    }
//}
