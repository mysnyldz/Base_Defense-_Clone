using System;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using States.EnemyStates;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject health;
        [SerializeField] private TextMeshPro healthText;
        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        private float _timer;
        private float _regenerationSpeed = 0.1f;

        #endregion

        #endregion

        private void Update()
        {
            RegenerationHealth();
            DeathState();
        }


        public void HealthBarVisibles(PlayerStateTypes types)
        {
            switch (types)
            {
                case PlayerStateTypes.Battle:
                    healthBar.SetActive(true);
                    break;
                case PlayerStateTypes.Idle:
                    if (manager.Data.PlayerHealth == 100)
                    {
                        healthBar.SetActive(false);
                    }
                    break;
            }
        }

        private void DeathState()
        {
            if (manager.playerTypes == PlayerStateTypes.Battle &&
                manager.Data.PlayerHealth <=0)
            {
                CoreGameSignals.Instance.onFailed?.Invoke();
                manager.ChangeState(PlayerStateTypes.Death);
            }
        }
        private void RegenerationHealth()
        {
            _timer += Time.deltaTime;
            if (_timer >= _regenerationSpeed && manager.playerTypes == PlayerStateTypes.Idle &&
                manager.Data.PlayerHealth < 100)
            {
                _timer = 0;
                manager.Data.PlayerHealth++;
                HealthUpdate();
                if (manager.Data.PlayerHealth == 100)
                {
                    healthBar.SetActive(false);
                }
            }
        }

        public void DecreaseHealth(int value)
        {
            manager.Data.PlayerHealth -= value;
            HealthUpdate();
        }

        private void HealthUpdate()
        {
            float healthX = (manager.Data.PlayerHealth / 100f);
            health.transform.DOScale(new Vector3(healthX, 1, 1), 0.1f);
            HealthTextUpdate();
        }

        private void HealthTextUpdate()
        {
            healthText.text = manager.Data.PlayerHealth.ToString();
        }

        public void HealtBarRotation()
        {
            healthBar.transform.rotation = Quaternion.Euler(26,0,0);
        }


    }
}