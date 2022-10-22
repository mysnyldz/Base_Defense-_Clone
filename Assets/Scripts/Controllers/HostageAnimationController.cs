using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class HostageAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent _agent;

        #endregion

        #endregion
        
        public void ChangeVelocity()
        {
            var speed = _agent.speed;
            var Velocity = speed/speed;
            animator.SetFloat("Velocity", Velocity);
        }

        private void ChangeAnimationState(HostageAnimTypes type, bool mode)
        {
            animator.SetBool(type.ToString(), mode);
        }


        public void SetPlayerAnimationStateTypes(HostageStatesTypes types)
        {
            ChangeAnimationState(HostageAnimTypes.Idle, false);
            ChangeAnimationState(HostageAnimTypes.Scared, false);
            ChangeAnimationState(HostageAnimTypes.Walk, false);
            switch (types)
            {
                case HostageStatesTypes.Idle:
                    ChangeAnimationState(HostageAnimTypes.Scared, true);
                    break;
                case HostageStatesTypes.Follow:
                    ChangeAnimationState(HostageAnimTypes.Walk, true);
                    break;
                case HostageStatesTypes.Tent:
                    ChangeAnimationState(HostageAnimTypes.Walk, true);
                    break;
            }
        }
    }
}