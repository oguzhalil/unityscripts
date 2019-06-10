//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.IO;

//public class WeaponEditor : EditorWindow
//{
//    [MenuItem( "Window/Weapon" )]

//    public static void ShowWindow ()
//    {
//        EditorWindow.GetWindow( typeof( WeaponEditor ) );
//    }


//}

//public static class Create
//{
//    [MenuItem( "Assets/Create/CreateWeapon" )]
//    public static void CreateAsset ()
//    {
//        CreateAsset<SOCWeapon>();
//    }

//    public static void CreateAsset<T> () where T : ScriptableObject
//    {
//        T asset = ScriptableObject.CreateInstance<T>();

//        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
//        if ( path == "" )
//        {
//            path = "Assets";
//        }
//        else if ( Path.GetExtension( path ) != "" )
//        {
//            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
//        }

//        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

//        AssetDatabase.CreateAsset( asset , assetPathAndName );

//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }
//}
