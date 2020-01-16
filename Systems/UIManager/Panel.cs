using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public string id;
    [NonSerialized] public Canvas canvas;

    private void Awake ()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Hide ()
    {
        gameObject.SetActive( false );
    }

    public void Show ()
    {
        gameObject.SetActive( true );
    }

}
