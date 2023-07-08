using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Jungle.Exam
{
    public class BulletWheel : MonoBehaviour
    {
        public List<Bullet> BulletList;
        public int MaxBulletNumber;
        public int CurrentBulletNumber => BulletList.Count;
        public bool isEmpty => BulletList.Count == 0;

        public float WheelRadius;
        public float RotatingSpeed;

        private float _currentRotateStandardAngle = 0f;

        public void AddBullet(Bullet bullet)
        {
            if (CurrentBulletNumber >= MaxBulletNumber) return;
            if (BulletList.Contains(bullet)) return;
            
            BulletList.Add(bullet);
            if (CurrentBulletNumber > 0)
            {
                BulletList[0].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            }
        }

        public void RemoveBullet(Bullet bullet)
        {
            if (CurrentBulletNumber <= 0) return; 
            if (!BulletList.Contains(bullet)) return;

            BulletList.Remove(bullet);
            if (CurrentBulletNumber > 0)
            {
                BulletList[0].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            }
        }

        private void Awake()
        {
            Bullet.OnBulletCollideWithDestructableWall += RemoveBullet;
        }

        private void Update()
        {
            _currentRotateStandardAngle += RotatingSpeed * Time.deltaTime;

            if (isEmpty) return;

            float angleInterval = 360f / CurrentBulletNumber;
            float currentAngleInRadian = Mathf.Deg2Rad * _currentRotateStandardAngle;
            float angleIntervalInRadian = Mathf.Deg2Rad * angleInterval;

            for (int i = 0; i < CurrentBulletNumber; i++)
            {
                BulletList[i].TargetPosition = new Vector3(
                    Mathf.Cos(currentAngleInRadian + angleIntervalInRadian * i) * WheelRadius + transform.position.x,
                    Mathf.Sin(currentAngleInRadian + angleIntervalInRadian * i) * WheelRadius + transform.position.y,
                    0);
            }
        }

        public GameObject BulletPrefab;
        
        [ContextMenu("AddNewBullet")]
        public void AddNewBullet()
        {
            if (CurrentBulletNumber >= MaxBulletNumber) return;
            
            Bullet bullet = Instantiate(BulletPrefab, transform).GetComponent<Bullet>();

            AddBullet(bullet);
        }

        public Bullet PopBullet()
        {
            if (isEmpty) return null;

            Bullet bullet = BulletList[0];
            
            RemoveBullet(bullet);
            
            bullet.transform.parent = null;

            return bullet;
        }

        public Bullet[] PopAllBullet()
        {
            Bullet[] bullets = BulletList.ToArray();

            foreach (Bullet bullet in bullets)
            {
                RemoveBullet(bullet);
                bullet.transform.parent = null;
            }

            return bullets;
        }
    }
}