using System.Linq;
using System;
using UnityEngine;
using ESparrow.Utils.Extensions;

namespace Game.Controllers
{
    [Serializable]
    public class MeshController
    {
        [SerializeField] private MeshFilter firstFilter;
        [SerializeField] private MeshFilter secondFilter;

        public void Set(Mesh[] slices)
        {
            SetMesh(firstFilter, slices.First());
            SetMesh(secondFilter, slices.Last());

            void SetMesh(MeshFilter filter, Mesh mesh)
            {
                filter.mesh = mesh;

                var renderer = filter.gameObject.GetComponent<SkinnedMeshRenderer>();
                var cloth = filter.gameObject.GetComponent<Cloth>();

                renderer.sharedMesh = mesh;

                for (int i = 0; i < cloth.vertices.Length; i++)
                {
                    cloth.coefficients[i].maxDistance = Mathf.Abs(cloth.vertices[i].x / 25f);
                }
            }
        }
    }
}
