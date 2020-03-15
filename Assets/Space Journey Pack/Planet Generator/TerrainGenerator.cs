using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Unity.Operator;
using LibNoise.Unity.Generator;
using System;

public class TerrainGenerator : MonoBehaviour
{

    public Transform target;
    public GameObject planeprefab;
    public int buffer;
    public float detailScale;
    public float heightScale;
    public float planeSize = 60;
    private int planeCount;

    private int tileX;
    private int tileZ;

    private Perlin noise = new Perlin();

    private Tile[,] terrainTiles;
    void Start()
    {
        planeCount = buffer * 2 + 1;
        tileX = Mathf.RoundToInt(target.position.x / planeSize);
        tileZ = Mathf.RoundToInt(target.position.z / planeSize);
        Generate();
    }
    void Generate(float det_scale, float h_scale)
    {
        this.detailScale = det_scale;
        this.heightScale = h_scale;

    }
    void Generate()
    {
        if (terrainTiles != null)
        {
            foreach (Tile t in terrainTiles)
            {
                Destroy(t.gameObject);
            }
        }
        terrainTiles = new Tile[planeCount, planeCount];
        for (int x = 0; x < planeCount; x++)
        {
            for (int z = 0; z < planeCount; z++)
            {
                terrainTiles[x, z] = GenerateTile(tileX - buffer + x, tileZ - buffer + z);

            }
        }
    }
    private Tile GenerateTile(int x, int z)
    {
        GameObject plane = (GameObject)Instantiate(planeprefab, new Vector3(x * planeSize, 0, z * planeSize), Quaternion.identity);
        plane.transform.localScale = new Vector3(planeSize * 0.1f, 1, planeSize * 0.1f);
        plane.transform.parent = transform;

        Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices.Length; v++)
        {
            Vector3 vertex_pos = plane.transform.position + vertices[v] * planeSize / 10;
            float height = (float)noise.GetValue(vertex_pos * detailScale);
            vertices[v].y = height * heightScale;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        plane.AddComponent<MeshCollider>();
        Tile tile = new Tile();
        tile.gameObject = plane;
        tile.tileX = x;
        tile.tileZ = z;

        return tile;
    }

    private void Cull(int changeX, int changeZ)
    {
        int i, j;
        Tile[] newTiles = new Tile[planeCount];
        Tile[,] newTerrainTiles = new Tile[planeCount, planeCount];

        if (changeX != 0)
        {
            for (i = 0; i < planeCount; i++)
            {
                Destroy(terrainTiles[buffer - buffer * changeX, i].gameObject);
                terrainTiles[buffer - buffer * changeX, i] = null;
                newTiles[i] = GenerateTile(tileX + buffer * changeX + changeX, tileZ - buffer + i);
            }
        }
        if (changeZ != 0)
        {
            for (i = 0; i < planeCount; i++)
            {
                Destroy(terrainTiles[i, buffer - buffer * changeZ].gameObject);
                terrainTiles[i, buffer - buffer * changeZ] = null;
                newTiles[i] = GenerateTile(tileX - buffer + i, tileZ + buffer * changeZ + changeZ);
            }
        }
        Array.Copy(terrainTiles, newTerrainTiles, planeCount * planeCount);
        for (i = 0; i < planeCount; i++)
        {
            for (j = 0; j < planeCount; j++)
            {
                Tile t = terrainTiles[i, j];
                if (t != null)
                {
                    newTerrainTiles[-tileX - changeX + buffer + t.tileX, -tileZ - changeZ + buffer + t.tileZ] = t;
                }
            }
        }
        for (i = 0; i < newTiles.Length; i++)
        {
            Tile t = newTiles[i];
            newTerrainTiles[-tileX - changeX + buffer + t.tileX, -tileZ - changeZ + buffer + t.tileZ] = t;
        }
        terrainTiles = newTerrainTiles;
    }
    void Update()
    {
        int newTileX = Mathf.RoundToInt(target.position.x / planeSize);
        int newTileZ = Mathf.RoundToInt(target.position.z / planeSize);

        if (newTileX != tileX)
        {
            Cull(newTileX - tileX, 0);
            tileX = newTileX;
        }

        if (newTileZ != tileZ)
        {
            Cull(0, newTileZ - tileZ);
            tileZ = newTileZ;
        }
    }
}
public class Tile
{
    public GameObject gameObject;
    public int tileX;
    public int tileZ;
};