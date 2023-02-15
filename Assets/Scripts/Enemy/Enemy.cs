using UnityEngine;
using PlayerSystem;
using Interfaces;

namespace EnemySystem
{
    [RequireComponent(typeof(EnemyMovement), typeof(EnemyShoot))]
    public class Enemy : MonoBehaviour, IPoolObject, IDamageable
    {
        public float Health => _health;
        private float _health;

        private EnemyMovement _movement;
        private EnemyShoot _shoot;

        public void ApplyDamage(float value)
        {
            if (value < 0)
                throw new System.Exception("Damage cant be less than zero!");

            _health -= value;

            if (_health < 0)
                OnDead();
        }

        public void Init()
        {
            _health = GameManager.Instance.GameData.EnemyData.Health;
            _movement = GetComponent<EnemyMovement>();
            _shoot = GetComponent<EnemyShoot>();

            _movement.RigidBody2D = GetComponent<Rigidbody2D>();
            _movement.MoveSpeed = GameManager.Instance.GameData.EnemyData.MoveSpeed;
            _movement.FollowRange = GameManager.Instance.GameData.EnemyData.FollowRange;

            _shoot.AttackRange = GameManager.Instance.GameData.EnemyData.FollowRange;
        }

        public void OnKill()
        {
            gameObject.SetActive(false);
        }

        public void OnSpawn()
        {
            gameObject.SetActive(true);
            _health = GameManager.Instance.GameData.EnemyData.Health;
            _shoot.NextAttackTime = _shoot.FireRate;
        }
        private void OnDead()
        {
            GameManager.Instance.EnemyPool.ReturnToThePool(this);
        }
    }
}
