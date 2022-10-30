using Enums;
using UnityEngine;

namespace Controllers
{
    public class CameraMovementController : MonoBehaviour
    {
        [SerializeField] Animator camAnimator;
        public void EnterIdle() { changeCam(CameraStates.Idle); } 

        public void EnterDrone() { changeCam(CameraStates.Drone);}

        public void EnterTurret() { changeCam(CameraStates.Turret); }
        
        public void CamReset() { changeCam(CameraStates.Idle); }

        public void changeCam(CameraStates cameraStates)
        {
            camAnimator.Play(cameraStates.ToString());
        }
    }
}