using System.Collections;
using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoSingleton<UISignals>
{
    public UnityAction<UIPanels> onOpenPanel = delegate { };
    public UnityAction<UIPanels> onClosePanel = delegate { };
    public UnityAction<int> onSetBaseText = delegate { };
    public UnityAction<float> onSetMoneyText = delegate { };
    public UnityAction<float> onSetGemText = delegate { };
    
    
}