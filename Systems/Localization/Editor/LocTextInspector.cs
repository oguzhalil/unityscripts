using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UtilityScripts
{
    [CustomEditor( typeof( LocText ) )]
    public class LocTextInspector : Editor
    {
        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector();
        }
    }
}
