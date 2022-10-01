using Enums;
using UnityEngine;

namespace Controllers
{
    public class EnemyAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion
        
        #endregion

        public void SetAnim(EnemyAnimTypes typeses)
        {
            animator.SetTrigger(typeses.ToString());
        }
    }
}