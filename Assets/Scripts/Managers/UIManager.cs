using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] private UIPanelController UIPanelController;

        #endregion SerializeField Variables

        #endregion Self Veriables

        #region Event Subcription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            CoreGameSignals.Instance.onPlay -= OnPlay;
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
        public void TryAgain()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

    }
}