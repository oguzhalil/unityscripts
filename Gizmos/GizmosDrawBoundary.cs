using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawBoundary : MonoBehaviour
{
    public Color color = Color.green;
    public Vector3 size = Vector3.one;
    public bool bWireframe = true;
    public bool bTransformMatrix = true;

    private void OnDrawGizmos ()
    {
        if ( bTransformMatrix )
        {
            Gizmos.matrix = Matrix4x4.TRS( transform.position , transform.rotation , transform.localScale );
        }

        var components = GetComponents<BoxCollider>();

        foreach ( var component in components )
        {
            Gizmos.color = color;
            if ( bWireframe || !component.enabled)
                Gizmos.DrawWireCube( Vector3.zero + component.center , component.size );
            else
                Gizmos.DrawCube( Vector3.zero + component.center , component.size );
        }
    }
}
