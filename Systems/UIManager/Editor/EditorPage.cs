using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorPage
{
    public static void Show ( SerializedProperty list , bool showListSize = false )
    {
        EditorGUILayout.PropertyField( list );
        EditorGUI.indentLevel += 1;

        // if(list.isExpanded)
        // EditorGUILayout.PropertyField( list.FindPropertyRelative( "Array.size" ) );
        if ( list.isExpanded )
        {
            if ( showListSize )
                EditorGUILayout.PropertyField( list.FindPropertyRelative( "Array.size" ) );

            for ( int i = 0; i < list.arraySize; i++ )
            {

                var panel = list.GetArrayElementAtIndex( i );

                Panel p =panel.objectReferenceValue as Panel;

                EditorGUI.indentLevel += 1;


                EditorGUILayout.PropertyField( panel );

                EditorGUILayout.LabelField( p.id );

                EditorGUI.indentLevel -= 1;


                //var exposures = new SerializedObject( panel.objectReferenceValue ) .FindProperty( "exposeElements" );

                //EditorGUI.indentLevel += 1;

                //for ( int j = 0; j < exposures.arraySize; j++ )
                //{
                //    var exposure = exposures.GetArrayElementAtIndex(j);


                //    EditorGUILayout.PropertyField( exposure );

                //}

                //EditorGUI.indentLevel -= 1;


                //var r = new SerializedObject( v ).FindProperty( "exposeElements" );
                //var z = new SerializedObject( r.objectReferenceValue ).FindProperty ( "comp" );

                //    //.FindProperty("exposeElements");
                ////list.GetArrayElementAtIndex( i ).serializedObject.ApplyModifiedProperties();

                //if ( r != null )
                //    for ( int j = 0; j < r.arraySize; j++ )
                //    {
                //        var property = r.GetArrayElementAtIndex( j );
                //        if ( property == null )
                //            continue;

                //        EditorGUILayout.PropertyField( property );

                //    }
                //else
                //    EditorGUILayout.PropertyField( list.GetArrayElementAtIndex( i ) );
            }
        }


        EditorGUI.indentLevel -= 1;
    }
}
