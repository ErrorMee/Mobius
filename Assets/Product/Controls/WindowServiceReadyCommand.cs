using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowServiceReadyCommand : SignalCommand
{
    [Inject]
    public WindowService windowService { get; set; }

    public override void Execute()
    {
        Debug.Log("WindowServiceReadyCommand Execute");

        windowService.OpenWindow(WindowEnum.MainWindow);
    }
}
