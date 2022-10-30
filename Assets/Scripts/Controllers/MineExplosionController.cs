using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class MineExplosionController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public SphereCollider collider;

        #endregion

        #region Serializefield Variables

        [SerializeField] private MineExplosionManager manager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void Start()
        {
            collider.transform.DOScale(new Vector3(0f, 0f, 0f), 0.1f).SetEase(Ease.OutFlash);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                manager.EnemyList.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                manager.EnemyList.Remove(other.gameObject);
            }
        }

        public void ColliderOpen()
        {
            collider.transform.DOScale(new Vector3(7f, 7f, 7f), 1f).SetEase(Ease.OutFlash);
        }

        public void ColliderClose()
        {
            collider.transform.DOScale(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutFlash);
        }
    }
}