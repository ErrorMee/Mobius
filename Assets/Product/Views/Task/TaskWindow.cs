using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskWindow : WindowBaseView
{
    public Button closeBtn;
    public Button fightBtn;

    internal const string CLICK_CLOSE = "CLICK_CLOSE";
    internal const string CLICK_FIGHT = "CLICK_FIGHT";

    protected override void Awake()
    {
        base.Awake();
        EventTriggerListener.Get(closeBtn.gameObject).onClick = OnClickClose;
        EventTriggerListener.Get(fightBtn.gameObject).onClick = OnClickFight;
    }

    private void OnClickClose(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_CLOSE, go);
    }

    private void OnClickFight(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_FIGHT, go);
    }

}
