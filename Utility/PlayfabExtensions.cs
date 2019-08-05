using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayfabExtension
{
    public static bool ContainsItem ( this List<ItemInstance> list , string id )
    {
        for ( int i = 0; i < list.Count; i++ )
        {
            if ( list [ i ].ItemId == id )
            {
                return true;
            }
        }

        return false;
    }

    public static int GetStatistic ( this List<StatisticValue> list , string id )
    {
        for ( int i = 0; i < list.Count; i++ )
        {
            if ( list [ i ].StatisticName == id )
            {
                return list [ i ].Value;
            }
        }

        return 0;
    }

    public static PlayerLeaderboardEntry GetEntry ( this List<PlayerLeaderboardEntry> players , string id )
    {
        for ( int i = 0; i < players.Count; i++ )
        {
            if ( players [ i ].PlayFabId == id )
            {
                return players [ i ];
            }
        }

        return null;
    }

    public static int GetCount ( this List<ItemInstance> items , string _class )
    {
        int count = 0;

        for ( int i = 0; i < items.Count; i++ )
        {
            if ( items [ i ].ItemClass == _class )
            {
                ++count;
            }
        }

        return count;
    }

    public static int GetDiff ( this List<StatisticValue> list , string a , string b )
    {
        return list.GetStatistic( a ) - list.GetStatistic( b );
    }

    public static int GetCurrency ( this Dictionary<string , int> currencies , string id )
    {
        if ( currencies.ContainsKey( id ) )
        {
            return currencies [ id ];
        }

        return 0;
    }
}
