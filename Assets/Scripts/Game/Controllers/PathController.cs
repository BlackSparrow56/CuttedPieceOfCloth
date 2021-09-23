using System;
using System.Linq;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Controllers
{
    [Serializable]
    public class PathController
    {
        [SerializeField] private int subdivides;

        /// <summary>
        /// ¬озвращает точки исход€ из количества точек и ширины по которой они будут расположены.
        /// </summary>
        public Collection<Vector2> GeneratePath
        (
            Vector2 startPoint, 
            Vector2 endPoint, 
            float amplitude,
            int count
        )
        {
            var list = new Collection<Vector2>();

            list.Add(startPoint);

            for (int i = 0; i < count; i++)
            {
                list.Add(new Vector2(Random.Range(-amplitude / 2, amplitude / 2), (endPoint.y - startPoint.y) / count * i));
            }

            list.Add(endPoint);

            return list;
        }

        /// <summary>
        /// ѕодраздел€ет точки определЄнное количество раз.
        /// </summary>
        private Collection<Vector2> Subdivide(Collection<Vector2> points, int divides)
        {
            var newPoints = new Collection<Vector2>();
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
