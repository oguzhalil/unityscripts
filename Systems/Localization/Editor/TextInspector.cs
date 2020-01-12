using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Reflection;
using System;

[CustomEditor( typeof( Text ) )]
public class TextInspector : DecoratorEditor
{
    public TextInspector() : base("TextInspector")
    {

    }

    public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI();

        GUILayout.Button( "Adding this button" );
    }

    ////Unity's built-in editor
    //Editor defaultEditor;
    //Transform transform;

    ////void OnEnable ()
    ////{
    ////    //When this inspector is created, also create the built-in inspector
    ////    defaultEditor = Editor.CreateEditor( targets , Type.GetType( "UnityEditor.TransformInspector, UnityEditor" ) );
    ////    transform = target as Transform;
    ////}

    ////void OnDisable ()
    ////{
    ////    //When OnDisable is called, the default editor we created should be destroyed to avoid memory leakage.
    ////    //Also, make sure to call any required methods like OnDisable
    ////    MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    ////    if ( disableMethod != null )
    ////        disableMethod.Invoke( defaultEditor , null );
    ////    DestroyImmediate( defaultEditor );
    ////}

    //public override void OnInspectorGUI ()
    //{
    //    base.OnInspectorGUI();
    //    EditorGUILayout.LabelField( "Local Space" , EditorStyles.boldLabel );
    //    //defaultEditor.OnInspectorGUI();

    //    //return;

    //    //EditorGUILayout.LabelField( "Local Space" , EditorStyles.boldLabel );
    //    //Text text = (target as Text);

    //    //if ( GUILayout.Button( "Convert Localization Text" ) )
    //    //{
    //    //    Text copy = GetCopyOf<Text>(text , text );

    //    //    Debug.Log( copy.font.name );
    //    //}

    //    //DrawDefaultInspector();


    //}

    //public static T GetCopyOf<T> ( MonoBehaviour Monobehaviour , T Source ) where T : MonoBehaviour
    //{
    //    //Type check
    //    Type type = Monobehaviour.GetType();
    //    if ( type != Source.GetType() ) return null;

    //    //Declare Binding Flags
    //    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

    //    //Iterate through all types until monobehaviour is reached
    //    while ( type != typeof( MonoBehaviour ) )
    //    {
    //        //Apply Fields
    //        FieldInfo[] fields = type.GetFields(flags);
    //        foreach ( FieldInfo field in fields )
    //        {
    //            field.SetValue( Monobehaviour , field.GetValue( Source ) );
    //        }

    //        //Move to base class
    //        type = type.BaseType;
    //    }
    //    return Monobehaviour as T;
    //}
}
/*
 * [CustomEditor (typeof (RemoveComponentName))]
public class RemoveComponentNameEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();

		RemoveComponentName rmc = (RemoveComponentName) target;

		if (GUILayout.Button ("Remove ComponentName")) {
			rmc.RemoveComponents ();
		}
	}
}
 
     */
