//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor( typeof( RenderQueueManager ) )]
//public class RenderQueueEditor : Editor
//{
//    public SerializedProperty m_IncludeInActive;

//    RenderQueueManager m_RenderQueueManager;

//    public override void OnInspectorGUI ()
//    {
//        m_IncludeInActive = serializedObject.FindProperty( "m_IncludeInActive" );

//        EditorGUILayout.PropertyField( m_IncludeInActive );

//        serializedObject.ApplyModifiedProperties();

//        if ( GUILayout.Button( "Find All Materials In Scene" ) )
//        {
//            m_RenderQueueManager.FindAllMaterialsInScene( m_IncludeInActive.boolValue );
//        }

//        //DrawDefaultInspector();

//    }
//}