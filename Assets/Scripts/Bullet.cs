using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Jungle.Exam
{
    public class Bullet : MonoBehaviour
    {
        public Vector3 TargetPosition;
        public float DampingTime;
        public bool isFired;
        public float FiringSpeed;

        private float _currentDampingSpeedX;
        private float _currentDampingSpeedY;

        public static event Action<Bullet> OnBulletCollideWithDestructableWall;
        private void Update()
        {
            if (isFired)
            {
                transform.Translate(Vector2.right * (Time.deltaTime * FiringSpeed));
            }
            else
            {
                transform.position = new Vector3(
                    Mathf.SmoothDamp(transform.position.x, TargetPosition.x, ref _currentDampingSpeedX, DampingTime), 
                    Mathf.SmoothDamp(transform.position.y, TargetPosition.y, ref _currentDampingSpeedY, DampingTime), 
                    0);
            }
        }

        public void Fire()
        {
            isFired = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("DestructableWall"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                OnBulletCollideWithDestructableWall?.Invoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "BulletDestoyer")
            {
                Destroy(gameObject);
            }
        }
    }
}