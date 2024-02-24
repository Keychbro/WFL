using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.MyMesh
{
    public static class MeshCreator
    {
        public static Mesh GetMesh(Vector3[] vertices, Vector2[] uvs, int[] trianglesIndexes)
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = trianglesIndexes;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }
    }
}