using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColorPicker : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material targetMaterial;
    public Color color = Color.gray;

    private void Start ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SetColor( color );
    }

    public void SetColor( Color color )
    {
        foreach ( var material in meshRenderer.sharedMaterials )
        {
            if(targetMaterial.name.Contains(targetMaterial.name))
            {
                material.color = color;
            }
        }
    }
}
