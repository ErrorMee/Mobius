using UnityEngine;
using UnityEngine.UI;

public class FightWindow : WindowBaseView
{
    public Button closeBtn;

    internal const string CLICK_CLOSE = "CLICK_CLOSE";

    protected override void Awake()
    {
        base.Awake();

        EventTriggerListener.Get(closeBtn.gameObject).onClick = OnClickClose;
    }

    private void OnClickClose(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_CLOSE, go);
    }

}