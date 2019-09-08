using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class WindowService : MonoService
{

    [Inject]
    public WindowServiceReadySignal windowServiceReadySignal { get; set; }
    [Inject]
    public WindowModel windowModel { get; set; }

    [SerializeField]
    private Transform m_CanvasRoot = null;
    [SerializeField]
    private WindowGroupInfo[] m_WindowGroupInfos = null;
    
    private static ushort VIEW_GROUP_DEPTH_FACTOR = 10000;

    private readonly Dictionary<int, WindowGroupComponent> m_WindowGroupComponents = new Dictionary<int, WindowGroupComponent>();

    private void Start()
    {
        Debug.Log("WindowService Start");
        InitWindowGroup();

        windowServiceReadySignal.Dispatch();
    }

    private void InitWindowGroup()
    {
        for (int i = 0; i < m_WindowGroupInfos.Length; i++)
        {
            AddWindowGroup(m_WindowGroupInfos[i]);
        }
    }

    /// <summary>
    /// 添加界面组
    /// </summary>
    /// <param name="windowGroupEnum"></param>
    private void AddWindowGroup(WindowGroupInfo windowGroupInfo)
    {
        GameObject windowGroupObj = new GameObject(windowGroupInfo.Name);

        windowGroupObj.GetOrAddComponent<GraphicRaycaster>();
        RectTransform rectTranform = windowGroupObj.GetOrAddComponent<RectTransform>();
        rectTranform.anchorMin = Vector2.zero;
        rectTranform.anchorMax = Vector2.one;
        rectTranform.sizeDelta = Vector2.zero;
        rectTranform.SetParent(m_CanvasRoot, false);
        rectTranform.gameObject.layer = LayerMask.NameToLayer("UI");

        Canvas canvas = windowGroupObj.GetOrAddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = VIEW_GROUP_DEPTH_FACTOR * windowGroupInfo.Depth;

        WindowGroupComponent windowGroupComponent = windowGroupObj.GetOrAddComponent<WindowGroupComponent>();
        windowGroupComponent.m_WindowGroupInfo = windowGroupInfo;
        m_WindowGroupComponents.Add(windowGroupInfo.Depth, windowGroupComponent);
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="windowEnum"></param>
    /// <param name="userData"></param>
    public void OpenWindow(WindowEnum windowEnum, object userData = null)
    {
        WindowConfig config = WindowConfigList.Instance.GetConfig((int)windowEnum);
        WindowGroupComponent windowGroupComponent = m_WindowGroupComponents[config.WindowGroup];

        if (config.Singleton)
        {
            WindowBaseView windowBase = GetWindow(windowEnum);
            if (windowBase != null)
            {
                return;
            }
        }

        windowModel.AddOpenWindowData(windowEnum, userData);

        string assetPath = AssetPathUtil.GetWindowAssetPath(config.AssetName);

        Addressables.InstantiateAsync(assetPath, windowGroupComponent.transform, false).Completed += (aoh) =>
        {
            Debug.LogWarning("111");
            WindowBaseView baseWindow = aoh.Result.GetOrAddComponent<WindowBaseView>();
            baseWindow.config = config;

            if (config.PauseCovered)
            {
                LinkedList<WindowBaseView> allWindows = windowGroupComponent.GetAllWindows();
                foreach (WindowBaseView window in allWindows)
                {
                    if (window.isActiveAndEnabled)
                    {
                        window.gameObject.SetActive(false);
                    }
                }
            }

            windowGroupComponent.AddWindow(baseWindow);
            MobiusMediator mobiusMediator = aoh.Result.GetComponent<MobiusMediator>();
            mobiusMediator.windowEnum = windowEnum;
            Debug.LogWarning("222");
        };
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="windowEnum"></param>
    public void CloseWindow(WindowEnum windowEnum)
    {
        WindowBaseView windowBase = GetWindow(windowEnum);
        if (windowBase == null)
        {
            return;
        }
        WindowConfig config = WindowConfigList.Instance.GetConfig((int)windowEnum);
        WindowGroupComponent windowGroupComponent = m_WindowGroupComponents[config.WindowGroup];
        WindowBaseView windowPrevious = windowGroupComponent.GetWindowToLast(1);
        if (windowPrevious != null)
        {
            if (!windowPrevious.isActiveAndEnabled)
            {
                windowPrevious.gameObject.SetActive(true);
            }
        }
        Addressables.ReleaseInstance(windowBase.gameObject);
        windowGroupComponent.RemoveWindow(windowBase);
    }

    /// <summary>
    /// 查找界面
    /// </summary>
    /// <param name="windowEnum"></param>
    /// <returns></returns>
    public WindowBaseView GetWindow(WindowEnum windowEnum)
    {
        foreach (WindowGroupComponent windowGroupComponent in m_WindowGroupComponents.Values)
        {
            WindowBaseView windowBase = windowGroupComponent.GetWindow((int)windowEnum);
            if (windowBase != null)
            {
                return windowBase;
            }
        }
        return null;
    }
}
