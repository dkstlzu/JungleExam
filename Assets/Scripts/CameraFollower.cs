using System;
using UnityEngine;

namespace Jungle.Exam
{
    public class CameraFollower : MonoBehaviour
    {
        private Character _targetCharacter;

        public Vector2 Offset;
        public float Dampingtime;
        private float _dampingSpeedX;
        private float _dampingSpeedY;

        private void Awake()
        {
            _targetCharacter = GameObject.FindWithTag("Player").GetComponent<Character>();
        }

        private void Update()
        {
            transform.position = new Vector3(
                Mathf.SmoothDamp(transform.position.x, _targetCharacter.transform.position.x + Offset.x, ref _dampingSpeedX, Dampingtime),
                Mathf.SmoothDamp(transform.position.y, _targetCharacter.transform.position.y + Offset.y, ref _dampingSpeedY, Dampingtime),
                transform.position.z);
        }
    }
}