using UnityEngine;
using Interfaces;
using Tools;

namespace PlayerSystem
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerShoot))]
    public class Player : Singleton<Player>, IDamageable
    {
        public event System.Action OnDeadEvent;

        [SerializeField] private float _health;
        public float Health => _health;

        private PlayerMovement _movement;
        private PlayerShoot _shoot;

        private void Start()
        {
            _movement = GetComponent<PlayerMovement>();
            _shoot = GetComponent<PlayerShoot>();

            _movement.MoveSpeed = GameManager.Instance.GameData.PlayerMovementData.MoveSpeed;
            _movement.TurnSpeed = GameManager.Instance.GameData.PlayerMovementData.TurnSpeed;
        }

        public void ApplyDamage(float value)
        {
            if (value < 0)
                throw new System.Exception("Damage cant be less than zero!");

            _health -= value;

            if (_health < 0)
                OnDead();
        }
        public void Revive()
        {
            gameObject.SetActive(true);
        }
        protected override void OnDestroy()
        {
            OnDeadEvent = null;

            base.OnDestroy();
        }
        private void OnDead()
        {
            gameObject.SetActive(false);
            OnDeadEvent?.Invoke();
        }
    }
}
