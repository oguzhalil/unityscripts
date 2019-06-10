//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


//[CustomEditor( typeof( Page ) )]
//public class PageInspector : Editor
//{
//    public override void OnInspectorGUI ()
//    {
//        //base.OnInspectorGUI();

//        EditorPage.Show( serializedObject.FindProperty( "panels" ) , true);

//        EditorGUILayout.PropertyField( serializedObject.FindProperty( "sprBackground" ) );

//        EditorGUILayout.PropertyField( serializedObject.FindProperty( "panelDefault" ) );

//        serializedObject.ApplyModifiedProperties();

//        if ( GUILayout.Button( "Update" ) )
//        {
//            Page page = target as Page;

//            Panel [] panels = page.GetComponentsInChildren<Panel>(true);

//            page.panels = panels;

//            foreach ( var panel in panels )
//            {
//                panel.exposeElements = panel.GetComponentsInChildren<ExposeElement>(true);
//            }
//        }
//    }
//}
