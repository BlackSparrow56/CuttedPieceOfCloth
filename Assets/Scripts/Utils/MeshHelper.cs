using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class MeshHelper
    {
        public static Dictionary<Vector3, int> AsDictionary(this Mesh self)
        {
            var dictionary = new Dictionary<Vector3, int>();
            for (int i = 0; i < self.vertexCount; i++)
            {
                dictionary.Add(self.vertices[i], i);
            }

            return dictionary;
        }
    }
}
