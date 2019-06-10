using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageAsset : ScriptableObject
{
    public TextAsset textAsset;

    private static readonly Dictionary<SystemLanguage,  string> values = new Dictionary<SystemLanguage, string>();

    [SerializeField] private SystemLanguage language;

    public static string Get( SystemLanguage language )
    {
        if ( language == SystemLanguage.Turkish )
            return values [ SystemLanguage.Turkish ];

        else return values [ SystemLanguage.English ];
    }

    void OnEnable()
    {
        values.Add( language , textAsset.text );
    }
}
