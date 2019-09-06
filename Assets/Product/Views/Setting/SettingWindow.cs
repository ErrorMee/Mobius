using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : WindowBaseView
{
    [SerializeField]
    private Button closeBtn;

    [SerializeField]
    private Button logOutBtn;

    internal const string CLICK_CLOSE = "CLICK_CLOSE";
    internal const string CLICK_LOGOUT = "CLICK_LOGOUT";

    protected override void Awake()
    {
        base.Awake();

        EventTriggerListener.Get(closeBtn.gameObject).onClick = OnClickClose;
        EventTriggerListener.Get(logOutBtn.gameObject).onClick = OnClickLogOut;
    }

    private void OnClickClose(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_CLOSE, go);
    }

    private void OnClickLogOut(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_LOGOUT, go);
    }
}