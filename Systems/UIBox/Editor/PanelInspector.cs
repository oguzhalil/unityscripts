using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( Panel ) )]
public class PanelInspector : Editor
{
    public override void OnInspectorGUI ()
    {
        EditorGUILayout.PropertyField( serializedObject.FindProperty( "id" ) );

        DrawExposeElement( serializedObject.FindProperty( "elems" ) );


        serializedObject.ApplyModifiedProperties();

        if ( GUILayout.Button( "Update") )
        {
            Panel panel = target as Panel;
            panel.elems = panel.GetComponentsInChildren<UIElem>( true );
        }
    }

    void DrawExposeElement ( SerializedProperty list )
    {

        for ( int i = 0; i < list.arraySize; i++ )
        {
            EditorGUI.indentLevel += 1;

            SerializedProperty exposeElement = list.GetArrayElementAtIndex(i);

            EditorGUILayout.PropertyField( exposeElement );

            EditorGUI.indentLevel += 1;

            SerializedObject soExpose = new SerializedObject( exposeElement.objectReferenceValue );

            SerializedProperty id = soExpose.FindProperty("id");

            EditorGUILayout.PropertyField( id );

            soExpose.ApplyModifiedProperties();
            //ExposeElement mono = exposeElement.objectReferenceValue as ExposeElement;

            //EditorGUILayout.LabelField("id : " , mono.id );

            EditorGUI.indentLevel -= 1;

            EditorGUI.indentLevel -= 1;
        }


    }
}
