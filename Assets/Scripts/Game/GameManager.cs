using UnityEngine;
using PlayerSystem;
using EnemySystem;
using Tools;
using Data;

public class GameManager : Singleton<GameManager>
{
    /*
     * инициализация всего дерьма
     * запуск компонентов геймплея
     * обработка смерти игрока
     * перезапуск игры
     */

    [field: SerializeField] public GameData GameData { get; private set; }

    public ObjectPool<Bullet> BulletPool;
    public ObjectPool<Enemy> EnemyPool;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Enemy _enemyPrefab;

    protected override void Awake()
    {
        base.Awake();

        BulletPool = new ObjectPool<Bullet>(100, _bulletPrefab);
        EnemyPool = new ObjectPool<Enemy>(10, _enemyPrefab);

        Player.Instance.OnDeadEvent += OnPlayerDead;
    }
    private void OnDisable()
    {
        Player.Instance.OnDeadEvent -= OnPlayerDead;
    }
    private void OnPlayerDead()
    {
        //clear all pools

        Player.Instance.transform.position = Vector3.zero;
        Player.Instance.Revive();
    }
}
