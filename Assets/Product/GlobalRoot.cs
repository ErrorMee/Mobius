using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

public class GlobalRoot : MobiusContextView
{
    void Awake()
    {
        context = new GlobalContext(this);
    }
}
public class GlobalContext : MobiusContext
{
    public GlobalContext(MonoBehaviour view) : base(view)
    {
    }

    protected override void mapBindings()
    {
        Debug.Log("GlobalContext mapBindings");
        GlobalRoot globalRoot = (contextView as GameObject).GetComponent<GlobalRoot>();
        //command
        commandBinder.Bind<StartSignal>().To<GlobalStartCommand>().Once();
        commandBinder.Bind<WindowServiceReadySignal>().To<WindowServiceReadyCommand>().Once();

        //Service
        WindowService windowService = globalRoot.GetComponentInChildren<WindowService>();
        injectionBinder.injector.Inject(windowService);
        injectionBinder.Bind<WindowService>().ToValue(windowService);

        //Signal
        injectionBinder.Bind<ViewInteractSignal>().To<ViewInteractSignal>();

        //View
        mediationBinder.Bind<MainWindow>().To<MainMediator>();
        mediationBinder.Bind<TaskWindow>().To<TaskMediator>();
        mediationBinder.Bind<FightWindow>().To<FightMediator>();
        mediationBinder.Bind<SettingWindow>().To<SettingMediator>();
        mediationBinder.Bind<MsgWindow>().To<MsgMediator>();
    }
}