using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/*
     public class PoolUser
     {
        Pool<ParticleSystem> particlePool;
        // I want to override instantiation of particles 
        void Start()
        {
            particlePool  = new Pool<ParticleSystem>(instance, parent,size).OverrideInstantiation()
        }
     }
*/

namespace UtilityScripts
{
    public class Pool<T> where T : Component
    {
        public readonly T instance;
        public readonly T [] objects;
        public readonly int size = 10;
        public readonly Transform parent;
        private Action<T> onInstantiation;
        private Func<T> overrideInstantiation;
        public bool bOverrideInstantiation;

        public T this [ int index ]
        {
            get { return objects [ index ]; }
            set { objects [ index ] = value; }
        }

        public Pool ( T instance , Transform parent , int size )
        {
            this.instance = instance;
            this.size = size;
            objects = new T [ size ];
        }

        public Pool<T> OnInstantiation ( Action<T> onInit )
        {
            this.onInstantiation = onInit;
            return this;
        }

        public Pool<T> OverrideInstantiation ( Func<T> overrideDelegate )
        {
            this.overrideInstantiation = overrideDelegate;
            bOverrideInstantiation = true;
            return this;
        }

        public Pool<T> Fill ( GameObject parent = null )
        {
            for ( int i = 0; i < size; i++ )
            {
                if ( bOverrideInstantiation )
                {
                    objects [ i ] = overrideInstantiation();
                }
                else if ( parent != null )
                {
                    objects [ i ] = Object.Instantiate( instance , parent.transform );
                }
                else
                {
                    objects [ i ] = Object.Instantiate( instance );
                }

                onInstantiation.SafeInvoke( objects [ i ] );
            }

            return this;
        }

        public T Get ()
        {
            foreach ( var item in objects )
            {
                if ( !item.gameObject.activeSelf )
                {
                    item.gameObject.SetActive( true );
                    return item;
                }
            }

            Debug.LogError( " Pool returning null object of  " + typeof( T ) );
            return null;
        }

        public void Restore ()
        {
            bool derivedFromIPool = false;

            if ( First() is IPoolMember )
                derivedFromIPool = true;

            foreach ( var obj in objects )
            {
                obj.gameObject.SetActive( false );

                if ( derivedFromIPool )
                {
                    ( obj as IPoolMember ).OnRestore();
                }
                else
                {
                    //obj.SendMessage( "Restore" );
                }
            }
        }

        public T First ()
        {
            return objects [ 0 ];
        }

        public T Last ()
        {
            return objects [ ( size - 1 ) ];
        }
    }

    public interface IPoolMember
    {
        void OnRestore ();
    }
}
