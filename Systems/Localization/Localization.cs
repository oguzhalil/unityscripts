using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace UtilityScripts
{
    public class Localization : UniqueSingleton<Localization>
    {
        public Languages m_languages;
        public const string m_sKeyLang = "#language";
        private Dictionary<string , string> m_selectedLangPairs;
        public SystemLanguage m_selectSysLang;
        Dictionary<LocText , Action<string>> m_localizedTexts;

        protected override void Awake ()
        {
            base.Awake();
            int language = PlayerPrefs.GetInt( m_sKeyLang , ( int ) SystemLanguage.English );
            m_localizedTexts = new Dictionary<LocText , Action<string>>();
            UpdateLanguage( language );
        }

        public void UpdateLanguage ( int systemLanguage )
        {
            Languages.TextFile textFile = m_languages.GetTextFile( systemLanguage ); ;
            m_selectSysLang = textFile.m_systemLanguage;
            m_selectedLangPairs = JsonConvert.DeserializeObject<Dictionary<string , string>>( textFile.m_textAsset.text );

            foreach ( var localizedText in m_localizedTexts ) // call registered actions
            {
                if ( m_selectedLangPairs.ContainsKey( localizedText.Key.id ) )
                    localizedText.Value( m_selectedLangPairs [ localizedText.Key.id ] );
            }

            PlayerPrefs.SetInt( m_sKeyLang , systemLanguage );
        }

        public void Register ( LocText locText )
        {
            if ( !m_localizedTexts.ContainsKey( locText ) )
            {
                m_localizedTexts.Add( locText , locText.OnLanguageUpdated );
            }
        }

        public void Unregister ( LocText locText )
        {
            if ( m_localizedTexts.ContainsKey( locText ) )
            {
                m_localizedTexts.Remove( locText );
            }
        }

        public string GetText ( string id )
        {
            if ( m_selectedLangPairs.ContainsKey( id ) )
            {
                return m_selectedLangPairs [ id ];
            }

            return "#ERROR";
        }
    }
}
