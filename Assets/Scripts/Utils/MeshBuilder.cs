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
            dictionary.Add(first, dictionary.Count);
            dictionary.Add(second, dictionary.Count);
            dictionary.Add(third, dictionary.Count);

            Apply(dictionary);

            var triangles = new List<int>(_mesh.triangles);

            triangles.Add(dictionary[first]);
            triangles.Add(dictionary[second]);
            triangles.Add(dictionary[third]);

            _mesh.triangles = triangles.ToArray();

            return this;
        }

        public MeshBuilder AddQuad(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
        {
            return AddTriangle(first, second, third).AddTriangle(first, third, fourth);
        }

        public Mesh Build()
        {
            return _mesh;
        }

        private void Apply(Dictionary<Vector3, int> dictionary)
        {
            _mesh.vertices = dictionary.Keys.ToArray();
        }
    }
}
