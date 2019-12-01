using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivityEvent : MonoBehaviour
{
    public UnityEvent m_actOnEnable;
    public UnityEvent m_actOnDisable;

    private void OnEnable ()
    {
        //if ( DevelopmentScript.m_bWaitForAuth )
        //{
        //    StartCoroutine( WaitAuth() );
        //}
        //else
        //{
            if ( m_actOnEnable != null )
            {
                m_actOnEnable.Invoke();
            }
        //}
    }

    private void OnDisable ()
    {
        if ( m_actOnDisable != null )
        {
            m_actOnDisable.Invoke();
        }
    }

    IEnumerator WaitAuth ()
    {
        yield return new WaitUntil( () => PlayFab.PlayFabClientAPI.IsClientLoggedIn() && DevelopmentScript.m_bInvokeEvents);
        m_actOnEnable.Invoke();
    }
}
