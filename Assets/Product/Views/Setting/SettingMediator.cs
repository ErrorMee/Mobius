using UnityEngine;
using UnityEngine.UI;

public class SettingMediator : MobiusMediator
{
    [Inject]
    public SettingWindow view { get; set; }
    public override void OnRegister()
    {
        view.viewInteractSignal.AddListener(OnViewInteract);
    }

    private void OnViewInteract(string type1, GameObject type2)
    {
        switch (type1)
        {
            case SettingWindow.CLICK_CLOSE:
                windowService.CloseWindow(WindowEnum.SettingWindow);
                break;
            case SettingWindow.CLICK_LOGOUT:
                windowService.OpenWindow(WindowEnum.MsgWindow, "UnLock");
                break;
            default:
                break;
        }
    }

    public override void OnRemove()
    {
        view.viewInteractSignal.RemoveListener(OnViewInteract);
    }
}