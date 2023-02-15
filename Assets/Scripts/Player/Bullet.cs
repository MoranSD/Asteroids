using System.Collections;
using UnityEngine;
using Interfaces;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPoolObject
    {
        private Rigidbody2D _rigidBody2D;
        private float _moveSpeed;
        private float _damage;

        private float _lifeTime;
        Coroutine _lifeTimeProcess;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
                GameManager.Instance.BulletPool.ReturnToThePool(this);
            }
        }

        public void Shoot(Transform point)
        {
            transform.position = point.position;
            transform.up = point.up;
            _rigidBody2D.AddForce(transform.up * _moveSpeed, ForceMode2D.Impulse);
        }
        public void Init()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _moveSpeed = GameManager.Instance.GameData.BulletData.Speed;
            _damage = GameManager.Instance.GameData.BulletData.Damage;
            _lifeTime = GameManager.Instance.GameData.BulletData.LifeTime;
        }
        public void OnKill()
        {
            if(_lifeTimeProcess != null)
                StopCoroutine(_lifeTimeProcess);

            gameObject.SetActive(false);
        }

        public void OnSpawn()
        {
            gameObject.SetActive(true);
            _lifeTimeProcess = StartCoroutine(LifeTimeProcess());
        }

        private IEnumerator LifeTimeProcess()
        {
            yield return new WaitForSeconds(_lifeTime);

            _lifeTimeProcess = null;
            GameManager.Instance.BulletPool.ReturnToThePool(this);
        }
    }
}
