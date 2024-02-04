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
        Init(); // 상속받은 UI 에서 Awake로 호출하고 호출 안 됐으면 그냥 true로 전환
    }

    #region Bind Type
    /// <summary>
    /// 넘겨준 Enum Type안의 모든 Name과 일치하는 하위 GameObject의 T 타입을 바인드
    /// </summary>
    protected void Bind<T>(Type type) where T : UnityEngine.Object // Enum의 Type을 넘겨주면 Enum안의 모든 Name과 같은 자식 오브젝트들을 검색, T 타입을 찾아 _objects에 Type키에 저장
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
    /// 넘겨준 index와 같은 Name의 오브젝트를 T 타입으로 가져옴
    /// </summary>    
    protected T Get<T>(int index) where T : UnityEngine.Object // Enum의 index를 넘겨주면 T Type키의 index번째 Object를 T 타입으로 캐스팅하여 반환
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
