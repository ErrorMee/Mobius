using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMediator : MobiusMediator
{
    [Inject]
    public MainWindow view { get; set; }

    public override void OnRegister()
    {
        view.viewInteractSignal.AddListener(OnViewInteract);
    }

    private void OnViewInteract(string type1, GameObject type2)
    {
        switch (type1)
        {
            case MainWindow.CLICK_TASK:
                windowService.OpenWindow(WindowEnum.TaskWindow);
                break;
            case MainWindow.CLICK_SETTING:
                windowService.OpenWindow(WindowEnum.SettingWindow);
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
