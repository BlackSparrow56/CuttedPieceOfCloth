using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class MeshHelper
    {
        public static Dictionary<int, Vector3> AsDictionary(this Mesh self)
        {
            var dictionary = new Dictionary<int, Vector3>();
            for (int i = 0; i < self.vertexCount; i++)
            {
                dictionary.Add(i, self.vertices[i]);
            }

            return dictionary;
        }
    }
}
