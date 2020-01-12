using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        PositiveX = 1,
        NegativeY = -2,
        PositiveY = 2,
        NegativeZ = -3,
        PositiveZ = 3,
    }

    private SnapTo around = SnapTo.BoundingBox;
    private Space space = Space.Global;
    private Direction dir = Direction.NegativeX;
    private int count;
    public bool useEpsilon;

    [MenuItem( "Tools/Windows/Duplicate Special" )]
    public static void ShowWindow ()
    {
        EditorWindow.GetWindow( typeof( DuplicateSpecial ) );
    }

    private Bounds bounds;
    private GameObject referenceObject;

    void OnGUI ()
    {
        EditorGUILayout.LabelField( "Duplicate Special" , EditorStyles.boldLabel );
        around = ( SnapTo ) EditorGUILayout.EnumPopup( "Operation" , around );

        EditorGUI.BeginChangeCheck();
        space = ( Space ) EditorGUILayout.EnumPopup( "Space" , space );
        dir = ( Direction ) EditorGUILayout.EnumPopup( "Direction" , dir );
        useEpsilon = EditorGUILayout.Toggle( "Use Epsilon" , useEpsilon );
        if ( EditorGUI.EndChangeCheck() )
        {
            SceneView.RepaintAll();
        }

        count = EditorGUILayout.IntField( "Count" , count );

        if ( GUILayout.Button( "Duplicate" ) )
        {
            if ( Selection.activeGameObject )
            {
                Transform selection = Selection.activeGameObject.transform;
                referenceObject = selection.gameObject;

                if ( space == Space.Global )
                {

                    int index = Mathf.Abs( ( int ) dir );

                    Vector3 vDir = indexToDirection( index );
                    bounds = new Bounds( selection.position , Vector3.zero );
                    MeshRenderer [] renderers = selection.GetComponentsInChildren<MeshRenderer>();
                    foreach ( var renderer in renderers )
                    {
                        bounds.Encapsulate( renderer.bounds );
                    }
                    float f = bounds.extents[ Mathf.Abs( (int)dir) - 1 ] * 2;

                    if(useEpsilon)
                    {
                        f -= Mathf.Epsilon;
                    }

                    for ( int i = 1; i <= count; i++ )
                    {
                        Vector3 position = selection.position + vDir * f * i;

                        if ( PrefabUtility.IsPartOfAnyPrefab( selection.gameObject ) )
                        {
                            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab( AssetDatabase.LoadAssetAtPath(
                               PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot( Selection.activeObject ) , typeof( GameObject ) ) , selection.parent );
                            go.transform.position = position;
                            go.transform.rotation = selection.rotation;
                            go.transform.localScale = selection.localScale;
                        }
                        else
                        {
                            GameObject go = Instantiate( selection.gameObject , position , selection.rotation );
                            go.transform.SetParent( selection.parent , true );
                            go.transform.localScale = selection.localScale;
                        }

                    }
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
            case 1:
                return Vector3.right * Mathf.Sign( ( int ) dir );
            case 2:
                return Vector3.up * Mathf.Sign( ( int ) dir );
            case 3:
                return Vector3.forward * Mathf.Sign( ( int ) dir );
            default:
                return Vector3.zero * Mathf.Sign( ( int ) dir );
        }
    }

    // Window has been selected
    void OnFocus ()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        // Add (or re-add) the delegate.
        SceneView.duringSceneGui -= this.OnSceneGUI;
        SceneView.duringSceneGui += this.OnSceneGUI;
    }

    void OnDestroy ()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.duringSceneGui -= this.OnSceneGUI;
    }

    void OnSceneGUI ( SceneView sceneView )
    {
        if ( Event.current.type == EventType.Repaint )
        {
            if ( Selection.activeGameObject && Selection.activeGameObject.GetComponentsInChildren<MeshRenderer>().Length > 0)
            {
                var vector = indexToDirection( Mathf.Abs( ( int ) dir ) );
                //Debug.Log( vector );
                Handles.color = Color.white;
                MeshRenderer [] renderers = Selection.activeGameObject.GetComponentsInChildren<MeshRenderer>();
                Bounds bounds = new Bounds( Selection.activeGameObject.transform.position , Vector3.zero );
                foreach ( var renderer in renderers )
                {
                    bounds.Encapsulate( renderer.bounds );
                }

                Vector3 pos = bounds.center + Vector3.up * 3.0f;
                Handles.ArrowHandleCap( 0 , pos , Quaternion.FromToRotation( Vector3.forward , vector ) , 3.0f , EventType.Repaint );
                //Handles.ArrowHandleCap( 0 , Selection.activeGameObject.transform.position , Quaternion.LookRotation( -Vector3.right , Vector3.up ) , 1f , EventType.Repaint );

            }
        }
    }
}
