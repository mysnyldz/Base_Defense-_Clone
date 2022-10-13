using Enums;
using UnityEngine;

namespace Controllers.SupporterControllers
{
    public class SupporterMoneyAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion
        
        public void SetAnim(SupporterAnimTypes animTypes)
        {
            animator.SetTrigger(animTypes.ToString());
        } 
    }
}