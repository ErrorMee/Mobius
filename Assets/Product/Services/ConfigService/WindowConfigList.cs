using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowConfigList : Singleton<WindowConfigList>
{
    private Dictionary<int, WindowConfig> m_ConfigDic;

    public void Init()
    {
        m_ConfigDic = new Dictionary<int, WindowConfig>();

        m_ConfigDic[(int)WindowEnum.DialogWindow] = new WindowConfig((int)WindowEnum.DialogWindow, "DialogWindow", 1);
        m_ConfigDic[(int)WindowEnum.MsgWindow] = new WindowConfig((int)WindowEnum.MsgWindow, "MsgWindow", 1);
        m_ConfigDic[(int)WindowEnum.AsyncLoadWindow] = new WindowConfig((int)WindowEnum.AsyncLoadWindow, "AsyncLoadWindow", 1);
        
        m_ConfigDic[(int)WindowEnum.MainWindow] = new WindowConfig((int)WindowEnum.MainWindow, "MainWindow", -1);
        m_ConfigDic[(int)WindowEnum.TaskWindow] = new WindowConfig((int)WindowEnum.TaskWindow, "TaskWindow", -1, true);
        m_ConfigDic[(int)WindowEnum.FightWindow] = new WindowConfig((int)WindowEnum.FightWindow, "FightWindow", -1, true);
        m_ConfigDic[(int)WindowEnum.SettingWindow] = new WindowConfig((int)WindowEnum.SettingWindow, "SettingWindow", 0);
    }

    /// <summary>
    /// 获取数据表行。
    /// </summary>
    /// <param name="id">数据表行的编号。</param>
    /// <returns>数据表行。</returns>
    public WindowConfig GetConfig(int id)
    {
        WindowConfig config = null;
        if (m_ConfigDic.TryGetValue(id, out config))
        {
            return config;
        }
        return null;
    }
}
