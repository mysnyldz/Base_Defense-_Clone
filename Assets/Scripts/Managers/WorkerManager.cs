using UnityEngine;

namespace Managers
{
    public class WorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables



        #endregion

        #region Serializefield Variables



        #endregion

        #region Private Variables



        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
    }
}