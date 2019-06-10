using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAsset : MonoBehaviour
{
    static int id;

    [MenuItem( "Tools/Reload Preloaded" )]
    public static void UpdatePreloaded ()
    {
        var preloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets();
        UnityEditor.PlayerSettings.SetPreloadedAssets( preloadedAssets );
    }

    [MenuItem( "Tools/ClearPrefs" )]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem( "Tools/ScreenShot" )]
    public static void TakeShot ()
    {
        string name = "screenshot " + id + ".png";

        string dataPath = Application.dataPath;

        dataPath = dataPath.Remove( dataPath.Length - "Assets".Length , "Assets".Length );

        while( File.Exists( dataPath + name ))
        {
            id++;
            name = "screenshot " + id + ".jpg";
        }

        ScreenCapture.CaptureScreenshot( name );
    }

    //[MenuItem( "Assets/Create/Language Asset" )]
    //public static void Run ()
    //{
    //    Create<LanguageAsset>();
    //}

    [MenuItem( "Assets/Create/Json" )]
    public static void JSON ()
    {
        string path = AssetDatabase.GetAssetPath (Selection.activeObject);
        if ( path == "" )
        {
            path = "Assets";
        }
        else if ( Path.GetExtension( path ) != "" )
        {
            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/NewJSON".ToString() + ".json");

        print( assetPathAndName );

        string dataPath = Application.dataPath;

        dataPath = dataPath.Remove( dataPath.Length - "Assets".Length , "Assets".Length );

        print( dataPath + assetPathAndName );

        File.Create( dataPath + assetPathAndName );

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        //AssetDatabase.CreateAsset( new TextAsset (), assetPathAndName );
    }

    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void Create<T> () where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T> ();

        string path = AssetDatabase.GetAssetPath (Selection.activeObject);
        if ( path == "" )
        {
            path = "Assets";
        }
        else if ( Path.GetExtension( path ) != "" )
        {
            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset( asset , assetPathAndName );

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
