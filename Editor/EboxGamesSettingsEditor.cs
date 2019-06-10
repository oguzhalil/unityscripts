using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor( typeof( EboxGamesSettings ) )]
public class EboxGamesSettingsEditor : Editor
{
    public SerializedProperty googlePlayGameServices;
    public SerializedProperty playfabServices;
    public SerializedProperty admobServices;
    public SerializedProperty unityAdsServices;
    private EboxGamesSettings settings;
    private string defineSymbols;
    public const string symbolAdmob = "ADMOB";
    public const string symbolUnityAds = "UNITY_ADS";
    public const string symbolGPGS = "GPGS";
    public const string symbolPlayfab = "PLAYFAB";

    public override void OnInspectorGUI ()
    {
        bool bGoogle = googlePlayGameServices.boolValue;
        bool bPlayfab = playfabServices.boolValue;
        bool bAdmob = admobServices.boolValue;
        bool bUnityAds = unityAdsServices.boolValue;

        EditorGUILayout.PropertyField( googlePlayGameServices );
        EditorGUILayout.PropertyField( playfabServices );
        EditorGUILayout.PropertyField( admobServices );
        EditorGUILayout.PropertyField( unityAdsServices );

        serializedObject.ApplyModifiedProperties();
        if ( bGoogle != googlePlayGameServices.boolValue )
        {
            ChangeSymbol( symbolGPGS , googlePlayGameServices.boolValue );
        }

        if (bPlayfab != playfabServices.boolValue)
        {
            ChangeSymbol( symbolPlayfab , playfabServices.boolValue );
        }

        if (bAdmob  != admobServices.boolValue)
        {
            ChangeSymbol( symbolAdmob , admobServices.boolValue );
        }

        if (bUnityAds != unityAdsServices.boolValue)
        {
            ChangeSymbol( symbolUnityAds , unityAdsServices.boolValue );
        }
    }

    private void OnEnable ()
    {
        settings = target as EboxGamesSettings;
        googlePlayGameServices = serializedObject.FindProperty( "googlePlayGameServices" );
        playfabServices = serializedObject.FindProperty( "playfabServices" );
        admobServices = serializedObject.FindProperty( "admobServices" );
        unityAdsServices = serializedObject.FindProperty( "unityAdsServices" );
        defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup( BuildTargetGroup.Standalone );
    }

    void ChangeSymbol ( string symbol , bool action ) // def CROSS_PLATFORM_INPUT;BCG_RCC
    {
        int n = defineSymbols.Length;

        List<string> elems = new List<string>( defineSymbols.Split( ';'));

        for ( int i = elems.Count - 1; i >= 0; i-- )
        {
            if ( elems [ i ] == symbol )
            {
                if ( action )
                {
                    break;
                }
                else
                {
                    elems.RemoveAt( i );
                }
            }
            else if(i == 0) // last element
            {
                if ( action )
                    elems.Add( symbol );
            }
        }

        string output = string.Empty;

        foreach ( var elem in elems )
        {
            output += elem + ";";
            Debug.Log( elem );
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup( BuildTargetGroup.Standalone , output);
        defineSymbols = output;
    }
}
