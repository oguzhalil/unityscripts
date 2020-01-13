using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform target;
    public bool bLocal;

    private void LateUpdate ()
    {
        if ( bLocal )
        {
            transform.localRotation = target.rotation;
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }

    private void OnDrawGizmos ()
    {
        //var bounds = GetComponent<MeshRenderer>().bounds;
        ////Gizmos.matrix = Matrix4x4.TRS( transform.position , transform.rotation , transform.localScale );
        //Gizmos.DrawWireCube( bounds.center , bounds.size );
        ////Gizmos.DrawLine( transform.position , transform.position + transform.rotation * bounds.center.normalized );
        //Gizmos.DrawLine( transform.position , bounds.center );
    }
}
