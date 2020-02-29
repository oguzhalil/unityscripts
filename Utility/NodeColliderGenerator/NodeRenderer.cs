using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRenderer : MonoBehaviour
{
    //public enum EMesh { Cube = 0 }
    public Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private BoxCollider boxCollider;
    private Mesh newMesh;
    private static bool bCalculate = true;
    public Material material;

    private void Awake ()
    {
           
    }

    // Start is called before the first frame update
    void Start ()
    {
        if(tag == "NoCollider")
        {
            return;
        }
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();

        newMesh = new Mesh();
        newMesh.vertices = mesh.vertices;
        newMesh.triangles = mesh.triangles;
        newMesh.uv = mesh.uv;

        Vector3 [] vertices = newMesh.vertices;
        boxCollider = GetComponent<BoxCollider>();

        for ( int i = 0; i < vertices.Length; i++ )
        {
            Vector3 vPos = vertices [ i ];
            vPos.x *= boxCollider.size.y;
            vPos.y *= boxCollider.size.x;
            vPos.z *= boxCollider.size.z;
            vertices [ i ] = (Quaternion.AngleAxis(-90f , Vector3.forward) * vPos) + boxCollider.center;
        }
        newMesh.vertices = vertices;
        newMesh.RecalculateBounds();
        //newMesh.RecalculateNormals();
        //newMesh.RecalculateTangents();
        meshFilter.sharedMesh = newMesh;
        meshRenderer.material = material;
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
