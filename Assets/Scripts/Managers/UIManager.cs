using UnityEngine;
using System.Collections.Generic;
public class UIManager
{
    // Singleton mode
    public static UIManager instance;

    /// <summary>
    /// 存储UI panel的栈结构
    /// </summary>
    public Stack<BasePanel> stack_ui;

    /// <summary>
    /// 存储panel的名称与物体的对应关系
    /// </summary>
    public Dictionary<string, GameObject> dict_uiobject;

    /// <summary>
    /// 当前场景下对应的canvas
    /// </summary>
    public GameObject canvasObj;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("UIManager实例不存在！");
            return instance;
        }
        return instance;
    }

    public UIManager()
    {
        instance = this;
        stack_ui = new Stack<BasePanel>();
        dict_uiobject = new Dictionary<string, GameObject>();
    }

    public GameObject GetSingleObject(UIType uIType)
    {
        if (dict_uiobject.ContainsKey(uIType.GetName))
        {
            return dict_uiobject[uIType.GetName];
        }
        if (canvasObj == null)
        {
            canvasObj = UIMethods.GetInstance().FindCanvas();
            return canvasObj;
        }

        GameObject gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uIType.GetPath),canvasObj.transform);
        return gameObject;
    }

    /// <summary>
    /// 往stack里压入一个panel
    /// </summary>
    /// <param name="panel">目标panel</param>
    public void Push(BasePanel panel)
    {
        Debug.Log($"{panel.uiType.GetName}被push进了stack");

        if (stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisable();
        }

        GameObject ui_object = GetSingleObject(panel.uiType);
        dict_uiobject.Add(panel.uiType.GetName, ui_object);
        panel.activeObj = ui_object;

        if (stack_ui.Count == 0)
        {
            stack_ui.Push(panel);
        }
        else
        {
            if (stack_ui.Peek().uiType.GetName != panel.uiType.GetName)
            {
                stack_ui.Push(panel);
            }
        }
        panel.OnStart();
    }

    /// <summary>
    /// 出栈
    /// </summary>
    /// <param name="isload">isload为真时pop全部；为假时pop栈顶</param>
    public void Pop(bool isload)
    {
        if (stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisable();
            stack_ui.Peek().OnDestroy();
            GameObject.Destroy(dict_uiobject[stack_ui.Peek().uiType.GetName]);
            dict_uiobject.Remove(stack_ui.Peek().uiType.GetName);
            stack_ui.Pop();
        }
        if (isload)
        {
            if (stack_ui.Count > 0) Pop(true);
        }
        else
        {
            if (stack_ui.Count > 0) stack_ui.Peek().OnEnable();
        }
    }
}
