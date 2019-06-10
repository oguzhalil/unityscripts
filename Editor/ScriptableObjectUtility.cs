//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;

//public static class ScriptableObjectUtility
//{
//    static int ss_id = 0;

//    [MenuItem( "Assets/Create/CreateBall" )]
//    public static void CreateBall ()
//    {
//        CreateAsset<Ball>();
//    }

//    [MenuItem("Assets/Create/CreateCloth")]
//    public static void CreateCloth()
//    {
//        CreateAsset<Cloth>();
//    }

//    [MenuItem( "Assets/Create/CreateAccessory" )]
//    public static void CreateAccessory ()
//    {
//        CreateAsset<Accessory>();
//    }

//    [MenuItem( "Tools/Run Windows Build" )]
//    public static void RunWindowsBuild ()
//    {
//        Process foo = new Process();
//        foo.StartInfo.FileName = @"C:\Clients\TurgayYilmaz\BasketballShooter\Builds\run.bat";
//        //foo.StartInfo.Arguments = "put your arguments here";
//        foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
//        foo.Start();
//    }

//    [MenuItem( "Tools/DisableChildsRaycasters" )]
//    public static void DisableRaycasters()
//    {
//        GameObject go = Selection.activeGameObject;

//        if ( go == null )
//            return;

//        foreach ( Transform child in go.transform )
//        {
//            var components = child.GetComponentsInChildren<Graphic>(true);

//            foreach ( var component in components )
//            {
//                component.raycastTarget = false;
//            }
//        }
//    }

//    /// <summary>
//    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
//    /// </summary>
//    public static void CreateAsset<T>() where T : ScriptableObject
//    {
//        T asset = ScriptableObject.CreateInstance<T>();

//        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
//        if (path == "")
//        {
//            path = "Assets";
//        }
//        else if (Path.GetExtension(path) != "")
//        {
//            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
//        }

//        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

//        AssetDatabase.CreateAsset(asset, assetPathAndName);

//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    //[MenuItem("Tools/CreateItems")]
//    //public static void CreateItems()
//    //{
//    //    CreateAsset<Items>();
//    //}

//    [MenuItem("Tools/ScreenShot")]
//    public static void TakeScreenShot()
//    {
//        ScreenCapture.CaptureScreenshot("ss_id" + ++ss_id + ".png");

//    }
//}