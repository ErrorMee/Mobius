using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgWindow : WindowBaseView
{
    [SerializeField]
    private Text text;

    internal const string SHOW_MSG_COMPLETE = "SHOW_MSG_COMPLETE";

    public void ShowMsg(string msg)
    {
        text.text = msg;

        transform.DOMoveY(30, 1.2f, false).onComplete += OnShowMsgComplete;
    }

    private void OnShowMsgComplete()
    {
        viewInteractSignal.Dispatch(SHOW_MSG_COMPLETE, gameObject);
    }
}
