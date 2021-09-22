using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Controllers
{
    [Serializable]
    public class PathController
    {
        [SerializeField] private int subdivides;

        public List<Vector2> GeneratePath
        (
            Vector2 startPoint, 
            Vector2 endPoint, 
            float amplitude,
            int count
        )
        {
            var list = new List<Vector2>();

            list.Add(startPoint);

            for (int i = 0; i < count; i++)
            {
                list.Add(new Vector2(Random.Range(-amplitude / 2, amplitude / 2), (endPoint.y - startPoint.y) / count * i));
            }

            list.Add(endPoint);

            return Subdivide(list, subdivides);
        }

        private List<Vector2> Subdivide(List<Vector2> points, int divides)
        {
            var newPoints = new List<Vector2>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                var current = points[i];
                var next = points[i + 1];

                newPoints.Add(current);
                for (int j = 0; j < divides; j++)
                {
                    newPoints.Add(Vector2.Lerp(current, next, 1f / divides * j));
                }
            }

            newPoints.Add(points.Last());

            return newPoints;
        }
    }
}
