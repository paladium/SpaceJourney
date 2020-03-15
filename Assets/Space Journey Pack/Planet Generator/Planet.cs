using UnityEngine;
using LibNoise.Unity;
using LibNoise.Unity.Generator;
using LibNoise.Unity.Operator;
using System.IO;
using System;
using System.Collections;
public class Planet : MonoBehaviour
{

    public float amplitude = 10;


    void Start()
    {
        Perlin noise = new Perlin();
        noise.Seed = UnityEngine.Random.seed;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        var i = 0;


        while (i < vertices.Length)
        {
            Vector3 pos = vertices[i];
            vertices[i] = pos * ((float)(amplitude + noise.GetValue(pos.x, pos.y, pos.z)));
            i++;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }


}
