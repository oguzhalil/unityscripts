using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawBoundary : MonoBehaviour
{
    public Color color = Color.green;
    public Vector3 size = Vector3.one;
    public bool bWireframe = true;
    public bool bTransformMatrix = true;
    public bool bEncapsulateBounds = false;
    public Type type;

    public enum Type
    {
        BoundingBox = 0,
        BoxCollider = 1
    }

    private void OnDrawGizmos ()
    {
        if ( bTransformMatrix )
        {
            Gizmos.matrix = Matrix4x4.TRS( transform.position , transform.rotation , transform.localScale );
        }

        if ( type == Type.BoxCollider )
        {
            var components = GetComponentsInChildren<BoxCollider>();

            foreach ( var component in components )
            {
                Gizmos.color = color;
                if ( bWireframe || !component.enabled )
                    Gizmos.DrawWireCube( Vector3.zero + component.center , component.size );
                else
                    Gizmos.DrawCube( Vector3.zero + component.center , component.size );
            }
        }

        else if ( type == Type.BoundingBox )
        {
            var components = GetComponentsInChildren<MeshRenderer>();

            if ( bEncapsulateBounds )
            {
                Bounds encapsulated = new Bounds( transform.position , Vector3.zero );

                foreach ( var component in components )
                {
                    encapsulated.Encapsulate( component.bounds );
                }

                Gizmos.color = color;
                if ( bWireframe)
                    Gizmos.DrawWireCube( Vector3.zero + encapsulated.center , encapsulated.size );
                else
                    Gizmos.DrawCube( Vector3.zero + encapsulated.center , encapsulated.size );
            }
            else
            {
                foreach ( var component in components )
                {
                    Gizmos.color = color;
                    if ( bWireframe || !component.enabled )
                        Gizmos.DrawWireCube( Vector3.zero + component.bounds.center , component.bounds.size );
                    else
                        Gizmos.DrawCube( Vector3.zero + component.bounds.center , component.bounds.size );
                }
            }
        }

    }
}
