﻿using UnityEngine;

namespace UtilityScripts
{
    // Refer to : Curiously Recurring Template Pattern ( CRTP) 
    // C# Generics
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> 
    {
        public static T Instance { private set; get; }

        protected virtual void Awake ()
        {
            if ( Instance == null )
            {
                Instance = this as T;
            }
            else
            {
                Destroy( gameObject );
            }
        }
    }

    public class UniqueSingleton<T> : MonoBehaviour where T : UniqueSingleton<T>
    {
        public static T Instance { private set; get; }

        protected virtual void Awake ()
        {
            if ( Instance == null )
            {
                Instance = this as T;
                transform.SetParent( null );
                DontDestroyOnLoad( gameObject );
            }
            else
            {
                Destroy( gameObject );
            }
        }
    }

}
