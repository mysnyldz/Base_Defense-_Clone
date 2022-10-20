using Abstract;
using Controllers;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using States.MinerStates;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public NavMeshAgent agent;
        [ShowInInspector] public MinerBaseState currentMinerBaseState;
        public MoveMinerState MoveMiner = new MoveMinerState();
        public MoveDepotState MoveDepot = new MoveDepotState();
        public DigMinerState DigMiner = new DigMinerState();
        public GameObject GemVeins;
        public GameObject GemDepot;

        #endregion

        #region Serialized Variables
    
        [SerializeField] private MinerAnimationController animationController;
        [SerializeField] private GameObject gem;
        [SerializeField] private GameObject pickaxe;


        #endregion
        #region Private Variables
    

        #endregion
        #endregion

        private void OnEnable()
        {
            GemVeins = IdleSignals.Instance.onGetMineGemVeinTarget();
            GemDepot = IdleSignals.Instance.onGetMineDepotTarget();
            agent = GetComponent<NavMeshAgent>();
            currentMinerBaseState = MoveMiner;
            currentMinerBaseState.EnterState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            currentMinerBaseState.OnTriggerEnter(this, other);
        }

        public void SetTriggerAnim(MinerAnimTypes types)
        {
            animationController.SetAnim(types);
        }


        public void SwitchState(MinerStatesType state)
        {
            switch (state)
            {
                case MinerStatesType.MoveMine:
                    currentMinerBaseState = MoveMiner;
                    gem.SetActive(false);
                    pickaxe.SetActive(true);
                    break;
                case MinerStatesType.MoveDepot:
                    currentMinerBaseState = MoveDepot;
                    gem.SetActive(true);
                    pickaxe.SetActive(false);
                    break;
                case MinerStatesType.Dig:
                    currentMinerBaseState = DigMiner;
                    gem.SetActive(false);
                    pickaxe.SetActive(true);
                    break;
                case MinerStatesType.Gather:
                    currentMinerBaseState = DigMiner;
                    gem.SetActive(false);
                    pickaxe.SetActive(false);
                    break;
            }
            currentMinerBaseState.EnterState(this);
        }
    }
}