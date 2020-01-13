﻿using System;
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
    private bool randomizeYRotation;
    private bool randomizeScale;
    private bool randomizeSpacing;
    private float radius;
    private List<GameObject> instantiatedObjects;

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
        randomizeYRotation = EditorGUILayout.Toggle( "Randomize Y Rotation" , randomizeYRotation );
        randomizeScale = EditorGUILayout.Toggle( "Randomize Scale" , randomizeScale );
        randomizeSpacing = EditorGUILayout.Toggle( "Randomize Spacing" , randomizeSpacing );
        if ( randomizeSpacing )
        {
            radius = EditorGUILayout.FloatField( "Radius" , radius );
        }

        if ( EditorGUI.EndChangeCheck() )
        {
            SceneView.RepaintAll();
        }

        count = EditorGUILayout.IntField( "Count" , count );

        if ( GUILayout.Button( "Duplicate" ) )
        {
            if ( Selection.activeGameObject )
            {
                instantiatedObjects.Clear();
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
                    float f = bounds.extents [ Mathf.Abs( ( int ) dir ) - 1 ] * 2;

                    if ( useEpsilon )
                    {
                        f -= Mathf.Epsilon;
                    }

                    for ( int i = 1; i <= count; i++ )
                    {
                        Vector3 position = selection.position + vDir * f * i;
                        Quaternion rotation = selection.rotation;
                        Vector3 scale = selection.localScale;

                        if ( randomizeSpacing )
                        {
                            position += vDir * radius;
                            Vector2 rndCircle = UnityEngine.Random.insideUnitCircle * radius;
                            position += new Vector3( rndCircle.x , 0f , rndCircle.y );
                        }

                        if ( randomizeYRotation )
                        {
                            rotation = Quaternion.AngleAxis( UnityEngine.Random.value * 360f , Vector3.up ) * Quaternion.AngleAxis( UnityEngine.Random.value * 10f , Vector3.right ) * Quaternion.AngleAxis( UnityEngine.Random.value * 10f , Vector3.forward );
                        }
                        if ( randomizeScale )
                        {
                            scale = new Vector3( UnityEngine.Random.Range( 1f , 1.5f ) , UnityEngine.Random.Range( 1f , 1.5f ) , UnityEngine.Random.Range( 1f , 1.5f ) );
                        }

                        if ( PrefabUtility.IsPartOfAnyPrefab( selection.gameObject ) )
                        {
                            GameObject go = ( GameObject ) PrefabUtility.InstantiatePrefab( AssetDatabase.LoadAssetAtPath(
                               PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot( Selection.activeObject ) , typeof( GameObject ) ) , selection.parent );
                            go.transform.position = position;
                            go.transform.rotation = rotation;
                            go.transform.localScale = scale;
                            instantiatedObjects.Add( go );
                        }
                        else
                        {
                            GameObject go = Instantiate( selection.gameObject , position , rotation );
                            go.transform.SetParent( selection.parent , true );
                            go.transform.localScale = scale;
                            instantiatedObjects.Add( go );
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

        if(GUILayout.Button("Undo"))
        {
            instantiatedObjects.ForEach( x => UnityEngine.Object.DestroyImmediate( x ) );
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
            if ( Selection.activeGameObject && Selection.activeGameObject.GetComponentsInChildren<MeshRenderer>().Length > 0 )
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
                Handles.CircleHandleCap( 0 , Selection.activeGameObject.transform.position , Quaternion.AngleAxis(-90f,Vector3.right) , radius , EventType.Repaint );
                Vector3 pos = bounds.center + Vector3.up * 3.0f;
                Handles.ArrowHandleCap( 0 , pos , Quaternion.FromToRotation( Vector3.forward , vector ) , 3.0f , EventType.Repaint );
                //Handles.ArrowHandleCap( 0 , Selection.activeGameObject.transform.position , Quaternion.LookRotation( -Vector3.right , Vector3.up ) , 1f , EventType.Repaint );

            }
        }
    }
}
