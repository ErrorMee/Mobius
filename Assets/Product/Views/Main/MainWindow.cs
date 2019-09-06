
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : WindowBaseView
{
    public Button taskBtn;
    public Button settingBtn;

    internal const string CLICK_TASK = "CLICK_TASK";
    internal const string CLICK_SETTING = "CLICK_SETTING";

    protected override void Awake()
    {
        base.Awake();

        EventTriggerListener.Get(taskBtn.gameObject).onClick = OnClickTask;
        EventTriggerListener.Get(settingBtn.gameObject).onClick = OnClickSetting;
    }

    private void OnClickTask(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_TASK, go);
    }

    private void OnClickSetting(GameObject go)
    {
        viewInteractSignal.Dispatch(CLICK_SETTING, go);
    }
}
