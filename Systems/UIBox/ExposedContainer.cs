using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC<T> where T : Component
{
    public T value;
    public string id;
    public UIElem exposeElement;

    public EC(UIElem exposeElement )
    {
        this.value = exposeElement.component as T;
        this.exposeElement = exposeElement;
        id = exposeElement.id;
    }
}

[System.Serializable]
public class ExtraEE
{
    public Component component;
    public string id;
}
