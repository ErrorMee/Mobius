using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgMediator : MobiusMediator
{
    [Inject]
    public MsgWindow view { get; set; }

    public override void OnRegister()
    {
        Debug.LogWarning("OnRegister");
        view.viewInteractSignal.AddListener(OnViewInteract);
    }

    private void OnViewInteract(string type1, GameObject type2)
    {
        switch (type1)
        {
            case MsgWindow.SHOW_MSG_COMPLETE:
                CloseWindow();
                break;
            default:
                break;
        }
    }

    public override void OnEnabled()
    {
        Debug.LogWarning("OnEnabled");
        base.OnEnabled();
    }

    public override void OnRemove()
    {
    }
}
