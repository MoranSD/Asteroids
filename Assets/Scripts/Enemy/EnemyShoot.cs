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

        private const float distanceBetweenEnemyAndBullet = 3;

        private void Update()
        {
            Vector2 directionToTarget = transform.position - Player.Instance.transform.position;

            if(directionToTarget.magnitude <= AttackRange)
            {
                NextAttackTime -= Time.deltaTime;
                if(NextAttackTime <= 0)
                {
                    Bullet bullet = GameManager.Instance.BulletPool.SpawnObject();

                    _spawnPoint.rotation = Quaternion.LookRotation(directionToTarget);
                    _spawnPoint.localPosition = directionToTarget.normalized * distanceBetweenEnemyAndBullet;
                    bullet.Shoot(_spawnPoint);
                }
            }
        }
    }
}
