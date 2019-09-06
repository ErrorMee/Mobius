
public enum WindowEnum 
{
    Undefined = 0,

    #region 通用界面（1~99）
    /// <summary>
    /// 弹出框。
    /// </summary>
    DialogWindow = 1,

    /// <summary>
    /// 消息框。
    /// </summary>
    MsgWindow = 2,

    /// <summary>
    /// 异步加载界面。
    /// </summary>
    AsyncLoadWindow = 3,

    #endregion

    #region 功能界面，每个大功能预留100供子功能使用
    /// <summary>
    /// 登陆界面。
    /// </summary>
    LoginWindow = 100,

    /// <summary>
    /// 主界面。
    /// </summary>
    MainWindow = 200,

    /// <summary>
    /// 任务界面
    /// </summary>
    TaskWindow = 300,

    /// <summary>
    /// 战斗界面。
    /// </summary>
    FightWindow = 400,

    /// <summary>
    /// 设置界面。
    /// </summary>
    SettingWindow = 500,

    #endregion
}
