using UnityEngine;
using PlayerSystem;

namespace EnemySystem
{
    public class EnemyShoot : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        public float AttackRange { get; set; }
        public float FireRate { get; set; }
        public float NextAttackTime { get; set; }

        private const float distanceBetweenEnemyAndBullet = 0.3f;

        private void Update()
        {
            Vector2 directionToTarget = Player.Instance.transform.position - transform.position;

            if(directionToTarget.magnitude <= AttackRange)
            {
                NextAttackTime -= Time.deltaTime;
                if(NextAttackTime <= 0)
                {
                    NextAttackTime = FireRate;
                    Bullet bullet = GameManager.Instance.BulletPool.SpawnObject();

                    _spawnPoint.up = directionToTarget.normalized;
                    _spawnPoint.localPosition = directionToTarget.normalized * distanceBetweenEnemyAndBullet;
                    bullet.Shoot(_spawnPoint);
                }
            }
        }
    }
}
