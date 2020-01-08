using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class EditorUtilities : MonoBehaviour
{

    [MenuItem( "Tools/Commands/Reload Preloaded Assets" )]
    public static void UpdatePreloaded ()
    {
        var preloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets();
        UnityEditor.PlayerSettings.SetPreloadedAssets( preloadedAssets );
        Debug.Log( $"Command - UpdatePreloaded : Preloaded assets updated." );
    }

    [MenuItem( "Tools/Commands/Clear All Preferences" )]
    public static void ClearPrefs ()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log( $"Command - ClearPrefs: All Preferences is deleted." );
    }

    private static int id;

    [MenuItem( "Tools/Commands/Take Screen Shot" )]
    public static void TakeScreenShot ()
    {
        string name = $"{PlayerSettings.productName.ToLower()}_screenshot_{id}.png";
        string dataPath = Application.dataPath;
        dataPath = dataPath.Remove( dataPath.Length - "Assets".Length , "Assets".Length );
        while ( File.Exists( dataPath + name ) )
        {
            id++;
            name = $"{PlayerSettings.productName.ToLower()}_screenshot_{id}.png";
        }
        ScreenCapture.CaptureScreenshot( name );
        Debug.Log( $"Command - TakeScreenShot: Screenshot saved at {dataPath} with name {name}." );
    }

    [MenuItem( "Assets/Create/JSON File" )]
    public static void JSON ()
    {
        string path = AssetDatabase.GetAssetPath( Selection.activeObject );
        if ( path == "" )
        {
            path = "Assets";
        }
        else if ( Path.GetExtension( path ) != "" )
        {
            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath( path + "/NewJSON".ToString() + ".json" );
        string dataPath = Application.dataPath;
        dataPath = dataPath.Remove( dataPath.Length - "Assets".Length , "Assets".Length );
        File.Create( dataPath + assetPathAndName );
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        //AssetDatabase.CreateAsset( new TextAsset (), assetPathAndName );
        Debug.Log( $"Command - Create JSON: Json created at {dataPath} with name {AssetDatabase.GetMainAssetTypeAtPath( assetPathAndName ).Name}." );
    }

    [MenuItem( "Tools/Command/Run Last Windows Build" )]
    public static void RunWindowsBuild ()
    {
        Process foo = new Process();
        foo.StartInfo.FileName = @"C:\Clients\TurgayYilmaz\BasketballShooter\Builds\run.bat";
        foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        foo.Start();
    }

    [MenuItem( "Tools/Command/Disable All Childs Raycasters" )]
    public static void DisableRaycasters ()
    {
        GameObject go = Selection.activeGameObject;

        if ( go == null )
            return;
        int count = 0;

        foreach ( Transform child in go.transform )
        {
            var components = child.GetComponentsInChildren<Graphic>( true );

            foreach ( var component in components )
            {
                component.raycastTarget = false;
                count++;
            }
        }

        Debug.Log( $"Command - Disable Raycasters: {count} raycast disabled." );
    }

    [MenuItem( "Tools/Commands/Disable All Shadow Cast" )]
    public static void TransformIntoPickup ()
    {
        var go = Selection.gameObjects;
        int count = 0;
        foreach ( var item in go )
        {
            if ( item != null )
            {
                var renderer = item.GetComponent<MeshRenderer>();
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                count++;
                foreach ( var r in renderer.GetComponentsInChildren<MeshRenderer>() )
                {
                    r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    count++;
                }
            }
        }

        Debug.Log( $"Command - Disable ShadowCasts: {count} shadows cast disabled." );
    }

    [MenuItem( "Tools/Commands/Unparent 1 Depth" )]
    public static void Unparent ()
    {
        var go = Selection.gameObjects;
        int count = 0;
        foreach ( var item in go )
        {
            if ( item != null )
            {
                item.transform.SetParent( item.transform.parent.parent );
                count++;
            }
        }

        Debug.Log( $"Command - Unparent: {count} gameobjects unparented." );
    }


    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void Create<T> () where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath( Selection.activeObject );
        if ( path == "" )
        {
            path = "Assets";
        }
        else if ( Path.GetExtension( path ) != "" )
        {
            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath( path + "/New " + typeof( T ).ToString() + ".asset" );

        AssetDatabase.CreateAsset( asset , assetPathAndName );

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
