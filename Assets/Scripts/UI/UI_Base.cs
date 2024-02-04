using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    protected bool _init = false;    

    public virtual bool Init()
    {
        if (_init)
            return false;        

        _init = true;
        return true;
    }

    private void Start()
    {
        Init(); // ��ӹ��� UI ���� Awake�� ȣ���ϰ� ȣ�� �� ������ �׳� true�� ��ȯ
    }

    #region Bind Type
    /// <summary>
    /// �Ѱ��� Enum Type���� ��� Name�� ��ġ�ϴ� ���� GameObject�� T Ÿ���� ���ε�
    /// </summary>
    protected void Bind<T>(Type type) where T : UnityEngine.Object // Enum�� Type�� �Ѱ��ָ� Enum���� ��� Name�� ���� �ڽ� ������Ʈ���� �˻�, T Ÿ���� ã�� _objects�� TypeŰ�� ����
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    protected void BindText(Type type) => Bind<Text>(type);
    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindInputField(Type type) => Bind<InputField>(type);
    #endregion 

    #region Get Type
    /// <summary>
    /// �Ѱ��� index�� ���� Name�� ������Ʈ�� T Ÿ������ ������
    /// </summary>    
    protected T Get<T>(int index) where T : UnityEngine.Object // Enum�� index�� �Ѱ��ָ� T TypeŰ�� index��° Object�� T Ÿ������ ĳ�����Ͽ� ��ȯ
    {
        UnityEngine.Object[] objs;

        if (_objects.TryGetValue(typeof(T), out objs) == false)
            return null;

        return objs[index] as T;
    }

    protected Text GetText(int index) => Get<Text>(index);
    protected Button GetButton(int index) => Get<Button>(index);
    protected InputField GetInputField(int index) => Get<InputField>(index);
    #endregion    
}
