using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;
        [SerializeField] private PlayerAnimTypes playerAnimType;

        #endregion Serialized Variables

        #endregion Self Variables

        public void ChangeAnimationState(PlayerAnimTypes type)
        {
            if (playerAnimType != type)
            {   
                playerAnimType = type;
                playerAnimator.Play(type.ToString());
            }
        }
    }
}