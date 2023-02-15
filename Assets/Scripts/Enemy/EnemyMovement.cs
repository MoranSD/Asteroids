using UnityEngine;
using PlayerSystem;

namespace EnemySystem
{
    public class EnemyMovement : MonoBehaviour
    {
        public float MoveSpeed { get; set; }
        public float FollowRange { get; set; }
        public Rigidbody2D RigidBody2D { get; set; }

        private void Update()
        {
            Vector2 directiontoTarget = Player.Instance.transform.position - transform.position;

            if (directiontoTarget.magnitude > FollowRange)
                RigidBody2D.velocity = directiontoTarget.normalized * MoveSpeed;
        }
    }
}
