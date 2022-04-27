using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    public Renderer destinationRenderer;
    public Material[] materials;

    public void SetMaterial(int number)
    {
        if (number > materials.Length)
            return;
        destinationRenderer.material = materials[number];
    }
}
