using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Jungle.Exam
{
    public class Cannon : MonoBehaviour
    {
        public GameObject CannonBallPrefab;

        public Transform TargetDirection;
        public float AngleVariation;
        private Vector2 _thisFireDirection;

        public float FireInterval;
        public float FireIntervalVariation;
        private float _thisFireInterval;
        public float FirePower;
        public float FirePowerVariation;
        private float _thisFirePower;

        public float TorqueVariation;
        
        private float _timer;
        public bool isEnabled;

        public SpriteRenderer CannonBarrelSpriteRenderer;

        private void Start()
        {
            SetVariations();
        }

        public void Enable()
        {
            isEnabled = true;
        }

        public void Disable()
        {
            isEnabled = false;
        }

        private void Update()
        {
            if (!isEnabled) return;
            
            _timer += Time.deltaTime;
            CannonBarrelSpriteRenderer.color =
                new Color(1, Mathf.Lerp(0, 1, 1 - _timer / _thisFireInterval), Mathf.Lerp(0, 1, 1 - _timer / _thisFireInterval));

            if (_timer >= _thisFireInterval)
            {
                _timer = 0f;
                Rigidbody2D rb = Instantiate(CannonBallPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                
                rb.AddForce(_thisFireDirection * _thisFirePower, ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(-TorqueVariation, TorqueVariation));
                
                SetVariations();
            }
        }

        void SetVariations()
        {
            Vector2 direction = (TargetDirection.position - transform.position).normalized;

            float RadianFix = 0;
            if (direction.x < 0)
            {
                RadianFix = Mathf.Deg2Rad * 180;
            }
            else
            {
                RadianFix = 0;
            }
            float thisFireAngleInRadian = Mathf.Atan(direction.y / direction.x) + Random.Range(-Mathf.Deg2Rad * AngleVariation, Mathf.Deg2Rad * AngleVariation);

            thisFireAngleInRadian += RadianFix;
            
            _thisFireDirection = new Vector2(Mathf.Cos(thisFireAngleInRadian), Mathf.Sin(thisFireAngleInRadian));
            _thisFireInterval = FireInterval + Random.Range(-FireIntervalVariation, FireIntervalVariation);
            _thisFirePower = FirePower + Random.Range(-FirePowerVariation, FirePowerVariation);
        }
    }
}