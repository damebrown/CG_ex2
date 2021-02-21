using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;


public class MeshData
{
    public List<Vector3> vertices; // The vertices of the mesh 
    public List<int> triangles; // Indices of vertices that make up the mesh faces
    public Vector3[] normals; // The normals of the mesh, one per vertex

    // Class initializer
    public MeshData()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    // Returns a Unity Mesh of this MeshData that can be rendered
    public Mesh ToUnityMesh()
    {
        Mesh mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            normals = normals
        };

        return mesh;
    }

    // Calculates surface normals for each vertex, according to face orientation
    public void CalculateNormals()
    {
        normals = new Vector3[vertices.Count];
        for (int i=0; i < triangles.Count; i = i + 3)
        {
            Vector3 p1 = (vertices[triangles[i]]) - (vertices[triangles[i+2]]);
            Vector3 p2 = vertices[triangles[i+1]] - vertices[triangles[i+2]];
            Vector3 n = Vector3.Cross(p1, p2);
            n = Vector3.Normalize(n);
            normals[triangles[i]] += n;
            normals[triangles[i+1]] += n;
            normals[triangles[i+2]] += n;    
        }

        for(int i=0; i<normals.Length; ++i)
        {
            normals[i] = Vector3.Normalize(normals[i]);
        }
    }

    // Edits mesh such that each face has a unique set of 3 vertices
    public void MakeFlatShaded()
    {
        // Explanation: because each face contain his own verives all his vertices normals will
        //be calculat base of the same face normal - so they will get the same color.
        List<Vector3> new_vertices = new List<Vector3>();
        List<int> new_triangles = new List<int>();

        for (int i=0; i<triangles.Count; ++i){
            new_triangles.Add(i);
            new_vertices.Add(vertices[triangles[i]]);
        }
        vertices = new_vertices;
        triangles = new_triangles;
    }
}