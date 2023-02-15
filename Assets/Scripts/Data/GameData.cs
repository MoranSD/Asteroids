using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game/Data")]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] public SpawnerData SpawnerData { get; private set; }
    }
}
