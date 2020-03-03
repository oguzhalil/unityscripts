using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class EnumComponent : MonoBehaviour
{
    [HideInInspector] public int enumAsInt;
    [HideInInspector] public int enumTypeAsInt = 0;

    public T GetValue<T> ()
    {
        return ( T ) ( object ) enumAsInt;
    }

    public int GetValue ()
    {
        return enumAsInt;
    }

#if UNITY_EDITOR
    [CustomEditor( typeof( EnumComponent ) )]
    public class EnumComponentEditor : Editor
    {
        private EnumComponent enumComponent;
        private string [] options = new string [] { "Cube" , "Sphere" , "Plane" };
        private List<Type> enums;
        private void OnEnable ()
        {
            enumComponent = target as EnumComponent;
            UpdateEnums();
        }

        private void UpdateEnums ()
        {
            enums = FindAllEnums();
            options = new string [ enums.Count ];
            for ( int i = 0; i < options.Length; i++ )
            {
                options [ i ] = enums [ i ].Name;
            }

            if ( enums.Count == 0 )
            {
                options = new string [] { "NO_ENUM_FOUND!" };
            }
        }

        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector();
            EditorGUI.BeginChangeCheck();
            enumComponent.enumTypeAsInt = EditorGUILayout.Popup( new GUIContent( "Enum Type" ) , enumComponent.enumTypeAsInt , options );
            if ( EditorGUI.EndChangeCheck() )
            {
                UpdateEnums();
            }

            if ( enums == null || enums.Count == 0 )
            {
                return;
            }

            var enumType = ( Enum ) Enum.ToObject( enums [ enumComponent.enumTypeAsInt ] , enumComponent.enumAsInt );
            enumType = EditorGUILayout.EnumPopup( new GUIContent( "Enum Value" ) , enumType );
            enumComponent.enumAsInt = Convert.ToInt32( enumType );

            serializedObject.ApplyModifiedProperties();
        }

        public static List<Type> FindAllEnums ()
        {
            List<Type> enumTypes = new List<Type>( 100 );
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            for ( int i = 0; i < assemblies.Length; i++ )
            {
                var types = assemblies [ i ].GetTypes();
                for ( int n = 0; n < types.Length; n++ )
                {
                    //if ( types [ n ].IsEnum && Marshal.SizeOf( Enum.GetUnderlyingType( types [ n ] ) ) == 1 ) // is short
                    if ( types [ n ].IsEnum && types [ n ].Name.Contains( "_Enum" ) ) // is short
                    {
                        enumTypes.Add( types [ n ] );
                    }
                    //if ( typeof( UnityEngine.Object ).IsAssignableFrom( types [ n ] ) && aClassName == types [ n ].Name )
                    //    return UnityEngine.Object.FindObjectsOfType( types [ n ] );
                }
            }

            return enumTypes;
        }
    }
#endif
}
