using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;

namespace UtilityScripts
{
    // IngredientDrawerUIE
    [CustomPropertyDrawer( typeof( EnumType ) )]
    public class EnumMonoPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
        {
            EditorGUI.BeginProperty( position , label , property );

            Color oldGuiColor = GUI.color;
            GUI.color = Color.white;
            var skin = new GUIStyle( GUI.skin.box );
            skin.fontSize = 10;
            skin.fontStyle = FontStyle.Normal;
            skin.fixedHeight = 5;
            skin.fixedWidth = position.width;
            skin.contentOffset = Vector2.zero;
            //position.y += skin.fixedHeight * .5f;

            

            Rect enumRect = new Rect( position.x , position.y , position.width , 16 );
            SerializedProperty enumProperty = property.FindPropertyRelative( "enumAsInt" );
            Enum enumAsType = (Enum)Enum.Parse( ( attribute as EnumType ).type , enumProperty.intValue.ToString() );
            enumAsType = EditorGUI.EnumPopup( enumRect , new GUIContent( "Enum " ) , enumAsType );
            enumProperty.intValue = Convert.ToInt32( enumAsType );

            Rect objRect = new Rect( position.x , position.y + 18, position.width , 16 );
            EditorGUI.PropertyField( objRect , property.FindPropertyRelative( "_object" ) , new GUIContent("Page Object ") );

            Rect enumObjRect = new Rect( position.x , position.y + 36 , position.width , 5 );
            //EditorGUI.LabelField( enumObjRect , "EnumObject " + ( label.text [ label.text.Length - 1 ] - '0' ) , skin );
            EditorGUI.LabelField( enumObjRect , "-------------" , skin );

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight ( SerializedProperty property , GUIContent label )
        {
            return EditorGUIUtility.singleLineHeight * 3 + 6;
        }
    }



}
