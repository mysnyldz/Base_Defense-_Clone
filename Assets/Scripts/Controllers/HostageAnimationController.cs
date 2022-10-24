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

        #endregion

        #endregion

        public void SetTriggerAnimation(HostageAnimTypes type)
        {
            animator.SetTrigger(type.ToString());
        }

        public void SetBoolAnimation(HostageAnimTypes type, bool mode)
        {
            animator.SetBool(type.ToString(), mode);
        }
    }
}