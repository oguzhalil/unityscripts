using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class UIBoxExtensions
{
    public static void Insert ( this EC<Button> ec , Action<Button , string> action )
    {
        ec.value.onClick.AddListener( delegate { action( ec.value , ec.id ); } );
    }

    public static T GetExtraComponent<T> ( this UIElem elem , string id ) where T : Component
    {
        if ( elem.cache.ContainsKey( id ) )
        {
            return elem.cache [ id ] as T;
        }

        for ( int i = 0; i < elem.extraComponents.Count; i++ )
        {
            ExtraEE extraEE = elem.extraComponents[i];

            if ( extraEE.id == id )
            {
                //if(typeof(T) == typeof(RectTransform))
                //{
                //    return extraEE.component.GetComponent<T>();
                //}

                elem.cache.Add( id , extraEE.component.GetComponent<T>() );

                return extraEE.component.GetComponent<T>();
            }
        }


        return null;
    }
}
