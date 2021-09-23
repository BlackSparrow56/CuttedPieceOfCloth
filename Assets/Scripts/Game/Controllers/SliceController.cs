using System;
using System.Linq;
using System.Collections.ObjectModel;
using UnityEngine;
using Utils;
using ESparrow.Utils.Extensions;

namespace Game.Controllers
{
    [Serializable]
    public class SliceController 
    {
        [SerializeField] private float range;

        [SerializeField] private Rect rect;

        // Основной скрипт, остальные созданы для его корректной работы
        /// <summary>
        /// Принимает точки, по которым резать прямоугольник и возвращает два меша.
        /// </summary>
        public Collection<Mesh> GetSlicedMeshes(Collection<Vector2> points)
        {
            return GetLeftMesh(points).ConcatWith(GetRightMesh(points)).AsCollection();
        }

        private Mesh GetLeftMesh(Collection<Vector2> points)
        {
            var offset = new Vector2(-range / 2, 0);

            var builder = new MeshBuilder();
            for (int i = 0; i < points.Count - 1; i++)
            {
                float currentY = points[i].y;
                float nextY = points[i + 1].y;

                var upperLeft = new Vector2(rect.xMin, currentY);
                var upperRight = points[i] + offset;
                var lowerRight = points[i + 1] + offset;
                var lowerLeft = new Vector2(rect.xMin, nextY);

                builder = builder.AddQuad(upperLeft, upperRight, lowerRight, lowerLeft);
            }

            return builder.Build();
        }

        private Mesh GetRightMesh(Collection<Vector2> points)
        {
            var offset = new Vector2(range / 2, 0);

            var builder = new MeshBuilder();
            for (int i = 0; i < points.Count - 1; i++)
            {
                float currentY = points[i].y;
                float nextY = points[i + 1].y;

                var upperLeft = points[i] + offset;
                var upperRight = new Vector2(rect.xMax, currentY);
                var lowerRight = new Vector2(rect.xMax, nextY);
                var lowerLeft = points[i + 1] + offset;

                builder = builder.AddQuad(upperLeft, upperRight, lowerRight, lowerLeft);
            }

            return builder.Build();
        }
    }
}
