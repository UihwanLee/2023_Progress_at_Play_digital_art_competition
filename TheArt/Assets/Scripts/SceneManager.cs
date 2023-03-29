using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SceneManager : MonoBehaviour
{
    private int sceneIndex;
    private UIManager uiManager;

    [SerializeField]
    private GameObject loadingUI;

    // Player
    [Header("Player")]
    [SerializeField]
    private GameObject playerTextUI;
    [SerializeField]
    private Animator playerAnim;

    [Header("Ending")]
    [SerializeField]
    private GameObject Ending01UI;
    [SerializeField]
    private GameObject[] Ending03TextUI;
    [SerializeField]
    private GameObject[] Ending03UI;

    [Header("Camera")]
    [SerializeField]
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = 0;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        InitPlayerTextUI();
        InitEnding();

        resetAlpha(loadingUI);

        // Scene0 시작
        Scene01();
        mainCamera.orthographicSize = 5;
    }

    private void InitEnding()
    {
        Ending01UI.SetActive(true);
        Ending01UI.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        Ending01UI.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().material = null;
        Ending01UI.SetActive(false);


        Ending03TextUI[0].SetActive(true);
        Ending03TextUI[1].SetActive(true);
        Ending03TextUI[2].SetActive(true);
        Ending03TextUI[2].GetComponent<CanvasGroup>().alpha = 0.0f;
        Ending03TextUI[3].SetActive(true);
        Ending03TextUI[3].GetComponent<CanvasGroup>().alpha = 0.0f;
        Ending03TextUI[0].SetActive(false);

        Ending03UI[0].SetActive(true);
        Ending03UI[1].SetActive(true);
        Ending03UI[2].SetActive(true);

        Ending03UI[2].GetComponent<SpriteRenderer>().sprite = null;
        Ending03UI[2].GetComponent<SpriteRenderer>().material = null;
        Color tmp = Ending03UI[2].GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        Ending03UI[2].GetComponent<SpriteRenderer>().color = tmp;

        Ending03UI[0].SetActive(false);
    }

    private void resetAlpha(GameObject canvas)
    {
        canvas.SetActive(true);
        canvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        canvas.SetActive(false);
    }


    public void Loading()
    {
        StartCoroutine(LoadingCoroutine());
    }

    // 로딩 씬 구현
    IEnumerator LoadingCoroutine()
    {
        loadingUI.SetActive(true);

        // Fade
        CanvasGroup cg = loadingUI.GetComponent<CanvasGroup>();
        StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 3f));
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeCourtine(cg, cg.alpha, 0, 3f));

        yield return new WaitForSeconds(6f);
        loadingUI.SetActive(false);
    }

    IEnumerator FadeCourtine(CanvasGroup _cg, float _start, float _end, float _duration)
    {
        float counter = 0.0f;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            _cg.alpha = Mathf.Lerp(_start, _end, counter / _duration);

            yield return null;
        }

        // TO DO
    }

    private void InitPlayerTextUI()
    {
        playerTextUI.SetActive(true);
        playerTextUI.transform.GetChild(0).GetComponent<Text>().text = "";
        playerTextUI.SetActive(false);
    }

    // Scene0 : 공모전을 찾아보고 InGame 이동

    // Scene1 : 무엇을 그릴지 생각
    public void Scene01()
    {
        sceneIndex++;
        StartCoroutine(Scene01Coroutine());
    }

    IEnumerator Scene01Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);


        yield return SECOND_ONE;

        playerAnim.SetTrigger("OK");

        yield return SECOND_THREE;


        playerTextUI.SetActive(true);

        string text = "Let me think about Picture...";
        StartCoroutine(Typing(playerTextUI.transform.GetChild(0).GetComponent<Text>(), text, 0.1f));

        yield return SECOND_THREE;
        yield return SECOND_THREE;
        playerTextUI.SetActive(false);

        ThinkPicture(uiManager.GetQuestion(), "What's Draw?", uiManager.GetSelectPictureUI(), false);
    }

    // Scene2 : 어떤 색깔 조합으로 색칠할지 생각
    public void Scene02()
    {
        sceneIndex++;
        StartCoroutine(Scene02Coroutine());
    }

    IEnumerator Scene02Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);


        yield return SECOND_ONE;

        playerAnim.SetTrigger("OK");

        yield return SECOND_THREE;

        playerTextUI.SetActive(true);

        string text = "Let me think about Color...";
        StartCoroutine(Typing(playerTextUI.transform.GetChild(0).GetComponent<Text>(), text, 0.1f));

        yield return SECOND_THREE;
        yield return SECOND_THREE;
        playerTextUI.SetActive(false);

        ThinkPicture(uiManager.GetQuestion(), "What's Color?", uiManager.GetSelectColorUI(), true);
    }

    public void ThinkPicture(Text _textUI, string _text, GameObject _selectUI, bool _option)
    {
        StartCoroutine(ThinkPictureCoroutine(_textUI, _text, _selectUI, _option));
    }

    IEnumerator ThinkPictureCoroutine(Text _textUI, string _text, GameObject _selectUI, bool _option)
    {
        var SECOND_ONE = new WaitForSeconds(1f);

        yield return SECOND_ONE;
        uiManager.SetETCUI(true, false);
        yield return new WaitForSeconds(0.5f);
        uiManager.SetETCUI(true, true);
        yield return new WaitForSeconds(0.6f);
        uiManager.SetSelectCanvasUI(true);
        yield return SECOND_ONE;
        StartCoroutine(Typing(_textUI, _text, 0.05f));
        yield return SECOND_ONE;
        _selectUI.SetActive(true);
        if (_option) uiManager.ResetColorUI();
    }

    // Ending1
    public void Ending_01()
    {
        StartCoroutine(Ending_01Coroutine());
    }

    IEnumerator Ending_01Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);
        var SECOND_FIVE = new WaitForSeconds(5f);

        yield return SECOND_ONE;

        playerAnim.SetTrigger("OK");

        yield return SECOND_THREE;

        playerTextUI.SetActive(true);

        string text1 = "It's Done";
        string text2 = " . . . ?";
        for (int i = 0; i <= text1.Length; i++)
        {
            playerTextUI.transform.GetChild(0).GetComponent<Text>().text = text1.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i <= text2.Length; i++)
        {
            playerTextUI.transform.GetChild(0).GetComponent<Text>().text = text1 + text2.Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }

        yield return SECOND_THREE;
        playerTextUI.SetActive(false);

        yield return SECOND_ONE;


        playerAnim.SetTrigger("Done");
        yield return SECOND_FIVE;


        // Fade in/out
        Loading();


        yield return SECOND_THREE;
        mainCamera.orthographicSize = 1.32f;
        yield return SECOND_ONE;
        Ending_02();
    }

    // Ending2
    public void Ending_02()
    {
        Ending01UI.SetActive(true);

        // Canvas Set
        SetCanvas(Ending01UI.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>());

        StartCoroutine(Ending_02Coroutine());
    }

    IEnumerator Ending_02Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);
        var SECOND_FIVE = new WaitForSeconds(5f);

        yield return SECOND_FIVE;


        // Camera Zoom Out

        // Start -> 1.32 
        // End -> 20

        float counter = 0.0f;
        float start = 1.32f, end = 12.0f;
        float duration = 15.0f;


        while (counter < duration)
        {
            counter += Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }


        yield return SECOND_FIVE;

        // Player 화면 비추기

        mainCamera.orthographicSize = 5f;
        Ending_03();
    }

    public void Ending_03()
    {
        Ending03UI[0].SetActive(true);
        Ending03TextUI[0].SetActive(true);
        Ending03UI[2].SetActive(true);

        // Canvas Set
        SetCanvas(Ending03UI[2].GetComponent<SpriteRenderer>());

        StartCoroutine(Ending_03Coroutine());
    }

    IEnumerator Ending_03Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);
        var SECOND_FIVE = new WaitForSeconds(5f);


        // 완성된 그림 불러오기

        // Picture Drawing
        StartCoroutine(FadePictureCourtine(Ending03UI[2].GetComponent<SpriteRenderer>(), 0, 1, 5f));

        yield return SECOND_FIVE;

        CanvasGroup cg = Ending03TextUI[2].GetComponent<CanvasGroup>();
        StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 2f));

        yield return SECOND_THREE;

        cg = Ending03TextUI[3].GetComponent<CanvasGroup>();
        StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 2f));

        yield return SECOND_FIVE;

        // 시작 화면으로 돌아가기
    }

    IEnumerator FadePictureCourtine(SpriteRenderer _sr , float _start, float _end, float _duration)
    {
        float counter = 0.0f;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            Color tmp = _sr.color;
            tmp.a = Mathf.Lerp(_start, _end, counter / _duration);
            _sr.color = tmp;

            yield return null;
        }

        // TO DO
    }

    // 자막 타이핑 이펙트
    IEnumerator Typing(Text _textUI, string _text, float _time)
    {
        _textUI.text = "";
        for (int i = 0; i <= _text.Length; i++)
        {
            _textUI.text = _text.Substring(0, i);
            //textBox.GetComponent<Text>().text = text.Substring(0, i);
            yield return new WaitForSeconds(_time);
        }
    }

    public void SetCanvas(SpriteRenderer _sr)
    {
        _sr.sprite = uiManager.GetCurSprite();
        _sr.material = uiManager.GetMaterial();
        _sr.material.SetColor("_PictureColor1", uiManager.GetMainColor());
        _sr.material.SetColor("_PictureColor2", uiManager.GetSubColor());
    }

    public int GetCurSceneIndex() { return sceneIndex; }
}
