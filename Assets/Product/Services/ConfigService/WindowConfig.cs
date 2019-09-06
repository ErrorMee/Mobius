using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowConfig
{
    /// <summary>
    /// 编号
    /// </summary>
    public int Id
    {
        get;
        private set;
    }

    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName
    {
        get;
        private set;
    }

    /// <summary>
    /// 界面组
    /// </summary>
    public int WindowGroup
    {
        get;
        private set;
    }

    /// <summary>
    /// 暂停被其覆盖的界面
    /// </summary>
    public bool PauseCovered
    {
        get;
        private set;
    }

    /// <summary>
    /// 单例
    /// </summary>
    public bool Singleton
    {
        get;
        private set;
    }

    public WindowConfig(int id, string assetName,
        int windowGroup, bool pauseCovered = false, bool singleton = true)
    {
        Id = id;
        AssetName = assetName;
        WindowGroup = windowGroup;
        PauseCovered = pauseCovered;
        Singleton = singleton;
    }
}
