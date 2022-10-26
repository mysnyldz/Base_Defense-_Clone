using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] private UIPanelController UIPanelController;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI gemText;

        #endregion SerializeField Variables

        #endregion Self Veriables

        #region Event Subcription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onTryAgain += OnTryAgain;
            CoreGameSignals.Instance.onFailed += OnFailed;
            UISignals.Instance.onSetMoneyText += OnReadMoneyText;
            UISignals.Instance.onSetGemText += OnReadGemText;
        }


        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onTryAgain -= OnTryAgain;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            UISignals.Instance.onSetMoneyText -= OnReadMoneyText;
            UISignals.Instance.onSetGemText -= OnReadGemText;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subcription

        private void OnOpenPanel(UIPanels panels)
        {
            UIPanelController.OpenPanel(panels);
        }

        private void OnClosePanel(UIPanels panels)
        {
            UIPanelController.ClosePanel(panels);
        }

        public void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PlayPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
        
        public void OnTryAgain()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.TryPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PlayPanel);
        }
        

        public void TryAgain()
        {
            CoreGameSignals.Instance.onTryAgain?.Invoke();
        } 

        public void OnFailed()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.TryPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.PlayPanel);
            
        }

        private void OnReadMoneyText(float score)
        {
            moneyText.text = score.ToString();
        }

        private void OnReadGemText(float score)
        {
            gemText.text = score.ToString();
        }
    }
}