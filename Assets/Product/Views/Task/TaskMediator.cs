using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMediator : MobiusMediator
{
    [Inject]
    public TaskWindow view { get; set; }

    public override void OnRegister()
    {
        view.viewInteractSignal.AddListener(OnViewInteract);
    }

    private void OnViewInteract(string type1, GameObject type2)
    {
        Debug.Log(type1);
        switch (type1)
        {
            case TaskWindow.CLICK_FIGHT:
                windowService.OpenWindow(WindowEnum.FightWindow);
                break;
            case FightWindow.CLICK_CLOSE:
                CloseWindow();
                break;
            default:
                break;
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        view.viewInteractSignal.RemoveAllListeners();
    }
}
