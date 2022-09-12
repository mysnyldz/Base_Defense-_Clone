using Keys;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private Transform manager;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion
        public void LookRotation(InputParams ınputParams)
        {
            Vector3 movementDirection = new Vector3(ınputParams.InputValues.x, 0, ınputParams.InputValues.y);
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                manager.rotation = Quaternion.RotateTowards( manager.rotation,toRotation,30);
            }
        }
    }
}