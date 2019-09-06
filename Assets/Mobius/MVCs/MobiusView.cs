using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobiusView : View
{
    [Inject]
    public ViewInteractSignal viewInteractSignal { get; set; }
}
