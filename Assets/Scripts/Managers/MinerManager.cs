using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using States.MinerStates;
using UnityEngine;
using UnityEngine.AI;

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


    public void SwitchState(MinerBaseState state)
    {
        currentMinerBaseState = state;
        currentMinerBaseState.EnterState(this);
    }
}