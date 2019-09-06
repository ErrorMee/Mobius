using System;
using UnityEngine;

[Serializable]
public class WindowGroupInfo 
{
    [SerializeField]
    private string m_Name = "null";
    [SerializeField]
    private int m_Depth = -1;
    [SerializeField]
    private string m_Desc = string.Empty;

    public string Name
    {
        get
        {
            return m_Name;
        }
    }

    public int Depth
    {
        get
        {
            return m_Depth;
        }
    }

    public string Desc
    {
        get
        {
            return m_Desc;
        }
    }
}
