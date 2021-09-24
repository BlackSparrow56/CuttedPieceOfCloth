using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ESparrow.Utils.UI;
using ESparrow.Utils.Extensions;

namespace Game.Controllers
{
    /// <summary>
    /// Головной скрипт, может висеть на любом объекте. 
    /// Остальные классы сериализованы и вложены в этот.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Vector2 playerStartPoint;

        [SerializeField] private Vector2 startPoint;
        [SerializeField] private Vector2 endPoint;

        [SerializeField] private bool looped;

        [SerializeField] private PathController pathController;
        [SerializeField] private SliceController sliceController;
        [SerializeField] private MeshController meshController;

        [SerializeField] private CubeController cubeController;

        [SerializeField] private ImprovedButton button;
        
        [SerializeField] private Transform player;

        [SerializeField] private Slider amplitudeSlider;
        [SerializeField] private Slider countSlider;

        private bool _alreadyGenerated = false;

        private List<Vector3> points = new List<Vector3>();

        /// <summary>
        /// Главный и единственный метод, вызывается кнопкой.
        /// </summary>
        private async Task Generate()
        {
            if (!_alreadyGenerated)
            {
                var path = pathController.GeneratePath(startPoint, endPoint, amplitudeSlider.value, (int)countSlider.value);
                var path3d = path.Select(value => value.ToVector3()).AsCollection();

                points = path3d.ToList();

                var meshes = sliceController.GetSlicedMeshes(path);

                meshController.Set(meshes);

                _alreadyGenerated = true;

                do
                {
                    player.position = playerStartPoint;
                    player.rotation = Quaternion.identity;

                    await cubeController.Move(path3d);
                } while (looped);
            }
        }

        private async void Start()
        {
            await button.AwaitClick();
            await Generate();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            if (points.Count > 0)
            {
                var previous = points.First();
                foreach (var point in points)
                {
                    Gizmos.DrawLine(previous, point);
                    previous = point;
                }
            }
        }
    }
}
