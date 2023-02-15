using UnityEngine;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float MoveSpeed { get; set; }
        public float TurnSpeed { get; set; }

        private Rigidbody2D _rigidBody2D;

        private void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            _rigidBody2D.angularVelocity = -horizontalInput * TurnSpeed;
            _rigidBody2D.AddForce(transform.up * verticalInput * TurnSpeed * Time.fixedDeltaTime);
        }
    }
}
