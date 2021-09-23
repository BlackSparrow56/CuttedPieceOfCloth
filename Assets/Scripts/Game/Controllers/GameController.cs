using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Controllers
{
    /// <summary>
    /// �������� ������, ����� ������ �� ����� �������. 
    /// ��������� ������ ������������� � ������� � ����.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Vector2 playerStartPoint;

        [SerializeField] private Vector2 startPoint;
        [SerializeField] private Vector2 endPoint;

        [SerializeField] private PathController pathController;
        [SerializeField] private SliceController sliceController;
        [SerializeField] private MeshController meshController;

        [SerializeField] private Transform player;

        [SerializeField] private Slider amplitudeSlider;
        [SerializeField] private Slider countSlider;

        /// <summary>
        /// ������� � ������������ �����, ���������� �������.
        /// </summary>
        public void Generate()
        {
            var path = pathController.GeneratePath(startPoint, endPoint, amplitudeSlider.value, (int) countSlider.value);
            var meshes = sliceController.GetSlicedMeshes(path);
            meshController.Set(meshes);

            player.position = playerStartPoint;
            player.rotation = Quaternion.identity;
        }
    }
}
