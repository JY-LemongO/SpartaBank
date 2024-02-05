# SpartaBank
Unity 숙련 주차 개인과제1

## 구현 방식
* 씬 안에 Manager 하나만 두고 모든 오브젝트 Prefab 화 하여 동적생성  
  
![22](https://github.com/JY-LemongO/SpartaBank/assets/122505119/8fbeaebc-9d5c-48b2-b220-4f3e7a7d7317)

<details>
<summary>실행 전</summary>
<div markdown="1">

![33](https://github.com/JY-LemongO/SpartaBank/assets/122505119/8d415352-7510-484b-a5ad-e6f40172369d)
![55](https://github.com/JY-LemongO/SpartaBank/assets/122505119/8fc5d864-6a72-4d3b-ab75-3453a23414c9)

</div>
</details>

<details>
<summary>실행 후</summary>
<div markdown="1">

![44](https://github.com/JY-LemongO/SpartaBank/assets/122505119/516edce8-018c-4575-b2da-2a9ea5714704)
![66](https://github.com/JY-LemongO/SpartaBank/assets/122505119/1d8c7344-5ea1-49a9-a7be-1889d8cb37e6)

</div>
</details>   

## 주요 코드   
### UI_Base
  * 모든 UI 요소의 부모 클래스.
  * Bind 함수를 이용해 Button, Text 등 컴포넌트를 _objects에 저장할 수 있다.
```
protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

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
```
  * Get 함수를 이용해 원하는 컴포넌트를 가져올 수 있다.
```
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
```
<details>
<summary>사용예시</summary>
<div markdown="1">

```
enum Buttons
{
    K10_Btn,
    K30_Btn,
    K50_Btn,
    ManualDeposit_Btn,
    BackToATM_Btn,
}

enum InputFields
{
    DirectInput_IF,
}

private void Awake()
{
    Init();
}

public override bool Init()
{
    if (base.Init() == false)
        return false;

    BindButton(typeof(Buttons));
    BindInputField(typeof(InputFields));

    GetButton((int)Buttons.K10_Btn).onClick.AddListener(() => Managers.BM.Deposit(10000));
    GetButton((int)Buttons.K30_Btn).onClick.AddListener(() => Managers.BM.Deposit(30000)); 
    GetButton((int)Buttons.K50_Btn).onClick.AddListener(() => Managers.BM.Deposit(50000));
    GetButton((int)Buttons.ManualDeposit_Btn).onClick.AddListener(ManualDeposit);
    GetButton((int)Buttons.BackToATM_Btn).onClick.AddListener(BackToMain);        

    return true;
}
```

</div>
</details>
   
### UIManager
  * UI를 생성, 파괴할 수 있다.
  * Scene UI는 Canvas 컴포넌트가 있어 부모 설정없이 생성.
  * Main UI, Popup UI는 부모 @Root 하위로 생성.
  * Popup UI는 생성 시 알림문구를 설정하여 생성.
```
public GameObject Root
{
    get
    {
        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            GameObject go = Resources.Load<GameObject>($"Prefabs/@UI_Root").gameObject;
            root = Object.Instantiate(go);
            root.name = go.name;                
        }                
        return root;
    }
}

public void ShowSceneUI<T>(string path = null) where T : UI_Base => Util.Instantiate<T>(path);
public void ShowMenuUI<T>(string path = "Prefabs/UI") where T : UI_Base => Util.Instantiate<T>(path, Root.transform);
public void ShowPopupUI<T>(string alert, string path = "Prefabs/UI") where T : UI_Base
{
    UI_Popup popup = Util.Instantiate<T>(path, Root.transform).GetComponent<UI_Popup>();
    popup.SetupAlert(alert);
}

public void CloseUI(GameObject go) => Object.Destroy(go);
```   

### Util   
  * UI 뿐만 아니라 다른 곳에서도 쓰일 수 있는 static 클래스
  * GameObject의 자식을 찾거나, 매개변수로 넘겨받은 파일 위치의 GameObject Prefab을 생성.
```
public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
{
    if(go == null)
        return null;

    if (recursive == false)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Transform transform = go.transform.GetChild(i);
            if (string.IsNullOrEmpty(name) || transform.name == name)
            {
                T component = transform.GetComponent<T>();
                if(component != null)
                    return component;
            }                    
        }
    }
    else
    {
        foreach(T component in go.GetComponentsInChildren<T>())
        {
            if (string.IsNullOrEmpty(name) || component.name == name)
                return component;
        }
    }

    return null;
}

public static GameObject Instantiate<T>(string path = null, Transform parent = null) where T : UI_Base
{
    if (string.IsNullOrEmpty(path))
        path = "Prefabs";        

    string fileName = typeof(T).Name;

    GameObject prefab = Resources.Load<T>($"{path}/{fileName}").gameObject;
    
    GameObject go = Object.Instantiate(prefab, parent);
    go.name = prefab.name;

    return go;
}
```
   
### Managers
  * 싱글톤으로 구현된 모든 Manager를 관리할 클래스.
  * 유일하게 Monobehavior를 상속받아 하이어라키에 등록되어있는 Manager.
  * Managers를 통해 다른 Manager에 접근가능.
```
static Managers s_instace; // 유일한 매니저
static Managers Instance { get { Init(); return s_instace; } } // 유일한 매니저를 반환

UIManager       _ui = new UIManager();
BankManager     _bm = new BankManager();
AccountManager  _am = new AccountManager();

public static UIManager         UI => Instance?._ui;
public static BankManager       BM => Instance?._bm;
public static AccountManager    AM => Instance?._am;    

private void Awake()
{
    Init();
}

private static void Init() // 유일한 매니저 s_instance가 없으면 "@Managers" 를 찾아 반환, 못 찾으면 새로운 싱글톤 생성
{
    if (s_instace == null)
    {
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject("@Managers");
            go.AddComponent<Managers>();
        }

        s_instace = go.GetComponent<Managers>();
        
        AM.LoadAllAccounts();
        UI.ShowSceneUI<UI_Login>();
    }
}
```

### 각종 Manager
  * UIManager
    * UI 생성, 파괴 담당.
  * BankManager
    * ATM 입, 출금 및 현재 사용 계정의 계좌 갱신 담당.
  * AccountManager
    * 로그인, 회원가입, 계정저장 담당.

코드 생략


## 구현 된 사항
* Login 창
  * 로그인
  * 회원가입
    * 중복 아이디 불가, ID 영문 숫자 혼합, 글자 수 제한, 비밀번호 일치
    * 회원가입 시 PlayerPrefs 저장

* Main 창
  * 계정 정보
    * 이름, 현금, 계좌 잔액
  * 입, 출금
    * 입, 출금 시 PlayerPrefs 저장
  * Logout 시 Login 창으로 돌아감
  * 송금
    * 송금 시 PlayerPrefs 저장
    * ID 검색하여 해당 아이디 존재하면 송금

* 공통
  * 알림 창
    * 각 이벤트 시 팝업으로 알림창(UI_AlertPopup prefab) 표시
