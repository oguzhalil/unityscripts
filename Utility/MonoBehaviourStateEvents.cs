using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourStateEvents : MonoBehaviour
{
    public UnityEvent uEventOnEnable;
    public UnityEvent uEventOnDisable;

    private void OnEnable ()
    {
            uEventOnEnable.SafeInvoke();
    }

    private void OnDisable ()
    {
        uEventOnDisable.SafeInvoke();
    }
}
