using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace Tools
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolObject
    {
        public int Size { get; private set; }
        public int ActiveCount { get; private set; } = 0;
        public T Prefab { get; private set; }

        private List<T> _pool;

        public ObjectPool(int startSize, T prefab)
        {
            Size = startSize;
            Prefab = prefab;
            _pool = new List<T>();

            InitPool();
        }
        public T SpawnObject()
        {
            T obj;

            if (ActiveCount == Size)
            {
                Size++;
                obj = AddObjectInPool();
            }
            else
            {
                obj = TryGetFreeObject();
            }

            obj.OnSpawn();
            ActiveCount++;
            return obj;
        }
        public void ReturnToThePool(T obj)
        {
            obj.OnKill();
            ActiveCount--;
        }
        private void InitPool()
        {
            for (int i = 0; i < Size; i++)
                AddObjectInPool().Init();
        }
        private T AddObjectInPool()
        {
            T obj = Object.Instantiate(Prefab);
            obj.gameObject.SetActive(false);

            _pool.Add(obj);
            return obj;
        }
        private T TryGetFreeObject()
        {
            for (int i = 0; i < _pool.Count; i++)
                if (_pool[i].gameObject.activeInHierarchy == false)
                    return _pool[i];

            return null;
        }
    }
}
