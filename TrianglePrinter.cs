using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePrinter : MonoBehaviour
{

    [ContextMenu("Print")]
    public void Print()
    {
        MeshFilter [] meshFilters = FindObjectsOfType<MeshFilter>();

        Dictionary<GameObject , int> pairs = new Dictionary<GameObject , int>();

        for ( int i = 0; i < meshFilters.Length; i++ )
        {
            MeshFilter meshFilter = meshFilters [ i ];

            if (  meshFilter.transform.root.name == "HigherVertexCount" )
                continue;

            if ( !pairs.ContainsKey( meshFilter.gameObject) && meshFilter.gameObject && meshFilter.sharedMesh )
                pairs.Add( meshFilter.gameObject , meshFilter.sharedMesh.vertexCount );
        }

        bubbleSort( pairs );

    }

    static void bubbleSort ( Dictionary<GameObject , int> pairs )
    {
        int [] arr = new int [ pairs.Count ];

        GameObject [] strArray = new GameObject [ pairs.Count];

        pairs.Keys.CopyTo( strArray , 0 );

        pairs.Values.CopyTo( arr , 0 );

        Dictionary<GameObject , int> sortedPairs = new Dictionary<GameObject , int>();

        int n = arr.Length;
        for ( int i = 0; i < n - 1; i++ )
            for ( int j = 0; j < n - i - 1; j++ )
                if ( arr [ j ] > arr [ j + 1 ] )
                {
                    // swap temp and arr[i] 
                    GameObject stemp = strArray [ j ];
                    strArray [ j ] = strArray [ j + 1 ];
                    strArray [ j + 1 ] = stemp;

                    int temp = arr [ j ];
                    arr [ j ] = arr [ j + 1 ];
                    arr [ j + 1 ] = temp;
                }


        for ( int i = 0; i < arr.Length; i++ )
        {
            int vertexCount = arr [ i ];
            GameObject name = strArray [ i ];

            Debug.Log( "Vertex Count  " + vertexCount + "mesh name " + name.name , name );
        }
    }
}
