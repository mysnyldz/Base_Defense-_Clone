using Enums;
using UnityEngine;

namespace Controllers
{
    public class MinerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion
        
        #endregion

        public void SetAnim(MinerAnimTypes types)
        {
            animator.SetTrigger(types.ToString());
        }
    }
}