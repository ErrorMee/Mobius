using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowModel
{
    /// <summary>
    /// 打开界面时需要参数
    /// </summary>
    private Dictionary<WindowEnum, object> m_OpenWindowDatas = new Dictionary<WindowEnum, object>();

    public void AddOpenWindowData(WindowEnum windowEnum, object userData)
    {
        m_OpenWindowDatas[windowEnum] = userData;
    }


    public object GetOpenWindowData(WindowEnum windowEnum)
    {
        if (!m_OpenWindowDatas.ContainsKey(windowEnum))
        {
            return null;
        }
        return m_OpenWindowDatas[windowEnum];
    }
}
