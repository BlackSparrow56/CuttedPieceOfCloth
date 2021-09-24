using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using ESparrow.Utils.Extensions;

namespace Utils
{
    public class MeshBuilder
    {
        private Mesh _mesh = new Mesh();

        public MeshBuilder()
        {

        }

        public MeshBuilder(Mesh mesh)
        {
            _mesh = mesh;
        }
        
        public MeshBuilder AddTriangle(Vector3 first, Vector3 second, Vector3 third)
        {
            var vertices = GetVertices();
            _mesh.vertices = vertices.ToArray();

            var triangles = GetTriangles();
            _mesh.triangles = triangles.ToArray();

            return this;

            List<Vector3> GetVertices()
            {
                var vertices = new List<Vector3>(_mesh.vertices);

                vertices.Add(first);
                vertices.Add(second);
                vertices.Add(third);

                return vertices;
            }

            List<int> GetTriangles()
            {
                var triangles = new List<int>(_mesh.triangles);

                triangles.Add(vertices.IndexOf(first));
                triangles.Add(vertices.IndexOf(second));
                triangles.Add(vertices.IndexOf(third));

                return triangles;
            }
        }

        public MeshBuilder AddQuad(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
        {
            return AddTriangle(first, second, third).AddTriangle(first, third, fourth);
        }

        public Mesh Build()
        {
            return _mesh;
        }
    }
}
