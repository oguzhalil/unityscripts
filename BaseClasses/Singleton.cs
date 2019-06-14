using UnityEngine;

namespace EboxGames
{
    public abstract class Singleton<T> : MonoBehaviour /*, ISingleton*/ where T : Singleton<T> // prevent : class Derived : Singleton<SomeOtherType> T has to be inherited Singleton with same type
    {
        private static T instance;
        public static T Instance { get { return instance; } }
        public abstract bool DontDestroyWhenLoad ();

        protected virtual void Awake ()
        {
            if ( instance == null )
                instance = this as T;
            else
                Destroy( gameObject );

            if ( DontDestroyWhenLoad() )
            {
                transform.SetParent( null );
                DontDestroyOnLoad( gameObject );
            }
        }

        protected virtual void OnDestroy ()
        {
            instance = null;
        }
    }
}
