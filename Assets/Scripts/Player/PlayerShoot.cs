using UnityEngine;

namespace PlayerSystem
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Bullet bullet = GameManager.Instance.BulletPool.SpawnObject();
                bullet.Shoot(_spawnPoint);
            }
        }
    }
}
