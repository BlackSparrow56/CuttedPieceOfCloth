using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;
using ESparrow.Utils.Helpers;
using ESparrow.Utils.Extensions;

namespace Game.Controllers
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private float swipeForce;
        [SerializeField] private float speed;
        [SerializeField] private float rotatingDuration;

        [SerializeField] private Rigidbody rb;

        private Vector3 _currentPoint;

        private float _currentMouseX = 0f;
        private float _currentOffset = 0f;

        private bool _isSwiping = false;

        // Последовательное движение по точкам в аргументе
        public async Task Move(Collection<Vector3> points)
        {
            await MoveCoroutine(points).StartAsync(this);
        }

        // Движение по точкам
        private IEnumerator MoveCoroutine(Collection<Vector3> points)
        {
            foreach (var point in points)
            {
                yield return RotateToCoroutine(point, rotatingDuration);
                yield return MoveToCoroutine(point);
            }
        }

        // Движение к точке
        private IEnumerator MoveToCoroutine(Vector3 point)
        {
            var tempPosition = transform.position;

            float duration = (point - tempPosition).magnitude / speed;

            yield return CoroutinesHelper.Graduate(SetProgress, duration);

            void SetProgress(float progress)
            {
                _currentPoint = Vector3.Lerp(tempPosition, point, progress);
            }
        }

        // Плавный поворот в сторону точки
        private IEnumerator RotateToCoroutine(Vector3 point, float duration)
        {
            float tempRotation = transform.rotation.z;
            float targetRotation = transform.rotation.z + Vector3.SignedAngle(Vector3.down, point - transform.position, Vector3.forward);

            yield return CoroutinesHelper.Graduate(SetProgress, duration);

            void SetProgress(float progress)
            {
                transform.rotation = Quaternion.Lerp(Angle(tempRotation), Angle(targetRotation), progress);

                Quaternion Angle(float rotation)
                {
                    return Quaternion.Euler(0f, 0f, rotation);
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _currentMouseX = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(0) && _isSwiping)
            {
                if (_currentMouseX != 0)
                {
                    float delta =  _currentMouseX - Input.mousePosition.x;
                    _currentMouseX = Input.mousePosition.x;

                    _currentOffset += delta * swipeForce;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isSwiping = false;
            }

            transform.position = _currentPoint + Vector3.right * _currentOffset;
        }
    }
}
    