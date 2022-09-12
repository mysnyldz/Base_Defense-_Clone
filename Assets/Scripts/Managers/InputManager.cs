using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.InputData;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("Data")] public InputData Data;

    #endregion Public Variables

    public Vector3? MousePosition;

    #region Serialized Variables

    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;

    #endregion Serialized Variables

    #region Private Variables

    private bool _isTouching;
    private Vector3 _joystickPos;

    #endregion Private Variables

    #endregion Self Variables

    private void Awake()
    {
        Data = GetInputData();
    }

    private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion Event Subscriptions

    private void Update()
    {
        if (!isReadyForTouch) return;

        if (Input.GetMouseButtonUp(0))
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!isFirstTimeTouchTaken)
            {
                isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            MousePosition = Input.mousePosition;
        }

       if (Input.GetMouseButton(0))
       {
           _isTouching = true;

           if (_isTouching)
           {
               _joystickPos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
               InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
               {
                   InputValues = _joystickPos
               });
           }
       }
    }

    private void OnPlay() => isReadyForTouch = true;

    private void OnEnableInput() => isReadyForTouch = true;

    private void OnDisableInput() => isReadyForTouch = false;

    private void OnReset()
    {
        _isTouching = false;
        isReadyForTouch = false;
    }

    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}