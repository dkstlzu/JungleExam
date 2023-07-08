using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Jungle.Exam
{
    public class Character : MonoBehaviour
    {
        public float RechargeBulletCoolTime;
        public BulletWheel BulletWheel;

        [Header("Movement")]
        [SerializeField] private Rigidbody2D rb;
        public float MaxSpeed;
        public float Acceleration;
        public float JumpPower;
        public bool isGrounded;

        [Header("Attack")] 
        public bool isReadyForChargeAttack;
        public bool isCharging;
        public SpriteMask ChargeMask;
        public float ChargeTime;
        private float _chargeValue;
        
        private float _timer;
        private float _targetXVelocity;
        private float _velocityEpsilon = 0.01f;
        
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= RechargeBulletCoolTime)
            {
                _timer = 0;
                BulletWheel.AddNewBullet();
            }

            if (isCharging)
            {
                _chargeValue += Time.deltaTime / ChargeTime;
                ChargeMask.transform.localPosition = new Vector3(0, Mathf.Lerp(-1, 0, _chargeValue), 0);
            }

            RaycastHit2D hit;
            int layerMask = LayerMask.GetMask("VirtualFloor");

            if (hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 0.1f, layerMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        private void FixedUpdate()
        {
            if (rb.velocity.x < _targetXVelocity + _velocityEpsilon && rb.velocity.x > _targetXVelocity - _velocityEpsilon) return;
            
            float forceDirection = 0;
            if (_targetXVelocity + _velocityEpsilon < rb.velocity.x)
            {
                forceDirection = -1;
            } else if (_targetXVelocity - _velocityEpsilon > rb.velocity.x)
            {
                forceDirection = 1;
            }
            
            rb.AddForce(Vector2.right * forceDirection * Acceleration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Finish")
            {
                FindObjectOfType<GameOverUI>().GameClear();
            } else if (other.tag == "Respawn")
            {
                FindObjectOfType<GameOverUI>().GameFail();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("DestructableWall"))
            {
                FindObjectOfType<GameOverUI>().GameFail();
            }
        }

        public void Move(float to)
        {
            if (to < 0)
            {
                _targetXVelocity = -1 * MaxSpeed;
            } else if (to == 0)
            {
                _targetXVelocity = 0;
            } else if (to > 0)
            {
                _targetXVelocity = 1 * MaxSpeed;
            }
        }

        public void Jump()
        {
            if (!isGrounded) return;
            
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }

        public void AttackReady()
        {
            isCharging = true;
        }

        public void ChargeAttackReady()
        {
            isReadyForChargeAttack = true;
        }

        public void Fire()
        {
            isCharging = false;
            ChargeMask.transform.localPosition = new Vector3(0, -1, 0);
            _chargeValue = 0;
            
            if (isReadyForChargeAttack)
            {
                Bullet[] bullets = BulletWheel.PopAllBullet();

                foreach (Bullet bullet in bullets)
                {
                    bullet.Fire();
                }
                
                BulletWheel.AddNewBullet();
                BulletWheel.AddNewBullet();

                isReadyForChargeAttack = false;
            }
            else
            {
                Bullet bullet = BulletWheel.PopBullet();
                if (bullet == null) return;
                
                bullet.Fire();
            }
        }
    }
}