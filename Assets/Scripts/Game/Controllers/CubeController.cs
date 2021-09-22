using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private float fallingSpeed;

        [SerializeField] private Rigidbody rb;

        private float _currentVelocity = 0f;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float delta = ((targetPosition - rb.position) * 0.999f).x;
                _currentVelocity += delta;
            }

            rb.position += new Vector3(_currentVelocity * Time.deltaTime, -fallingSpeed * Time.deltaTime, 0f);
            rb.rotation = Quaternion.Euler(0f, 0f, _currentVelocity);

            _currentVelocity *= 0.1f * Time.deltaTime;
        }
    }
}
    