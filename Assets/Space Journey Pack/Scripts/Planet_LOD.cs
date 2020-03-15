using UnityEngine;
using System.Collections;

public class Planet_LOD : MonoBehaviour {

	public float radius; //leave as is and declare in the inspector, or declare a value in start
 
	void Start () {
 
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
 
		for (var i=0; i<vertices.Length; i++) {
 
			vertices[i] = vertices[i].normalized*radius;
 
		}
 
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds ();
 
	}
}
