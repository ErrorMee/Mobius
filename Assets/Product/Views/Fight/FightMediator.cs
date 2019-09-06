using UnityEngine;
using UnityEngine.UI;

public class FightMediator : MobiusMediator
{
    [Inject]
    public FightWindow view { get; set; }
    public override void OnRegister()
    {
        view.viewInteractSignal.AddListener(OnViewInteract);
    }

    private void OnViewInteract(string type1, GameObject type2)
    {
        Debug.Log(type1);
        switch (type1)
        {
            case FightWindow.CLICK_CLOSE:
                CloseWindow();
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