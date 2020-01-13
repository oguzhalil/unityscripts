using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDebugger : MonoBehaviour
{
    public BoxCollider [] boxColliders;


    [ContextMenu( "InitializeBoxColliders" )]
    public void InitializeBoxColliders ()
    {
        boxColliders = Resources.FindObjectsOfTypeAll<BoxCollider>();
    }

    [ContextMenu( "Remove Intersect Colliders" )]
    public void RemoveIntersectingColliders ()
    {
        var listA = Resources.FindObjectsOfTypeAll<BoxCollider>();

        List<GameObject> destroyList = new List<GameObject>();

        for ( int i = 0; i < listA.Length; i++ )
        {
            for ( int j = i; j < listA.Length; j++ )
            {
                if ( listA [ i ].name == listA [ j ].name )
                {
                    continue;
                }

                if ( listA [ j ].name == "Collider(Clone)" )
                {
                    if ( listA [ i ].bounds.Intersects( listA [ j ].bounds ) )
                    {
                        destroyList.Add( listA [ j ].gameObject );
                    }
                }
            }
        }

        //foreach ( var a in listA )
        //{
        //    foreach ( var b in listB )
        //    {
        //        if ( a.bounds.Intersects( b.bounds ) )
        //        {
        //            if( b.name == "Collider(Clone)" )
        //            {
        //                destroyList.Add( b.gameObject );
        //            }
        //        }
        //    }
        //}

        foreach ( var d in destroyList )
        {
            DestroyImmediate( d );
        }
    }

    private void OnDrawGizmos ()
    {
        foreach ( var collider in boxColliders )
        {
            Gizmos.matrix = Matrix4x4.TRS( collider.transform.position , collider.transform.rotation , collider.transform.localScale );
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube( Vector3.zero , collider.size );
        }
    }
}
