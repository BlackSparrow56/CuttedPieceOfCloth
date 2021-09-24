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
        /// <summary>
        /// Возвращает точки исходя из количества точек и ширины по которой они будут расположены.
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

            for (int i = 0; i < count; i++)
            {
                list.Add(new Vector2(Random.Range(-amplitude / 2, amplitude / 2), (endPoint.y - startPoint.y) / count * i));
            }

            return list;
        }
    }
}
