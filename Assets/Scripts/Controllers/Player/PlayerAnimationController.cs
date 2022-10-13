using Enums;
using Keys;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;
        [SerializeField] private PlayerAnimTypes playerAnimTypes;

        #endregion Serialized Variables
        
        #endregion Self Variables
        

        public void ChangeVelocity(InputParams inputParams)
        {
            var VelocityX = inputParams.InputValues.x;
            var VelocityZ = inputParams.InputValues.z;
            playerAnimator.SetFloat("Velocity", (Mathf.Pow(VelocityX, 2) + Mathf.Pow(VelocityZ, 2)) / 2);
        }

        public void ChangeAnimationState(PlayerAnimTypes type)
        {
            playerAnimator.SetTrigger(type.ToString());
        }
    }
}