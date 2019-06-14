using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Util2
{
    [MenuItem( "Tools/Shadow Cast Off" )]
    public static void TransformIntoPickup ()
    {
        var go = Selection.gameObjects;

        foreach ( var item in go )
        {
            if ( item != null )
            {
                var renderer = item.GetComponent<MeshRenderer>();

                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                foreach ( var r in renderer.GetComponentsInChildren<MeshRenderer>() )
                {
                    r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                } 
            }
        }
    }

    //    [MenuItem( "Tools/Set Parent To Root" )]
    //    public static void SetParentToRoot ()
    //    {
    //        var go = Selection.gameObjects;

    //        foreach ( var item in go )
    //        {
    //            if ( item != null )
    //                item.transform.SetParent( GameObject.Find( "HigherVertexCount" ).transform );
    //        }
    //    }

    //    [MenuItem( "Tools/Transform Into Pickup" )]
    //    public static void TransformIntoPickup ()
    //    {
    //        var go = Selection.gameObjects;

    //        foreach ( var item in go )
    //        {
    //            if ( item.tag == PickupObj.tag )
    //                continue;

    //            if ( item != null )
    //            {
    //                GameObject pickupObj = new GameObject();
    //                pickupObj.name = "PickupObj_" + item.name;
    //                pickupObj.AddComponent<PickupObj>();
    //                var collider = pickupObj.AddComponent<SphereCollider>();
    //                collider.isTrigger = true;
    //                pickupObj.tag = PickupObj.tag;

    //                var obj = Object.Instantiate( item ).gameObject;

    //                obj.transform.SetParent( pickupObj.transform );
    //                obj.transform.localPosition = Vector3.zero;

    //                string localPath = "Assets/BattleRoyale/Scripts/" + pickupObj.name + ".prefab";

    //                //Check if the Prefab and/or name already exists at the path
    //                if ( AssetDatabase.LoadAssetAtPath( localPath , typeof( GameObject ) ) )
    //                {
    //                    //Create dialog to ask if User is sure they want to overwrite existing Prefab
    //                    if ( EditorUtility.DisplayDialog( "Are you sure?" ,
    //                        "The Prefab already exists. Do you want to overwrite it?" ,
    //                        "Yes" ,
    //                        "No" ) )
    //                    //If the user presses the yes button, create the Prefab
    //                    {
    //                        CreateNew( pickupObj , localPath );
    //                    }
    //                }
    //                //If the name doesn't exist, create the new Prefab
    //                else
    //                {
    //                    Debug.Log( pickupObj.name + " is not a Prefab, will convert" );
    //                    CreateNew( pickupObj , localPath );
    //                }

    //                Object.DestroyImmediate( pickupObj );
    //                Object.DestroyImmediate( obj );

    //            }
    //        }
    //    }

    //    static void CreateNew ( GameObject obj , string localPath )
    //    {
    //        //Create a new Prefab at the path given
    //        Object prefab = PrefabUtility.CreatePrefab( localPath , obj );
    //        PrefabUtility.ReplacePrefab( obj , prefab , ReplacePrefabOptions.ConnectToPrefab );
    //    }

}
