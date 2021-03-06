﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor( typeof( UtilitySettings ) )]
public class UtilitySettingsEditor : Editor
{
    private UtilitySettings settings;
    private string defineSymbols;

    public const string symbolAdmob = "ENABLE_ADMOB";
    public const string symbolUnityAds = "ENABLE_UNITYADS";
    public const string symbolGPGS = "ENABLE_GPGS";
    public const string symbolPlayfab = "ENABLE_PLAYFAB";
    public const string symbolLogger = "ENABLE_LOGS";
    public const string symbolInApp = "ENABLE_INAPP";
    public const string symbolLeanTween = "ENABLE_LEANTWEEN";


    public override void OnInspectorGUI ()
    {
        EditorGUI.BeginChangeCheck();
        settings.googlePlayGameServices = EditorGUILayout.Toggle( "Google Play Services" , settings.googlePlayGameServices );
        settings.playfabServices = EditorGUILayout.Toggle( "Playfab Services" , settings.playfabServices );
        settings.admobServices = EditorGUILayout.Toggle( "Admob Services" , settings.admobServices );
        settings.unityAdsServices = EditorGUILayout.Toggle( "Unity Ads Services" , settings.unityAdsServices );
        settings.loggerEnabled = EditorGUILayout.Toggle( "Logger Enabled" , settings.loggerEnabled );
        settings.inAppPurcases = EditorGUILayout.Toggle( "In App Purcases Enabled" , settings.inAppPurcases );
        settings.leanTween = EditorGUILayout.Toggle( "Lean Tween Imported" , settings.leanTween );



        if ( EditorGUI.EndChangeCheck() )
        {
            ChangeSymbol( settings );
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable ()
    {
        settings = target as UtilitySettings;
    }

    private void ChangeSymbol ( UtilitySettings settings ) // def CROSS_PLATFORM_INPUT;BCG_RCC
    {
        string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup( BuildTargetGroup.Android );

        string [] seperatedSymbols = symbols.Split( ';' );
        List<string> newSymbols = new List<string>();

        if ( settings.googlePlayGameServices )
        {
            newSymbols.Add( symbolGPGS );
        }
        if ( settings.unityAdsServices )
        {
            newSymbols.Add( symbolUnityAds );
        }
        if ( settings.admobServices )
        {
            newSymbols.Add( symbolAdmob );
        }
        if ( settings.loggerEnabled )
        {
            newSymbols.Add( symbolLogger );
        }
        if ( settings.playfabServices )
        {
            newSymbols.Add( symbolPlayfab );
        }
        if ( settings.inAppPurcases )
        {
            newSymbols.Add( symbolInApp );
        }
        if ( settings.leanTween )
        {
            newSymbols.Add( symbolLeanTween );
        }

        foreach ( var seperatedSymbol in seperatedSymbols )
        {
            switch ( seperatedSymbol )
            {
                case symbolAdmob:
                    break;
                case symbolUnityAds:
                    break;
                case symbolGPGS:
                    break;
                case symbolPlayfab:
                    break;
                case symbolLogger:
                    break;
                case symbolInApp:
                    break;
                case symbolLeanTween:
                    break;
                default:
                    newSymbols.Add( seperatedSymbol );
                    break;
            }
        }

        if ( newSymbols.Count == 0 )
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup( BuildTargetGroup.Android , string.Empty );
        }
        else
        {
            string output = newSymbols [ 0 ];

            for ( int i = 1; i < newSymbols.Count; i++ )
            {
                output += ";" + newSymbols [ i ];
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup( BuildTargetGroup.Android , output );
        }
    }
}
