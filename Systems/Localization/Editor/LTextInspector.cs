using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UtilityScripts
{
    [CustomEditor( typeof( LText ) )]
    public class LTextInspector : Editor
    {
        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector();
        }
    }
}
