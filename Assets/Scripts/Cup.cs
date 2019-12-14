using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public MeshRenderer liquidMesh;

    public string GetName()
    {
        return liquidMesh.material.name;
    }
}
