using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using ESparrow.Utils.Extensions;

namespace Game.Controllers
{
    [Serializable]
    public class MeshController
    {
        [SerializeField] private int subdividesCount;

        [SerializeField] private MeshFilter firstFilter;
        [SerializeField] private MeshFilter secondFilter;

        /// <summary>
        /// Устанавливает двум заготовленным мешам форму из аргумента и симулирует из них ткань.
        /// </summary>
        public void Set(Collection<Mesh> slices)
        {
            SetMesh(firstFilter, slices.First());
            SetMesh(secondFilter, slices.Last());

            void SetMesh(MeshFilter filter, Mesh mesh)
            {
                filter.mesh = mesh;

                var renderer = filter.gameObject.GetComponent<SkinnedMeshRenderer>();
                var cloth = filter.gameObject.GetComponent<Cloth>();

                renderer.sharedMesh = mesh;

                SubdividePoints();
                SetMaxDistance();

                void SubdividePoints()
                {
                    var vertices = new List<Vector3>();
                    for (int i = 0; i < cloth.vertices.Length; i++)
                    {
                        var current = cloth.vertices[i];

                        vertices.Add(current);

                        if (i != cloth.vertices.Length - 1)
                        {
                            var next = cloth.vertices[i + 1];

                            for (int j = 0; j < subdividesCount; j++)
                            {
                                float progress = (float)j / subdividesCount;

                                var point = Vector3.Lerp(current, next, progress);
                                vertices.Add(point);
                            }
                        }
                    }
                }

                void SetMaxDistance()
                {
                    for (int i = 0; i < cloth.vertices.Length; i++)
                    {
                        cloth.coefficients[i].maxDistance = Mathf.Abs(cloth.vertices[i].x / 25f);
                    }
                }
            }
        }
    }
}
