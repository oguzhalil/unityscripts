using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using System.Runtime.Serialization.Formatters.Binary;

public static class ExtensionMethods
{
    public static Vector3 ToVector3 ( this Vector2 v , float z )
    {
        return new Vector3( v.x , v.y , z );
    }

    public static Vector2 ToVector2 ( this Vector3 v )
    {
        return new Vector2( v.x , v.y );
    }

    public static void XAxis ( this Transform t , float x )
    {
        t.position = new Vector3( t.position.x + x , t.position.y , t.position.z );
    }

    public static bool isPositive ( this float value )
    {
        return value > 0;
    }

    // YAxis'i verilen değere eşitler
    public static void MoveToYAxis ( this Transform transform , float yAxis )
    {
        Vector3 position = transform.position;
        position.y = yAxis;
        // Debug.Log(yAxis);
        transform.position = position;
    }

    // YAxis e verilen değeri ekler
    public static void MoveFromYAxis ( this Transform transform , float yAxis )
    {
        Vector3 position = transform.position;
        position.y += yAxis;
        // Debug.Log(yAxis);
        transform.position = position;
    }

    public static T RandomElement<T> ( this T [] array )
    {
        int index = Random.Range( 0 , array.Length );
        return array [ index ];
    }

    public static T RandomElement<T> ( this List<T> list )
    {
        int index = Random.Range( 0 , list.Count );
        return list [ index ];
    }

    private static System.Random rng = new System.Random();

    public static void Shuffle<T> ( this List<T> list )
    {
        int n = list.Count;
        while ( n > 1 )
        {
            n--;
            int k = rng.Next( n + 1 );
            T value = list [ k ];
            list [ k ] = list [ n ];
            list [ n ] = value;
        }
    }

    public static void Shuffle<T> ( this T [] array )
    {
        int n = array.Length;
        while ( n > 1 )
        {
            n--;
            int k = rng.Next( n + 1 );
            T value = array [ k ];
            array [ k ] = array [ n ];
            array [ n ] = value;
        }
    }

    public static T DeepClone<T> ( this T obj )
    {
        using ( var ms = new MemoryStream() )
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize( ms , obj );
            ms.Position = 0;

            return ( T ) formatter.Deserialize( ms );
        }
    }

    public static void SafeInvoke ( this Action action )
    {
        if ( action != null )
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke<T> ( this Action<T> action , T value )
    {
        if ( action != null )
        {
            action.Invoke( value );
        }
    }

    public static void SafeInvoke ( this UnityEvent @event )
    {
        if ( @event != null )
        {
            @event.Invoke();
        }
    }

    public static void SafeInvoke<T> ( this UnityEvent<T> @event , T value )
    {
        if ( @event != null )
        {
            @event.Invoke( value );
        }
    }

    //public static void SafeInvokeDelete ( this Action action )
    //{
    //    if ( action != null )
    //    {
    //        action.Invoke();
    //    }
    //    action = null;
    //}

    public static void SafeInvokeDelete<T> ( this Action<T> action , T value )
    {
        if ( action != null )
        {
            action.Invoke( value );
        }
        action = null;
    }

    public static List<GameObject> GetAllObjectsOnlyInScene ()
    {
        List<GameObject> objectsInScene = new List<GameObject>( 1000 );

        foreach ( GameObject go in Resources.FindObjectsOfTypeAll( typeof( GameObject ) ) as GameObject [] )
        {
#if UNITY_EDITOR
            if ( !UnityEditor.EditorUtility.IsPersistent( go.transform.root.gameObject ) && !( go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave ) )
#else
                if ( !( go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave ) )
#endif
                objectsInScene.Add( go );
        }

        return objectsInScene;
    }

    public static List<T> GetAllObjectsOnlyInSceneByType<T> () where T : Component
    {
        List<T> objectsInScene = new List<T>( 1000 );

        foreach ( T go in Resources.FindObjectsOfTypeAll( typeof( T ) ) as T [] )
        {
#if UNITY_EDITOR
            if ( !UnityEditor.EditorUtility.IsPersistent( go.transform.root.gameObject ) && !( go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave ) )
#else
                if ( !( go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave ) )
#endif
                objectsInScene.Add( go );
        }

        return objectsInScene;
    }

    public static void OpenURLStore ()
    {
#if UNITY_ANDROID
        Application.OpenURL( $"market://details?id={Application.identifier}" );
#elif UNITY_IPHONE
        Application.OpenURL($"itms-apps://itunes.apple.com/app/id{Application.identifier}");
#endif
    }
}