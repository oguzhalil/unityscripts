using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderByDistance : MonoBehaviour
{
    private Bounds bounds;
    private bool active = true;
    private Collider [] colliders;

    // Start is called before the first frame update
    void Start ()
    {
        colliders = GetComponentsInChildren<Collider>();

        for ( int i = 0; i < colliders.Length; i++ )
        {
            if ( i == 0 )
            {
                bounds = new Bounds( colliders [ i ].bounds.center , colliders [ i ].bounds.size );
            }

            bounds.Encapsulate( colliders [ i ].bounds );
        }

        // as default active.
        Activate();
    }

    private void Update ()
    {
        //if ( RCC_SceneManager.Instance.activePlayerVehicle )
        //{
        //    if ( Vector3.Distance( bounds.center , RCC_SceneManager.Instance.activePlayerVehicle.transform.position ) < bounds.extents.magnitude * 1.5f && !active )
        //    {
        //        Activate();
        //    }
        //    else if ( active )
        //    {
        //        Deactivate();
        //    }
        //}
    }

    private void Deactivate ()
    {
        active = false;

        for ( int i = 0; i < colliders.Length; i++ )
        {
            colliders [ i ].enabled = false;
        }
    }

    private void Activate ()
    {
        active = true;

        for ( int i = 0; i < colliders.Length; i++ )
        {
            colliders [ i ].enabled = true;
        }
    }
}
