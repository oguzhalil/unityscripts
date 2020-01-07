using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UtilityScripts
{
    public class LText : Text
    {
        public CaseStyle caseStyle = CaseStyle.Default;

        public string id;

        protected override void Start ()
        {
            base.Start();

            //Debug.Log(LocalizationManager.instance == null);

            if ( Localization.Instance != null )
            {
                string text = Localization.Instance.GetText( id );

                OnLanguageUpdated( text );

                Localization.Instance.Register( this );
            }
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            if ( Localization.Instance != null )
                Localization.Instance.UnRegister( this );

        }

        public void OnLanguageUpdated ( string text )
        {
            if ( caseStyle != CaseStyle.Default && !string.IsNullOrEmpty( text ) )
            {
                switch ( caseStyle )
                {
                    case CaseStyle.UpperCase:
                        text = text.ToUpperInvariant();
                        break;
                    case CaseStyle.LowerCase:
                        text = text.ToLowerInvariant();
                        break;
                    case CaseStyle.PascalCase:
                        if ( text.Length > 1 ) text = Char.ToUpperInvariant( text [ 0 ] ) + text.Substring( 1 );
                        break;
                    case CaseStyle.CamelCase:
                        if ( text.Length > 1 ) text = Char.ToLowerInvariant( text [ 0 ] ) + text.Substring( 1 );
                        break;
                    default:
                        break;
                }
            }

            this.text = text;
        }

        public enum CaseStyle
        {
            Default = 0,
            UpperCase = 1, // UPPERCASE
            LowerCase = 2, // lowercase
            PascalCase = 3, // PascalCase
            CamelCase = 4 // camelCase
        }

    }
}
