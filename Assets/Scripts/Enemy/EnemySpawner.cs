using UnityEngine;
using Tools;

namespace EnemySystem
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        private float _spawnRate;
        private float _currentSpawnTime = 0;
        private bool _isWorking;

        private void Start()
        {
            _spawnRate = GameManager.Instance.GameData.SpawnerData.SpawnRate;
            _currentSpawnTime = _spawnRate;
        }
        private void Update()
        {
            if (_isWorking == false) return;

            _currentSpawnTime -= Time.deltaTime;
            if(_currentSpawnTime <= 0)
            {
                _currentSpawnTime = _spawnRate;

                Vector2 nextPosition = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
                GameManager.Instance.EnemyPool.SpawnObject().transform.position = nextPosition;
            }
        }
        public void Begin() => _isWorking = true;
        public void ResetSpawn() => _currentSpawnTime = 0;
        public void Stop() => _isWorking = false;
    }
}
