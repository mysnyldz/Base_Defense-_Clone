using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region SerilizeField

        #endregion

        #region Private Variables

        private int _money;
        private int _gem;
        
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onAddMoney += OnAddMoney;
            ScoreSignals.Instance.onAddGem += OnAddGem;
            ScoreSignals.Instance.onReduceMoney += OnReduceMoney;
            ScoreSignals.Instance.onReduceGem += OnReduceGem;
            ScoreSignals.Instance.onBuyArea += OnBuyArea;
        }



        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onAddMoney -= OnAddMoney;
            ScoreSignals.Instance.onAddGem -= OnAddGem;
            ScoreSignals.Instance.onReduceMoney -= OnReduceMoney;
            ScoreSignals.Instance.onReduceGem -= OnReduceGem;
            ScoreSignals.Instance.onBuyArea -= OnBuyArea;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void OnBuyArea()
        {
            
        }

        private void OnReduceGem()
        {
            
        }

        private void OnReduceMoney()
        {
           
        }

        private void OnAddGem()
        {
            
        }

        private void OnAddMoney()
        {
            
        }
        
    }
}