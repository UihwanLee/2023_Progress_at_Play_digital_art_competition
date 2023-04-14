using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private int sceneIndex;
    private UIManager uiManager;

    [SerializeField]
    private GameObject loadingUI;
    [SerializeField]
    private GameObject startScene;

    // Player
    [Header("Player")]
    [SerializeField]
    private GameObject playerTextUI;
    [SerializeField]
    private Animator otherPlayerAnim;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private Animator playerAnim2;
    [SerializeField]
    private GameObject playerLastMessage;

    [Header("Start")]
    [SerializeField]
    private GameObject playerStartMessage;

    [Header("Ending01")]
    [SerializeField]
    private GameObject Ending01UI;
    [SerializeField]
    private GameObject[] homePageUI;
    [SerializeField]
    private GameObject otherPlayerThinkUI;
    [SerializeField]
    private GameObject otherPlayerComputerText;
    [SerializeField]
    private GameObject uploadPicture;
    [SerializeField]
    private GameObject line;
    [SerializeField]
    private GameObject mousePoint;
    [SerializeField]
    private Animator otherPlayerEndingAnim;

    [Header("Ending02")]
    [SerializeField]
    private GameObject Ending02UI;
    [SerializeField]
    private GameObject Ending02FadeUI;
    [SerializeField]
    private GameObject[] playerObjects;

    [Header("Ending03")]
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

        startScene.SetActive(false);
        playerStartMessage.SetActive(true);
        playerStartMessage.transform.GetChild(0).GetComponent<Text>().text = "";
        playerStartMessage.SetActive(false);

        InitPlayerTextUI();
        InitEnding();

        resetAlpha(loadingUI);

        // Scene0 ����
        Scene00();
        mainCamera.orthographicSize = 5;
    }

    private void InitEnding()
    {
        Ending01UI.SetActive(true);
        for(int i=0; i<homePageUI.Length; i++)
        {
            homePageUI[i].SetActive(false); 
        }
        uploadPicture.SetActive(false);
        line.SetActive(false);
        otherPlayerThinkUI.SetActive(false);
        otherPlayerComputerText.SetActive(true);
        otherPlayerComputerText.GetComponent<Text>().text = "";
        otherPlayerComputerText.SetActive(false);
        Ending01UI.SetActive(false);


        Ending02UI.SetActive(true);
        Ending02UI.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        Ending02UI.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().material = null;

        resetAlpha(Ending02FadeUI);
        playerLastMessage.SetActive(true);
        playerLastMessage.GetComponent<Text>().text = "";
        playerLastMessage.SetActive(false);
        Ending02UI.SetActive(false);


        Ending03TextUI[0].SetActive(true);
        Ending03TextUI[1].SetActive(true);
        Ending03TextUI[2].SetActive(true);
        Ending03TextUI[2].GetComponent<CanvasGroup>().alpha = 0.0f;
        Ending03TextUI[3].SetActive(true);
        Ending03TextUI[3].GetComponent<CanvasGroup>().alpha = 0.0f;
        Ending03TextUI[4].SetActive(true);
        Ending03TextUI[4].GetComponent<Text>().text = "";
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

    // �ε� �� ����
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

    // Scene0 : �������� ã�ƺ��� InGame �̵�

    public void Scene00()
    {
        startScene.SetActive(true);
        StartCoroutine(Scene00Coroutine());
    }

    IEnumerator Scene00Coroutine()
    {
        var SECOND_SIX = new WaitForSeconds(6f);
        var SECOND_TWO = new WaitForSeconds(2f);

        yield return new WaitForSeconds(2.2f);
        // ���콺 Ŭ�� �̺�Ʈ
        AudioManager.instance.PlaySE("MouseClick");

        yield return new WaitForSeconds(3.8f);
        playerStartMessage.SetActive(true);
        StartCoroutine(Typing(playerStartMessage.transform.GetChild(0).GetComponent<Text>(), "I want to enter this competition and win!", 0.1f));
        yield return SECOND_SIX;

        yield return SECOND_TWO;
        playerStartMessage.SetActive(false);

        // Fade in/out
        Loading();

        yield return new WaitForSeconds(4f);

        Scene01();
        startScene.SetActive(false);
    }

    // Scene1 : ������ �׸��� ����
    public void Scene01()
    {
        sceneIndex++;
        uiManager.option.SetActive(true);
        StartCoroutine(Scene01Coroutine());
    }

    IEnumerator Scene01Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);

        // BGM ����
        AudioManager.instance.PlayBGM("BackgroundSound");

        yield return SECOND_ONE;

        playerAnim.SetTrigger("OK");

        yield return SECOND_THREE;


        playerTextUI.SetActive(true);

        string text = "Let me think about Picture...";
        StartCoroutine(Typing(playerTextUI.transform.GetChild(0).GetComponent<Text>(), text, 0.1f));

        yield return SECOND_THREE;
        yield return SECOND_THREE;
        playerTextUI.SetActive(false);

        ThinkPicture(uiManager.GetQuestion(), "What should I draw?", uiManager.GetSelectPictureUI(), 0);
    }

    // Scene2 : � ���� �������� ��ĥ���� ����
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

        ThinkPicture(uiManager.GetQuestion(), "What about color combinations?", uiManager.GetSelectColorUI(), 1);
    }

    // Scene3 : ��ǰ Ÿ��Ʋ ����
    public void Scene03()
    {
        sceneIndex++;
        StartCoroutine(Scene03Coroutine());
    }

    IEnumerator Scene03Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_THREE = new WaitForSeconds(3f);


        yield return SECOND_ONE;

        playerAnim.SetTrigger("OK");

        yield return SECOND_THREE;

        playerTextUI.SetActive(true);

        string text = "I think it would be nice to decide on a title!";
        StartCoroutine(Typing(playerTextUI.transform.GetChild(0).GetComponent<Text>(), text, 0.1f));

        yield return SECOND_THREE;
        yield return SECOND_THREE;
        playerTextUI.SetActive(false);

        ThinkPicture(uiManager.GetQuestion(), "What should be the title?", uiManager.GetSelectTitleUI(), 2);
    }

    public void ThinkPicture(Text _textUI, string _text, GameObject _selectUI, int _option)
    {
        StartCoroutine(ThinkPictureCoroutine(_textUI, _text, _selectUI, _option));
    }

    IEnumerator ThinkPictureCoroutine(Text _textUI, string _text, GameObject _selectUI, int _option)
    {
        var SECOND_ONE = new WaitForSeconds(1f);

        if (_option == 1) uiManager.InitSelectTitleUI();
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
        if (_option == 1) uiManager.ResetColorUI();
    }

    // Ending1
    public void Ending_01()
    {
        uiManager.option.SetActive(false);
        StartCoroutine(Ending_01Coroutine());
    }

    IEnumerator Ending_01Coroutine()
    {
        var SECOND_ONE = new WaitForSeconds(1f);
        var SECOND_TWO = new WaitForSeconds(2f);
        var SECOND_THREE = new WaitForSeconds(3f);
        var SECOND_FIVE = new WaitForSeconds(5f);

        yield return SECOND_ONE;

        playerAnim.SetTrigger("OK");

        yield return SECOND_THREE;

        playerTextUI.SetActive(true);

        string text = "nice this is perfect!";
        for (int i = 0; i <= text.Length; i++)
        {
            playerTextUI.transform.GetChild(0).GetComponent<Text>().text = text.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return SECOND_THREE;
        playerTextUI.SetActive(false);

        yield return SECOND_ONE;

        // 18.8
        float counter = 0.0f;
        float duration = 8.0f;

        Vector3 startPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        Vector3 endPos = new Vector3(18.8f, mainCamera.transform.position.y, mainCamera.transform.position.z);

        float currVol = AudioManager.instance.audioSourceBgm.volume;
        float vol = 0.0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            // ī�޶� �̵�
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);

            // BGM �Ҹ� ���̱�
            vol = Mathf.Lerp(currVol, 0.0f, counter / duration);
            AudioManager.instance.SetAllBGMVolume(vol);

            yield return null;
        }

        yield return SECOND_ONE;
        // otherPlayer�� ī�޶� ���� �ִϸ��̼� ����
        otherPlayerAnim.SetTrigger("Done");
        yield return SECOND_FIVE;

        // CanvasTitle1 ����
        uiManager.SetCanvasTitlesActive(0, false);

        // Fade in/out
        Loading();


        yield return SECOND_THREE;

        // OtherPlayer Copy �ִϸ��̼�

        mainCamera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
        Ending01UI.SetActive(true);
        homePageUI[0].SetActive(true);

        // BGM Ʋ��
        AudioManager.instance.PlayBGM("DarkBGM");
        AudioManager.instance.audioSourceBgm.volume = currVol;

        yield return SECOND_THREE;

        // ���� ǳ�� ����
        yield return SECOND_ONE;
        uiManager.SetETCUI(true, false);
        yield return new WaitForSeconds(0.5f);
        uiManager.SetETCUI(true, true);
        yield return new WaitForSeconds(0.6f);
        otherPlayerThinkUI.SetActive(true);

        otherPlayerThinkUI.transform.GetChild(0).GetComponent<Image>().sprite = uiManager.GetCurSprite();
        otherPlayerThinkUI.transform.GetChild(0).GetComponent<Image>().material = uiManager.GetMaterial();
        otherPlayerThinkUI.transform.GetChild(0).GetComponent<Image>().material.SetColor("_PictureColor1", uiManager.GetMainColor());
        otherPlayerThinkUI.transform.GetChild(0).GetComponent<Image>().material.SetColor("_PictureColor2", uiManager.GetSubColor());

        yield return SECOND_THREE;

        yield return SECOND_THREE;

        otherPlayerThinkUI.SetActive(false);;
        uiManager.SetETCUI(false, false);

        // Ÿ����
        otherPlayerComputerText.SetActive(true);
        StartCoroutine(Typing(otherPlayerComputerText.GetComponent<Text>(), " The Art A.I.", 0.1f));

        yield return SECOND_THREE;

        // ���콺 ����Ʈ �̵�
        // �⺻ (-5.46, 0.41) -> (1.39, 0.41)
        startPos = new Vector3(mousePoint.transform.position.x, mousePoint.transform.position.y, mousePoint.transform.position.z);
        endPos = new Vector3(1.39f, mousePoint.transform.position.y, mousePoint.transform.position.z);
        counter = 0.0f;
        duration = 2.0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            mousePoint.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);
            yield return null;
        }

        // ���콺 Ŭ�� �̺�Ʈ
        AudioManager.instance.PlaySE("MouseClick");

        mousePoint.transform.localScale = new Vector3(0.09f, 0.09f, 1f);
        yield return new WaitForSeconds(0.5f);
        mousePoint.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        // Ȩ������ 2
        homePageUI[0].SetActive(false);
        otherPlayerComputerText.SetActive(false);
        homePageUI[1].SetActive(true);

        // Ȩ������ 3
        homePageUI[1].SetActive(false);
        otherPlayerComputerText.SetActive(true);
        homePageUI[2].SetActive(true);


        yield return SECOND_TWO;

        // ���콺 ����Ʈ �̵�
        // �⺻ (1.39, 0.41) -> (-4.13, -0.45)
        startPos = new Vector3(mousePoint.transform.position.x, mousePoint.transform.position.y, mousePoint.transform.position.z);
        endPos = new Vector3(-4.13f, -0.45f, mousePoint.transform.position.z);
        counter = 0.0f;
        duration = 2.0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            mousePoint.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);
            yield return null;
        }

        // ���콺 Ŭ�� �̺�Ʈ
        AudioManager.instance.PlaySE("MouseClick");

        mousePoint.transform.localScale = new Vector3(0.09f, 0.09f, 1f);
        yield return new WaitForSeconds(1f);
        mousePoint.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        // Ȩ������ 4
        homePageUI[2].SetActive(false);
        otherPlayerComputerText.SetActive(false);
        homePageUI[3].SetActive(true);


        yield return SECOND_TWO;

        // ���콺 ����Ʈ �̵�
        // �⺻ (-4.13, -0.45) -> (-5.45, 1.42)
        startPos = new Vector3(mousePoint.transform.position.x, mousePoint.transform.position.y, mousePoint.transform.position.z);
        endPos = new Vector3(-5.45f, 1.42f, mousePoint.transform.position.z);
        counter = 0.0f;
        duration = 2.0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            mousePoint.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);
            yield return null;
        }

        // ���콺 Ŭ�� �̺�Ʈ
        AudioManager.instance.PlaySE("MouseClick");

        mousePoint.transform.localScale = new Vector3(0.09f, 0.09f, 1f);
        yield return new WaitForSeconds(1f);
        mousePoint.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        // Picture Upload
        uploadPicture.SetActive(true);
        SetCanvas(uploadPicture.GetComponent<SpriteRenderer>());

        yield return SECOND_THREE;

        // Create ������ OtherPlayer Anim �ߵ�

        // ���콺 ����Ʈ �̵�
        // �⺻ (-5.45, 1.42) -> (-0.08, 1.42)
        startPos = new Vector3(mousePoint.transform.position.x, mousePoint.transform.position.y, mousePoint.transform.position.z);
        endPos = new Vector3(-0.08f, 1.42f, mousePoint.transform.position.z);
        counter = 0.0f;
        duration = 2.0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            mousePoint.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);
            yield return null;
        }

        // ���콺 Ŭ�� �̺�Ʈ
        AudioManager.instance.PlaySE("MouseClick");

        mousePoint.transform.localScale = new Vector3(0.09f, 0.09f, 1f);
        yield return new WaitForSeconds(1f);
        mousePoint.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        line.SetActive(true);

        yield return SECOND_TWO;

        otherPlayerEndingAnim.SetTrigger("Laugh");

        yield return SECOND_FIVE;

        // Ending_02 ������ �̵�

        // Fade in/out
        Loading();


        yield return SECOND_THREE;

        Ending01UI.SetActive(false);

        mainCamera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
        mainCamera.orthographicSize = 1.32f;
        yield return SECOND_ONE;
        Ending_02();
    }

    // Ending2
    public void Ending_02()
    {
        Ending02UI.SetActive(true);

        // Canvas Set
        SetCanvas(Ending02UI.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>());   // otherPlayer Canvas
        SetCanvas(Ending02UI.transform.GetChild(10).transform.GetChild(0).GetComponent<SpriteRenderer>());  // player Canvas

        // TitleSet
        uiManager.SetCanvasTitlesActive(1, true);
        SetTitle(uiManager.GetCanvasTitles(1), true);

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
        float duration = 12.0f;


        Vector3 startPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        Vector3 endPos = new Vector3(0f,0f, mainCamera.transform.position.z);

        bool isFirst = true;
        while (counter < duration)
        {
            counter += Time.deltaTime;

            if(counter > 3.0f && isFirst)
            {
                // �ڼ� �Ҹ�
                AudioManager.instance.PlaySE("ApplauseSoundFx");
                isFirst = false;
            }

            // ī�޶� �� �� / �̵�
            mainCamera.orthographicSize = Mathf.Lerp(start, end, counter / duration);
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);

            // canvasTitle ������ ����
            float scale = Mathf.Lerp(1.0f, 0.5f, counter / duration);
            uiManager.GetCanvasTitles(1).transform.localScale = new Vector3(scale, scale, 1);

            yield return null;
        }


        yield return SECOND_FIVE;

        // Player ȭ�� ���߱�
        // Camera  -24.8 -5.5 -10
        //

        counter = 0.0f;
        duration = 7.0f;
        start = 12.0f; end = 5.0f;

        startPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        endPos = new Vector3(-24.8f, -5.5f, mainCamera.transform.position.z);

        playerLastMessage.SetActive(true);
        StartCoroutine(Typing(playerLastMessage.GetComponent<Text>(), "Why is she over there . . . ?", 0.15f));


        Ending02FadeUI.SetActive(true);
        while (counter < duration)
        {
            counter += Time.deltaTime;

            // canvasTitle ������ ����
            float scale = Mathf.Lerp(0.5f, 1f, counter / duration);
            uiManager.GetCanvasTitles(1).transform.localScale = new Vector3(scale, scale, 1);

            // ī�޶� �� �� / �̵�
            mainCamera.orthographicSize = Mathf.Lerp(start, end, counter / duration);
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);

            // ���̵� ��
            Color tmp = Ending02FadeUI.GetComponent<SpriteRenderer>().color;
            tmp.a = Mathf.Lerp(0, 1, counter / duration);
            Ending02FadeUI.GetComponent<SpriteRenderer>().color = tmp;

            yield return null;
        }

        yield return SECOND_THREE;

        uiManager.SetCanvasTitlesActive(1, false);

        // �� �Ҹ� ����Ʈ
        AudioManager.instance.PlaySE("Boom Sound Effect_1");
        playerObjects[0].SetActive(false);
        playerObjects[1].SetActive(false);
        playerLastMessage.SetActive(false);

        yield return SECOND_FIVE;

        playerLastMessage.GetComponent<Text>().text = "";
        mainCamera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
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


        // �ϼ��� �׸� �ҷ�����

        // Picture Drawing
        StartCoroutine(FadePictureCourtine(Ending03UI[2].GetComponent<SpriteRenderer>(), 0, 1, 5f));

        yield return SECOND_FIVE;

        CanvasGroup cg = Ending03TextUI[2].GetComponent<CanvasGroup>();
        StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 2f));

        yield return SECOND_THREE;

        cg = Ending03TextUI[3].GetComponent<CanvasGroup>();
        StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 2f));

        yield return SECOND_THREE;

        string message = "Whose work is it ?";
        for (int i = 0; i <= message.Length; i++)
        {
            Ending03TextUI[4].GetComponent<Text>().text = message.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return SECOND_FIVE;

        // BGM ����
        AudioManager.instance.StopBGM();

        // ���� ȭ������ ���ư���
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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

    // �ڸ� Ÿ���� ����Ʈ
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

    public void SetTitle(GameObject _titleUI, bool _option)
    {
       
        if(_option)
        {
            string title = uiManager.GetTitle();


            // otherCanvas�� Title�� ��� �ڿ� 2���� ��ġ�ؼ� ����
            // ��, �ٲٷ��� �ϴ� ���ڰ� �޶����

            // Apple : 5
            // 0 : A 
            // 1 : p
            // 2 : p
            // 3 : l
            // 4 : e

            // ���ڰ� �ѹ��ڷ� ���ϵǾ� �ִ��� üũ
            bool isSame = false;

            char ch = title[1];
            for(int i=2; i < title.Length; i++)
            {
                if (!ch.Equals(title[i]))
                {
                    isSame = false;
                }
            }

            if(isSame)
            {
                // ���ڰ� �ѹ��ڷ� ���ϵǾ� �ִٸ� �������� ���� �߰��Ͽ� ������
                title = title + "a";
            }
            else
            {
                int index1 = Random.Range(1, title.Length - 1);
                int index2 = Random.Range(1, title.Length - 1);

                while (index2 == index1 || title[index1].Equals(title[index2]))
                {
                    index2 = Random.Range(1, title.Length - 1);
                }

                // ��ġ
                StringBuilder sb = new StringBuilder(title);
                char tmp = sb[index1];
                sb[index1] = title[index2];
                sb[index2] = tmp;

                title = sb.ToString();
            }


            _titleUI.GetComponent<Text>().text = "Title: <" + title + ">";
        }
        else
        {
            _titleUI.GetComponent<Text>().text = "Title: <" + uiManager.GetTitle() + ">";
        }
    }

    public int GetCurSceneIndex() { return sceneIndex; }
}
