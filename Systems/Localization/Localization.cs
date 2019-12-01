using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using EboxGames;

public class Localization : Singleton<Localization>
{
    public Languages m_languages;
    public const string m_sKeyLang = "#language";
    private Dictionary<string , string> m_selectedLangPairs;
    public SystemLanguage m_selectSysLang;
    Dictionary<LText , Action<string>> m_localizedTexts;

    protected override void Awake ()
    {
        base.Awake();
        int language = PlayerPrefs.GetInt( m_sKeyLang , ( int ) SystemLanguage.English );
        m_localizedTexts = new Dictionary<LText , Action<string>>();
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

    public void Register ( LText lText )
    {
        if ( !m_localizedTexts.ContainsKey( lText ) )
        {
            m_localizedTexts.Add( lText , lText.OnLanguageUpdated );
        }
    }

    public void UnRegister ( LText lText )
    {
        if ( m_localizedTexts.ContainsKey( lText ) )
        {
            m_localizedTexts.Remove( lText );
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

    public override bool DontDestroyWhenLoad () { return true; }
}
