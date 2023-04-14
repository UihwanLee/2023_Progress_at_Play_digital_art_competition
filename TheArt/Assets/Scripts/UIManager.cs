using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Pitcure[] pictures;

    // Select Canvas UI
    [SerializeField]
    private Text question;

    [SerializeField]
    private GameObject selectCanvasUI;
    [SerializeField]
    private GameObject selectPictureUI;
    [SerializeField]
    private GameObject selectColorUI;
    [SerializeField]
    private GameObject selectTitleUI;
    [SerializeField]
    private GameObject[] etcs;

    // Check Canvas
    [Header("CheckUI")]
    [SerializeField]
    private GameObject checkCanvasUI;

    // Color Canvas
    [Header("ColorUI")]   
    [SerializeField]
    private GameObject chooseColorUI;
    [SerializeField]
    private GameObject[] palletColors;
    [SerializeField]
    private GameObject[] curColorsUI;
    private bool isMainColor;
    private Color32 mainColor;
    private Color32 subColor;

    private Sprite curSprite;

    // Title Canvas
    [Header("TitleUI")]
    [SerializeField]
    private Text title;
    [SerializeField]
    private GameObject inputTitle;
    [SerializeField]
    private Text warningText;
    [SerializeField]
    private Animator inputFieldAnimator;

    private string curTitle;

    [SerializeField]
    private GameObject[] canvaseTitles;

    [SerializeField]
    private Animator animatorDraw;

    // 캔버스에 표시할 Sprite
    [SerializeField]
    private GameObject drawCanvas_Default;
    [SerializeField]
    private GameObject drawCanvas_Color;

    [SerializeField]
    private Material M_picture;

    public GameObject option;

    private Pitcure curPicture;

    private SceneManager sceneManager;

    private void Start()
    {
        option.SetActive(false);

        question.text = "";

        curPicture = null;
        selectColorUI.SetActive(false);
        InitSelectPictureUI();
        InitSelectTitleUI();
        checkCanvasUI.SetActive(false);
        chooseColorUI.SetActive(false);

        etcs[0].SetActive(false);
        etcs[1].SetActive(false);
        drawCanvas_Default.GetComponent<SpriteRenderer>().sprite = null;
        drawCanvas_Color.GetComponent<SpriteRenderer>().sprite = null;

        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        mainColor = PictureColor.GetColor(EPictureColor.White);
        subColor = PictureColor.GetColor(EPictureColor.White);
        isMainColor = true;

        for (int i=0; i<canvaseTitles.Length; i++)
        {
            canvaseTitles[i].SetActive(true);
            canvaseTitles[i].GetComponent<Text>().text = "";
            canvaseTitles[i].SetActive(false);
        }

        curTitle = null;
        curSprite = null;
    }

    private void InitSelectPictureUI()
    {
        selectCanvasUI.SetActive(true);
        selectPictureUI.SetActive(true);
        selectPictureUI.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "";
        selectPictureUI.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = "";
        selectPictureUI.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "";
        selectPictureUI.SetActive(false);
        selectCanvasUI.SetActive(false);
    }

    public void SetSelectCanvasUI(bool _active)
    {
        selectCanvasUI.SetActive(_active);
    }

    // 밑그림 캔버스 선택 시
    public void SelectPicture(GameObject _pictureImage)
    {
        // 그림 그리는 애니메이션 작동 후 그림 그리기
        curPicture = _pictureImage.GetComponent<SelectPicture>().GetCurPicture();
        drawCanvas_Default.GetComponent<SpriteRenderer>().sprite = _pictureImage.transform.GetChild(0).GetComponent<Image>().sprite;
        animatorDraw.SetTrigger("Draw_Default");

        // 마우스 클릭 이벤트
        AudioManager.instance.PlaySE("Brushing");

        question.text = "";
        InitSelectPictureUI();

        // 다 그린 후 보여주기
        etcs[0].SetActive(false);
        etcs[1].SetActive(false);

        StartCoroutine(ShowCheckCanvasUI());
    }

    public void SetSelectPictureUI(bool _active)
    {
        selectPictureUI.SetActive(_active);
    }

    public void SetETCUI(bool _active1, bool _active2)
    {
        etcs[0].SetActive(_active1);
        etcs[1].SetActive(_active2);
    }

    // 색깔 정하기
    public void SetSelectColorUI(bool _active)
    {
        selectColorUI.SetActive(_active);
    }

    public void SetCurColor(bool _main)
    {
        isMainColor = _main;
    }

    public void SetColor(Image _img)
    {
        if(isMainColor) this.mainColor = _img.color;
        else this.subColor = _img.color;   

        ResetColorUI(); 
    }

    public void ResetColorUI()
    {
        palletColors[0].GetComponent<Image>().color = this.mainColor;
        palletColors[1].GetComponent<Image>().color = this.subColor;

        curColorsUI[0].GetComponent<Image>().color = this.mainColor;
        curColorsUI[0].transform.GetChild(0).GetComponent<Text>().text = PictureColor.GetColorName(curColorsUI[0].GetComponent<Image>().color);
        curColorsUI[0].transform.GetChild(0).GetComponent<Text>().color = this.mainColor;

        curColorsUI[1].GetComponent <Image>().color = this.subColor;
        curColorsUI[1].transform.GetChild(0).GetComponent<Text>().text = PictureColor.GetColorName(curColorsUI[1].GetComponent<Image>().color);
        curColorsUI[1].transform.GetChild(0).GetComponent<Text>().color = this.subColor;
    }

    public void SelectColors()
    {
        question.text = "";
        selectColorUI.SetActive(false);
        selectCanvasUI.SetActive(false);
        etcs[0].SetActive(false);
        etcs[1].SetActive(false);

        if (curPicture != null)
        {
            animatorDraw.SetTrigger("Draw_Color");

            // 마우스 클릭 이벤트
            AudioManager.instance.PlaySE("Brushing");

            curSprite = curPicture.GetPictureColor();
            drawCanvas_Color.GetComponent<SpriteRenderer>().sprite = curPicture.GetPictureColor();
            drawCanvas_Color.GetComponent<SpriteRenderer>().material = M_picture;
            drawCanvas_Color.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor1", mainColor);
            drawCanvas_Color.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor2", subColor);

            StartCoroutine(ShowCheckCanvasUI());
        }
    }

    public void InitSelectTitleUI()
    {
        selectCanvasUI.SetActive(true);
        selectTitleUI.SetActive(true);
        SetSelectTitleUI();
        inputTitle.GetComponent<Text>().text = "";
        warningText.text = "";
        selectTitleUI.SetActive(false);
        selectCanvasUI.SetActive(false);
    }

    public void SetSelectTitleUI()
    {
        if(curPicture)
        {
            if (curTitle == null)
            {
                title.text = "<" + curPicture.GetPictureTitle() + ">";
                curTitle = curPicture.GetPictureTitle();
            }
            else title.text = "<" + curTitle + ">";
        }
        else
        {
            title.text = "";
        }
    }

    // 타이틀 입력 후 타이틀 검사
    public void CheckTitle()
    {
        if (inputTitle.GetComponent<Text>().text != "" && inputTitle.GetComponent<Text>().text.Length > 2 && inputTitle.GetComponent<Text>().text.Length <= 10)
        {
            warningText.text = "";
            curTitle = inputTitle.GetComponent<Text>().text;
            inputTitle.GetComponent<Text>().text = "";
            SetSelectTitleUI();
        }
        else
        {
            // 애니메이션 재생
            inputFieldAnimator.SetTrigger("warning");
            if(inputTitle.GetComponent<Text>().text == "")
            {
                warningText.text = "I should think about Title!";
            }
            else if(inputTitle.GetComponent<Text>().text.Length <= 2)
            {
                warningText.text = "I think this is too short!";
            }
            else if(inputTitle.GetComponent<Text>().text.Length > 10)
            {
                warningText.text = "I think this is too long!";
            }
        }
    }

    // Ok 버튼 누른 후
    public void SelectTitle()
    {
        etcs[0].SetActive(false);
        etcs[1].SetActive(false);

        if (title.text != "")
        {
            InitSelectTitleUI();

            animatorDraw.SetTrigger("Draw_Title");
            // 마우스 클릭 이벤트
            AudioManager.instance.PlaySE("Brushing");

            canvaseTitles[0].SetActive(true);
            canvaseTitles[0].GetComponent<Text>().text = "Title: <" + curTitle + ">";



            selectTitleUI.SetActive(false);

            StartCoroutine(ShowCheckCanvasUI());
        }
    }

    // 그림 그리고 난 후
    public void ReDraw()
    {
        checkCanvasUI.SetActive(false);
        if (sceneManager.GetCurSceneIndex() == 1)
        {
            sceneManager.ThinkPicture(question, "What should I draw?", selectPictureUI, 0);
        }
        else if (sceneManager.GetCurSceneIndex() == 2)
        {
            sceneManager.ThinkPicture(question, "What about color combinations?", selectColorUI, 1);
        }
        else if (sceneManager.GetCurSceneIndex() == 3)
        {
            sceneManager.ThinkPicture(question, "What should be the title?", selectTitleUI, 1);
        }
    }


    public void Done()
    {
        checkCanvasUI.SetActive(false);
        if (sceneManager.GetCurSceneIndex() == 1)
        {
            sceneManager.Scene02();
        }
        else if (sceneManager.GetCurSceneIndex() == 2)
        {
            sceneManager.Scene03();
        }
        else if(sceneManager.GetCurSceneIndex() == 3)
        {
            // SceneManager에서 엔딩 보기
            sceneManager.Ending_01();
        }
    }

    IEnumerator ShowCheckCanvasUI()
    {
        yield return new WaitForSeconds(4f);
        checkCanvasUI.SetActive(true);
    }

    public Text GetQuestion() { return question; }
    public GameObject GetSelectPictureUI() { return selectPictureUI; }
    public GameObject GetSelectColorUI() { return selectColorUI; }
    public GameObject GetSelectTitleUI() { return selectTitleUI; }

    public Color32 GetMainColor() { return mainColor; }
    public Color32 GetSubColor() { return subColor; }  

    public Sprite GetCurSprite() { return curSprite; }  
    public Material GetMaterial() { return M_picture; }
    public string GetTitle() { return curTitle; }

    public GameObject GetCanvasTitles(int _index)
    {
        if (_index < canvaseTitles.Length) return canvaseTitles[_index];
        else return null;
    }

    public void SetCanvasTitlesActive(int _index, bool _active)
    {
        if (_index < canvaseTitles.Length)
        {
            canvaseTitles[_index].SetActive(_active);
        }
    }
}
