#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMeshGenerator : MonoBehaviour
{
    public Material material;
    public Material lineMaterial;

    public int gridSize = 100;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<int> indices = new List<int>();


    void Start()
    {
        Material[] materialArray = new Material[2] { material, lineMaterial };

        //vertices
        for(int i = 0; i < gridSize; i++) { //row
            for(int j = 0; j < gridSize; j++) { //column
                vertices.Add(new Vector3(j, 0, i));
                vertices.Add(new Vector3(j, 0, i + 1));
                vertices.Add(new Vector3(j+1, 0, i + 1));
                vertices.Add(new Vector3(j+1, 0, i));
            }
        }

        //triangles + indices
        for(int k = 0; k < gridSize*gridSize; k++) {
            triangles.Add(k * 4);
            triangles.Add(k * 4 + 1);
            triangles.Add(k * 4 + 2);

            triangles.Add(k * 4);
            triangles.Add(k * 4 + 2);
            triangles.Add(k * 4 + 3);

            indices.Add(k * 4);
            indices.Add(k * 4 + 1);
            indices.Add(k * 4 + 1);
            indices.Add(k * 4 + 2);
            indices.Add(k * 4 + 2);
            indices.Add(k * 4 + 3);
            indices.Add(k * 4 + 3);
            indices.Add(k * 4);
        }



        //mesh
        Mesh mesh = new Mesh();
        mesh.subMeshCount = 2;

        mesh.vertices = vertices.ToArray();
        mesh.SetTriangles(triangles, 0);

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
        gameObject.transform.localScale = new Vector3(1, 1, 1);

        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

        mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 1);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

        gameObject.GetComponent<MeshRenderer>().materials = materialArray;

        var savePath = "Assets/" + "foo.asset";
        Debug.Log("Saved Mesh to:" + savePath);
        UnityEditor.AssetDatabase.CreateAsset(mesh, savePath);
    }

}

#endif