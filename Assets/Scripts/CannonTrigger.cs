using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jungle.Exam
{
    public class CannonTrigger : MonoBehaviour
    {
        public List<Cannon> EnableCannons;
        public List<Cannon> DisableCannons;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                foreach (var cannon in EnableCannons)
                {
                    cannon.Enable();
                }
                
                foreach (var cannon in DisableCannons)
                {
                    cannon.Disable();
                }
            }
        }
    }
}