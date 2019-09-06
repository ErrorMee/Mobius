using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowGroupComponent : MonoBehaviour
{
    [NonSerialized]
    public WindowGroupInfo m_WindowGroupInfo;

    private readonly LinkedList<WindowBaseView> m_Windows = new LinkedList<WindowBaseView>();

    /// <summary>
    /// 增加界面
    /// </summary>
    /// <param name="window"></param>
    public void AddWindow(WindowBaseView window)
    {
        m_Windows.AddLast(window);
    }

    /// <summary>
    /// 移除最后一个界面
    /// </summary>
    public void RemoveLastWindow()
    {
        m_Windows.RemoveLast();
    }

    /// <summary>
    /// 获取最后一个界面
    /// </summary>
    /// <returns></returns>
    public WindowBaseView GetLastWindow()
    {
        return m_Windows.Last.Value;
    }

    /// <summary>
    /// 获取距离最后一个的第几个
    /// </summary>
    /// <param name="toLastIndex"></param>
    /// <returns></returns>
    public WindowBaseView GetWindowToLast(int toLastIndex = 0)
    {
        return GetWindowToLastRecursion(toLastIndex, m_Windows.Last);
    }

    private WindowBaseView GetWindowToLastRecursion(int step, LinkedListNode<WindowBaseView> crtNode)
    {
        if (step > 0)
        {
            if (crtNode.Previous == null)
            {
                return null;
            }
            return GetWindowToLastRecursion(--step, crtNode.Previous);
        }
        else
        {
            return crtNode.Value;
        }
    }

    /// <summary>
    /// 获取所有界面
    /// </summary>
    /// <returns></returns>
    public LinkedList<WindowBaseView> GetAllWindows()
    {
        return m_Windows;
    }

    /// <summary>
    /// 移除界面
    /// </summary>
    /// <param name="window"></param>
    public void RemoveWindow(WindowBaseView window)
    {
        if (m_Windows.Contains(window))
        {
            m_Windows.Remove(window);
        }
        else
        {
            Debug.LogWarning("RemoveWindow " + window.name + " not in list");
        }
    }

    /// <summary>
    /// 查找界面
    /// </summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <returns>界面组中是否存在界面。</returns>
    public WindowBaseView GetWindow(int configID)
    {
        foreach (WindowBaseView windowBase in m_Windows)
        {
            if (windowBase.config.Id == configID)
            {
                return windowBase;
            }
        }
        return null;
    }

    
}
