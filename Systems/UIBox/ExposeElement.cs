using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExposeElement : MonoBehaviour
{
    public string id;
    public Component component;
    public List< ExtraEE> extraComponents;

    public readonly Dictionary<string, Component> cache = new Dictionary<string, Component>();

    public T Get<T> () where T : Component
    {
        if ( component is T )
            return component as T;
        else if ( component.GetComponent<T>() )
            return component.GetComponent<T>();


        Debug.LogError( "Error incompatible type. " );

        return null;

    }
}
