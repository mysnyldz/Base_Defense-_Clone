using Abstract;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using ES3Types;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyDeathState : EnemyBaseState
    {
        #region Self Variables

        public bool IsDeath;

        [SerializeField] private GameObject money;

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyData _data;
        private EnemyTypes _types;
        private float _timer;
        private float _deathDelay = 1.5f;

        #endregion

        #endregion

        public EnemyDeathState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data,
            ref EnemyTypes types)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
            _types = types;
        }

        public override void EnterState()
        {
            _manager.SetTriggerAnim(EnemyAnimTypes.Death);
            _manager.transform.DOJump(new Vector3(_agent.transform.position.x, -0.5f, _agent.transform.position.z + 2),
                1,
                1, 2f);
            _agent.enabled = false;
        }

        public override void UpdateState()
        {
            _timer += Time.deltaTime * 0.8f;
            if (_timer >= _deathDelay)
            {
                IsDeath = true;
                DropMoney();
                PoolSignals.Instance.onReleasePoolObject?.Invoke(_types.ToString(), _agent.gameObject);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }

        private void DropMoney()
        {
            for (int i = 0; i < 3; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Money.ToString(),
                    _agent.gameObject.transform);
                obj.transform.DOJump(
                    new Vector3(obj.transform.position.x + Random.Range(-1, 1),
                        obj.transform.position.y + Random.Range(0.5f, 2),
                        obj.transform.position.z + Random.Range(-1, 1)),
                    2, 1, 0.5f);
            }

            PlayerSignals.Instance.onEnemyRemoveTargetList?.Invoke(_agent.gameObject);
            _agent.enabled = true;
        }
    }
}