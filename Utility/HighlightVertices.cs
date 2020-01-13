using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightVertices : MonoBehaviour
{

    private MeshFilter filter;

    private void OnDrawGizmos ()
    {
        if(!filter )
        {
            filter = GetComponent<MeshFilter>();
        }

        foreach ( var item in filter.sharedMesh.vertices )
        {
            Gizmos.DrawSphere( item , .1f );
        }

        
    }
}
