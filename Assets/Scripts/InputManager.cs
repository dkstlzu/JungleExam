using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jungle.Exam
{
    public class InputManager : MonoBehaviour
    {
        public JungleExamInputAsset InputAsset;

        private Character Character;
        private void Awake()
        {
            InputAsset = new JungleExamInputAsset();
            InputAsset.Enable();

            InputAsset.Ingame.Move.performed += OnMove;
            InputAsset.Ingame.Move.canceled += OnMove;
            InputAsset.Ingame.Jump.performed += OnJump;
            InputAsset.Ingame.Attack.performed += OnAttackReady;
            InputAsset.Ingame.ChargeAttack.performed += OnChargeReady;
            InputAsset.Ingame.Attack.canceled += OnFire;

            Character = GameObject.FindWithTag("Player").GetComponent<Character>();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Character.Move(context.ReadValue<float>());
        }
        
        private void OnJump(InputAction.CallbackContext context)
        {
            Character.Jump();
        }
        
        private void OnAttackReady(InputAction.CallbackContext context)
        {
            Character.AttackReady();
        }

        private void OnChargeReady(InputAction.CallbackContext context)
        {
            Character.ChargeAttackReady();
        }
        
        private void OnFire(InputAction.CallbackContext obj)
        {
            Character.Fire();
        }
    }
}