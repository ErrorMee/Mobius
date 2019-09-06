using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

public class MobiusContext : MVCSContext
{
    public MobiusContext(MonoBehaviour view) : base(view)
    {
    }

    public MobiusContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        //EventCommandBinder
        //injectionBinder.Unbind<ICommandBinder>(); //Unbind to avoid a conflict!
        //injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();
        //SignalCommandBinder
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    protected override void mapBindings()
    {
        base.mapBindings();
        //todo
    }

    override public IContext Start()
    {
        base.Start();
        StartSignal startSignal = injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        return this;
    }
}
