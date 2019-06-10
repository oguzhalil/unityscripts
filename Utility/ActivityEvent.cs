using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivityEvent : MonoBehaviour
{
    public UnityEvent onEnable;

    private void OnEnable ()
    {
        if ( onEnable != null )
            onEnable.Invoke();
    }
}
