using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
            var dictionary = _mesh.AsDictionary();
            dictionary.Add(dictionary.Count, first);
            dictionary.Add(dictionary.Count, second);
            dictionary.Add(dictionary.Count, third);

            Apply(dictionary);

            var list = new List<int>(_mesh.triangles);

            list.Add(IndexOf(first));
            list.Add(IndexOf(second));
            list.Add(IndexOf(third));

            _mesh.triangles = list.ToArray();

            return this;
        
            int IndexOf(Vector3 vertex)
            {
                var list = _mesh.vertices.ToList();
                return list.IndexOf(vertex);
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

        private void Apply(Dictionary<int, Vector3> dictionary)
        {
            _mesh.vertices = dictionary.Values.ToArray();
        }
    }
}
