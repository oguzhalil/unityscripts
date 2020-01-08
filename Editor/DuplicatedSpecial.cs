using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DuplicateSpecial : EditorWindow
{

    enum SnapTo
    {
        BoundingBox = 0,
        BoundingSphere = 1
    }

    enum Space
    {
        Local,
        Global
    }

    enum Direction
    {
        NegativeX = -1,
        PositiveX = 1
    }

    private SnapTo around;
    private Space space;
    private Direction dir;
    private Vector3 direction;

    [MenuItem( "Tools/Windows/Duplicate Special" )]
    public static void ShowWindow ()
    {
        EditorWindow.GetWindow( typeof( DuplicateSpecial ) );
    }
    private Bounds bounds;

    void OnGUI ()
    {
        EditorGUILayout.LabelField( "Duplicate Special" , EditorStyles.boldLabel );
        around = ( SnapTo ) EditorGUILayout.EnumPopup( "Operation" , around );
        space = ( Space ) EditorGUILayout.EnumPopup( "Space" , space );
        dir = ( Direction ) EditorGUILayout.EnumPopup( "Direction" , dir );

        if ( GUILayout.Button( "Duplicate" ) )
        {
            if ( Selection.activeGameObject )
            {
                Transform selection = Selection.activeGameObject.transform;

                if ( space == Space.Global )
                {
                    int index = Mathf.Abs( ( ( int ) dir ) ) - 1;

                    bounds = new Bounds( selection.position , Vector3.zero );

                    MeshRenderer [] renderers = selection.GetComponentsInChildren<MeshRenderer>();

                    foreach ( var renderer in renderers )
                    {
                        bounds.Encapsulate( renderer.bounds );
                    }

                    float f = bounds.extents.x * 2;
                    Vector3 vDir = indexToDirection( index );
                    Debug.Log( f );
                    Vector3 position = selection.position + vDir * f;
                    GameObject go = Instantiate( selection.gameObject , position, selection.rotation );
                    Selection.activeGameObject = go;
                }
                else if ( space == Space.Local )
                {

                }

            }
            else
            {
                Debug.LogError( "DuplicateSpecial : Selected object is null." );
            }
        }
    }

    public Vector3 indexToDirection ( int index )
    {
        switch ( index )
        {
            case 0:
                return Vector3.right;
            case 1:
                return Vector3.up;
            case 2:
                return Vector3.forward;
            default:
                return Vector3.zero;
        }
    }

    //// Window has been selected
    //void OnFocus ()
    //{
    //    // Remove delegate listener if it has previously
    //    // been assigned.
    //    SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    //    // Add (or re-add) the delegate.
    //    SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    //}

    //void OnDestroy ()
    //{
    //    // When the window is destroyed, remove the delegate
    //    // so that it will no longer do any drawing.
    //    SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    //}

    //void OnSceneGUI ( SceneView sceneView )
    //{
    //    Handles.BeginGUI();
    //    Handles.DrawCube( 0 , bounds.center , Quaternion.identity , bounds.size.magnitude );
    //    // Do your drawing here using GUI.
    //    Handles.EndGUI();

    //}
}
