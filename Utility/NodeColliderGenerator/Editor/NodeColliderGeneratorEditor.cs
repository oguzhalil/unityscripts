﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CanEditMultipleObjects]
[CustomEditor( typeof( NodeColliderGenerator  )  )]
public class NodeColliderGeneratorEditor : Editor
{
    static NodeColliderGenerator nodeColliderGenerator;

    private void OnEnable ()
    {
        nodeColliderGenerator = target as NodeColliderGenerator;
    }


    [MenuItem("Tools/Collider Generator/Add Collider %e")]
    public static void GenerateShortcut()
    {
        if( nodeColliderGenerator != null )
        {
            nodeColliderGenerator.AddNode();
        }
    }

    public override void OnInspectorGUI ()
    {
        var serialization = serializedObject.FindProperty( "serialization" );

        EditorGUILayout.BeginHorizontal();
        if ( GUILayout.Button( "Add" ) )
        {
            nodeColliderGenerator.AddNode();
        }

        if ( GUILayout.Button( "Generate" ) )
        {
            nodeColliderGenerator.Generate();
        }

        if ( GUILayout.Button( "Clear" ) )
        {
            if ( EditorUtility.DisplayDialog( "Delete All Nodes?" , "All nodes gonna be deleted \nAre you sure?" , "Yes" , "No" ) )
            {
                nodeColliderGenerator.Clear();

            }
        }

        if ( GUILayout.Button( "ReArrange" ) )
        {
                nodeColliderGenerator.ReArrange();
        }
        EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField( serializedObject.FindProperty( "width" ) );
            EditorGUILayout.PropertyField( serializedObject.FindProperty( "height" ) );
        EditorGUILayout.PropertyField( serialization );

        if(targets.Length == 1)
        {


        for ( int i = 0; i < serialization.arraySize; i++ )
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField( i.ToString()  , GUILayout.Width(25));

            EditorGUILayout.PropertyField( serialization.GetArrayElementAtIndex( i ) , GUIContent.none);

            if (GUILayout.Button("Add Before"))
            {
                nodeColliderGenerator.AddBefore(i);
            }

            if ( GUILayout.Button( "Add After" ) )
            {
                nodeColliderGenerator.AddAfter( i );
            }

            if ( GUILayout.Button( "Remove" ) )
            {
                nodeColliderGenerator.Remove(i);
            }

            EditorGUILayout.EndHorizontal();
        }

            //EditorGUILayout.PropertyField( serializedObject.FindProperty( "serialization" ) , true );

        }


        serializedObject.ApplyModifiedProperties();

    }
}
