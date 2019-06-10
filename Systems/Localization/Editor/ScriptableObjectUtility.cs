using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class Util
{
    static int ss_id = 0;

    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void CreateAsset<T> () where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if ( path == "" )
        {
            path = "Assets";
        }
        else if ( Path.GetExtension( path ) != "" )
        {
            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset( asset , assetPathAndName );

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    [MenuItem( "Tools/Create Language Asset" )]
    public static void CreateItems ()
    {
        CreateAsset<LanguageAsset>();
    }

    [MenuItem( "Tools/CreateJSON" )]
    public static void CreateJSON ()
    {

        ////var asset = "";


        ////if ( path == "" )
        ////{
        ////    path = "Assets";
        ////}
        ////else if ( Path.GetExtension( path ) != "" )
        ////{
        ////    path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
        ////}

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/JSON_01.json"); // Assets/BasketballMaster/Scripts/Language/JSON_01.json

        string finalPath = @Application.dataPath + assetPathAndName.Remove ( 0 ,"Assets".Length );

        Debug.Log( finalPath );

        Debug.Log( assetPathAndName );

        Debug.Log( Application.dataPath );
        //AssetDatabase.CreateAsset( asset , assetPathAndName );


        if ( !File.Exists( finalPath ) )
        {
            // Create a file to write to.
            using ( StreamWriter sw = File.CreateText( finalPath ) )
            {
                sw.Write( "{\n}" );
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
    }

    [MenuItem( "Tools/Clear Preferences" )]
    public static void ClearPrefs ()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem( "Tools/Replace LText" )]
    public static void ReplaceLText ()
    {
        GameObject go = Selection.activeGameObject;

        if ( go == null )
            return;

        Text t = go.GetComponent<Text>();


        if ( t == null )
            return;

        Text copy = GetCopyOf( t , t );

        Debug.Log( copy.font.name );

        UnityEngine.Object.DestroyImmediate( go.GetComponent<Text>() );

        LText newText = go.AddComponent<LText>();

        // font
        newText.font = copy.font;
        newText.fontSize = copy.fontSize;

        // alignment
        newText.alignment = copy.alignment;

        // overflow
        newText.horizontalOverflow = copy.horizontalOverflow;
        newText.verticalOverflow = copy.verticalOverflow;

        // best fit
        newText.resizeTextForBestFit = copy.resizeTextForBestFit;
        newText.resizeTextMinSize = copy.resizeTextMinSize;
        newText.resizeTextMaxSize = copy.resizeTextMaxSize;

        // color
        newText.color = copy.color;

        //text
        newText.text = copy.text;

    }

    [MenuItem( "Tools/Replace LTEXT -> TEXT" )]
    public static void ReplaceText ()
    {
        GameObject go = Selection.activeGameObject;

        if ( go == null )
            return;

        LText t = go.GetComponent<LText>();


        if ( t == null )
            return;

        LText copy = GetCopyOf( t , t );

        Debug.Log( copy.font.name );

        UnityEngine.Object.DestroyImmediate( go.GetComponent<LText>() );

        Text newText = go.AddComponent<Text>();

        // font
        newText.font = copy.font;
        newText.fontSize = copy.fontSize;

        // alignment
        newText.alignment = copy.alignment;

        // overflow
        newText.horizontalOverflow = copy.horizontalOverflow;
        newText.verticalOverflow = copy.verticalOverflow;

        // best fit
        newText.resizeTextForBestFit = copy.resizeTextForBestFit;
        newText.resizeTextMinSize = copy.resizeTextMinSize;
        newText.resizeTextMaxSize = copy.resizeTextMaxSize;

        // color
        newText.color = copy.color;

        //text
        newText.text = copy.text;

    }

    public static T GetCopyOf<T> ( MonoBehaviour Monobehaviour , T Source ) where T : MonoBehaviour
    {
        //Type check
        Type type = Monobehaviour.GetType();
        if ( type != Source.GetType() ) return null;

        //Declare Binding Flags
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

        //Iterate through all types until monobehaviour is reached
        while ( type != typeof( MonoBehaviour ) )
        {
            //Apply Fields
            FieldInfo[] fields = type.GetFields(flags);
            foreach ( FieldInfo field in fields )
            {
                field.SetValue( Monobehaviour , field.GetValue( Source ) );
            }

            //Move to base class
            type = type.BaseType;
        }
        return Monobehaviour as T;
    }

    [MenuItem( "Tools/Reload Preloaded" )]
    public static void UpdatePreloaded ()
    {
        var preloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets();
        UnityEditor.PlayerSettings.SetPreloadedAssets( preloadedAssets );
    }

    [MenuItem( "Tools/ScreenShot" )]
    public static void TakeScreenShot ()
    {
        ScreenCapture.CaptureScreenshot( "ss_id" + ++ss_id + ".png" );

    }
}