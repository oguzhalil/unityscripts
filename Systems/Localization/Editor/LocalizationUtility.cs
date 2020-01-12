using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UtilityScripts
{
    public static class LocalizationUtility
    {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAsset<T> () where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath( Selection.activeObject );
            if ( path == "" )
            {
                path = "Assets";
            }
            else if ( Path.GetExtension( path ) != "" )
            {
                path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ) , "" );
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath( path + "/New " + typeof( T ).ToString() + ".asset" );

            AssetDatabase.CreateAsset( asset , assetPathAndName );

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        [MenuItem( "Tools/Create Language Asset" )]
        public static void CreateItems ()
        {
            CreateAsset<Languages>();
        }

        [MenuItem( "Tools/Localization/Text to LocText" )]
        public static void ReplaceLText ()
        {
            GameObject go = Selection.activeGameObject;

            if ( go == null )
                return;

            Text t = go.GetComponent<Text>();


            if ( t == null )
                return;

            Text copy = GetCopyOf( t , t );

            Debug.Log( copy.font.name );

            UnityEngine.Object.DestroyImmediate( go.GetComponent<Text>() );

            LocText newText = go.AddComponent<LocText>();

            // font
            newText.font = copy.font;
            newText.fontSize = copy.fontSize;

            // alignment
            newText.alignment = copy.alignment;

            // overflow
            newText.horizontalOverflow = copy.horizontalOverflow;
            newText.verticalOverflow = copy.verticalOverflow;

            // best fit
            newText.resizeTextForBestFit = copy.resizeTextForBestFit;
            newText.resizeTextMinSize = copy.resizeTextMinSize;
            newText.resizeTextMaxSize = copy.resizeTextMaxSize;

            // color
            newText.color = copy.color;

            //text
            newText.text = copy.text;

            Debug.Log( $"Localization : Default text { go.name } replaced by localized text." );
        }

        [MenuItem( "Tools/Localization/LocText to Text" )]
        public static void ReplaceText ()
        {
            GameObject go = Selection.activeGameObject;

            if ( go == null )
                return;

            LocText t = go.GetComponent<LocText>();


            if ( t == null )
                return;

            LocText copy = GetCopyOf( t , t );

            Debug.Log( copy.font.name );

            UnityEngine.Object.DestroyImmediate( go.GetComponent<LocText>() );

            Text newText = go.AddComponent<Text>();

            // font
            newText.font = copy.font;
            newText.fontSize = copy.fontSize;

            // alignment
            newText.alignment = copy.alignment;

            // overflow
            newText.horizontalOverflow = copy.horizontalOverflow;
            newText.verticalOverflow = copy.verticalOverflow;

            // best fit
            newText.resizeTextForBestFit = copy.resizeTextForBestFit;
            newText.resizeTextMinSize = copy.resizeTextMinSize;
            newText.resizeTextMaxSize = copy.resizeTextMaxSize;

            // color
            newText.color = copy.color;

            //text
            newText.text = copy.text;

            Debug.Log( $"Localization : Localized text { go.name } replaced by default text" );

        }

        public static T GetCopyOf<T> ( MonoBehaviour Monobehaviour , T Source ) where T : MonoBehaviour
        {
            //Type check
            Type type = Monobehaviour.GetType();
            if ( type != Source.GetType() ) return null;

            //Declare Binding Flags
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

            //Iterate through all types until monobehaviour is reached
            while ( type != typeof( MonoBehaviour ) )
            {
                //Apply Fields
                FieldInfo [] fields = type.GetFields( flags );
                foreach ( FieldInfo field in fields )
                {
                    field.SetValue( Monobehaviour , field.GetValue( Source ) );
                }

                //Move to base class
                type = type.BaseType;
            }
            return Monobehaviour as T;
        }
    }
}