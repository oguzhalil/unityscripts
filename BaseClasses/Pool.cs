using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EboxGames
{
    public class Pool<T> where T : Component
    {
        private T instance;
        private T[] objects;
        private int size = 10;

        public delegate T OnInit ( T poolMember );

        private OnInit onInit;

        public T this [ int index ]
        {
            get { return objects [ index ]; }
            set { objects [ index ] = value; }

        }

        public Pool ( T instance )
        {
            this.instance = instance;
        }

        public Pool<T> Size ( byte value )
        {
            this.size = value;
            return this;
        }

        public Pool<T> OnInstantiation ( OnInit onInit )
        {

            this.onInit = onInit;
            return this;
        }

        public Pool<T> Fill ( GameObject parent = null )
        {
            objects = new T [ size ];

            for ( int i = 0; i < size; i++ )
            {
                if ( parent != null )
                    objects [ i ] = Object.Instantiate( instance , parent.transform );
                else
                    objects [ i ] = Object.Instantiate( instance );

                if ( onInit != null )
                    onInit( objects [ i ] );
            }

            return this;
        }

        public T [] GetAll () { return objects; }

        public int GetSize { get { return size; } }

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
                    ( obj as IPoolMember ).OnRestore();
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
