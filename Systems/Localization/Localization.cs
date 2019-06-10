using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MiniJSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Localization : MonoBehaviour
{
    public static Localization Instance { private set; get; }

    public SystemLanguage language;

    Dictionary<string, string> pairs;

    Dictionary<LText, System.Action<string>> localizedText = new Dictionary<LText, System.Action<string>>();

    static ResourceRequest async;

    public int languageCode;

    public const string prefsKey = "#language";

    public bool Turkish;

    public readonly List<SystemLanguage> languages = new List<SystemLanguage>() { SystemLanguage.Turkish , SystemLanguage.English };

    void Awake ()
    {
        if ( Instance == null )
        {
            Instance = this;
        }
        else
        {
            Destroy( Instance );
            Instance = this;
        }

        if ( PlayerPrefs.HasKey( prefsKey ) )
        {
            languageCode = PlayerPrefs.GetInt( prefsKey );

            language =  ( SystemLanguage ) languageCode;
        }
        else
        {
            if ( languages.Contains( Application.systemLanguage ) )
            {
                language = Application.systemLanguage;
            }
            else
                language = SystemLanguage.English;

            languageCode = ( int ) language;

        }

        if ( Turkish )
        {
            language = SystemLanguage.Turkish;

            languageCode = ( int ) language;
        }
        else
        {
            language = SystemLanguage.English;

            languageCode = ( int ) language;
        }

        UpdateLanguage( false );

        //StartCoroutine(Run());

        RunSync();
    }

    public void Switch ()
    {
        if ( language == SystemLanguage.Turkish )
        {
            language = SystemLanguage.English;
        }
        else
        {
            language = SystemLanguage.Turkish;
        }

        RunSync();

        //User.Instance.UpdateLanguage( ( int ) language );

        UpdateLanguage( true );

    }

    void UpdateLanguage ( bool callbacks )
    {
        PlayerPrefs.SetInt( prefsKey , ( int ) language );

        if ( callbacks )
            foreach ( var label in localizedText )
            {
                label.Key.OnLanguageUpdated( GetText( label.Key.id ) );
            }
    }


    public void RunSync ()
    {
        //Debug.Log( Instance.GetType().Name + ".cs language folder loading process began." );

        //string json = Resources.Load<TextAsset>(instance.GetPath(instance.language)).text;

        Instance.pairs = JsonConvert.DeserializeObject<Dictionary<string , string>>( LanguageAsset.Get( language ) );

        //foreach (var text in instance.localizedText)
        //{
        //    text.Value(instance.pairs[text.Key.id]);
        //}

        //Debug.Log( Instance.GetType().Name + ".cs language folder loading process completed." );
    }

    public static IEnumerator Run ()
    {
        Debug.Log( Instance.GetType().Name + ".cs language folder loading process began." );

        async = Resources.LoadAsync<TextAsset>( Instance.GetPath( Instance.language ) );

        //async.completed += Async_completed;

        async.priority = ( int ) ThreadPriority.High;

        yield return new WaitUntil( () => async.progress >= 1f );

        Instance.pairs = JsonConvert.DeserializeObject<Dictionary<string , string>>( async.asset.ToString() );

        foreach ( var text in Instance.localizedText )
        {
            text.Value( Instance.pairs [ text.Key.id ] );
        }

        Debug.Log( Instance.GetType().Name + ".cs language folder loading process completed." );

    }

    private static void Async_completed ( AsyncOperation obj )
    {
        print( "Completed. " + obj.isDone + " " + obj.progress );
    }

    public void Register ( LText lText )
    {
        if ( !localizedText.ContainsKey( lText ) )
            localizedText.Add( lText , lText.OnLanguageUpdated );
    }

    public void UnRegister ( LText lText )
    {
        if ( localizedText.ContainsKey( lText ) )
            localizedText.Remove( lText );
    }

    string GetPath ( SystemLanguage language )
    {
        return language == SystemLanguage.Turkish ? "tr" : "en";
    }

    //public void OnLevelWasLoaded(int level)
    //{
    //    foreach (var text in instance.localizedText)
    //    {
    //        text.Value(instance.pairs[text.Key.id]);
    //    }
    //}

    public string GetText ( string id )
    {
        if ( pairs.ContainsKey( id ) )
            return pairs [ id ];

        return "#ERROR";
    }
}
