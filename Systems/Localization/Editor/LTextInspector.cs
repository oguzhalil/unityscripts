using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor( typeof( LText ) )]
public class LTextInspector : Editor
{
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();
    }
}
