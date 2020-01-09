using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class InputWindow : EditorWindow
{
    public string input;
    public Action<string> delegateInput;

    private void OnGUI ()
    {
        EditorGUILayout.BeginVertical();

        input = EditorGUILayout.TextField( input );

        if ( GUILayout.Button( "OKAY" ) )
        {
            delegateInput.SafeInvoke( input );
            delegateInput = null;
            Close();
        }
        EditorGUILayout.EndVertical();
    }

    public static void Activate()
    {
        //EditorUtility.DisplayPopupMenu( new Rect( Screen.width , Screen.height , 200 , 200 ) , "/Assets" , null );
    }

    public static void ShowWindow (string title , Action<string> action)
    {
        var window = (InputWindow)EditorWindow.GetWindow( typeof( InputWindow ) , true ,title );
        window.delegateInput = action;
        float height = 50;
        window.ShowAsDropDown( new Rect( Screen.currentResolution.width / 2 - 100 , Screen.currentResolution.height / 2 - (height * .5f) , 0 , 0 ) , new Vector2( 200 , height) );
        //window.position = new Rect( Screen.currentResolution.width / 2 , Screen.currentResolution.height / 2 , 100 , 100 );
        //window.ShowAsDropDown( new Rect( Screen.width / 2 , Screen.height / 2 , 200 , 200 ) , new Vector2( 200 , 200 ) );
        //window.show
        //window.ShowPopup();
        //window.ShowModalUtility();
    }

    void OnDestroy()
    {
        delegateInput.SafeInvoke( string.Empty );
        delegateInput = null;
    }
}
