using UnityEngine;

namespace Tools
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }
        protected Singleton() { }

        protected virtual void Awake()
        {
            if (Instance == null) Instance = this as T;
            else Destroy(this.gameObject);
        }
        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}
