using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgMediator : MobiusMediator
{
    [Inject]
    public MsgWindow view { get; set; }

    public override void PreRegister()
    {
        Debug.LogWarning("PreRegister");
    }

    public override void OnRegister()
    {
        Debug.LogWarning("OnRegister");
        view.viewInteractSignal.AddListener(OnViewInteract);

        view.ShowMsg("OnRegister");
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
        base.OnEnabled();
        Debug.LogWarning("OnEnabled");
    }

    public override void OnRemove()
    {
        Debug.LogWarning("OnRemove");
        view.viewInteractSignal.RemoveAllListeners();
    }

    public override void OnDisabled()
    {
        Debug.LogWarning("OnDisabled");
    }
}
