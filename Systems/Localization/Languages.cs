using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Custom/Localization/Language" )]
public class Languages : ScriptableObject
{
    public TextFile [] m_textFiles;

    public TextFile GetTextFile ( SystemLanguage systemLanguage )
    {
        foreach ( var textFile in m_textFiles )
        {
            if ( textFile.m_systemLanguage == systemLanguage )
            {
                return textFile;
            }
        }

        Debug.LogError( $"Requested text file is missing. { systemLanguage } " );

        return null;
    }

    public TextFile GetTextFile ( int systemLanguage )
    {
        return GetTextFile( ( SystemLanguage ) systemLanguage );
    }

    [System.Serializable]
    public class TextFile 
    {
        public TextAsset m_textAsset;
        public SystemLanguage m_systemLanguage;
    }
}
