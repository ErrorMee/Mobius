using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 列表功能：定容、去重、按访问排序
/// </summary>
/// <typeparam name="T"></typeparam>
public class SmartList<T> : List<T>
{

    private uint maxCount = 0;
    /// <summary>
    /// 定容：最大容量，默认0无限大，超出做队列弹出
    /// </summary>
    public uint MaxCount
    {
        get
        {
            return maxCount;
        }

        set
        {
            maxCount = value;
        }
    }

    private bool repeatItems = false;
    /// <summary>
    /// 去重：是否支持重复元素
    /// </summary>
    public bool RepeatItems
    {
        get
        {
            return repeatItems;
        }

        set
        {
            repeatItems = value;
        }
    }

    private bool usearrange = true;
    /// <summary>
    /// 按访问排序：在去重时生效。例如（1，2）add 1 =（2，1）
    /// </summary>
    public bool Usearrange
    {
        get
        {
            return usearrange;
        }

        set
        {
            usearrange = value;
        }
    }

    public SmartList(uint _MaxCount, int _Capacity) : base(_Capacity)
    {
        MaxCount = _MaxCount;
    }

    public new void Add(T item)
    {
        if (RepeatItems)
        {
            base.Add(item);
        }
        else
        {
            int idx = base.IndexOf(item);
            if (idx >= 0)
            {
                if (Usearrange)
                {
                    base.RemoveAt(idx);
                    base.Add(item);
                }
            }
            else
            {
                base.Add(item);
            }
        }
    }

    /// <summary>
    /// 添加元素+返回超出元素
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public T PushPop(T item)
    {
        this.Add(item);
        if (MaxCount > 0)
        {
            if (Count > MaxCount)
            {
                T temp = this[0];
                RemoveAt(0);
                return temp;
            }
            else
            {
                return default(T);
            }
        }
        else
        {
            return default(T);
        }
    }
}
