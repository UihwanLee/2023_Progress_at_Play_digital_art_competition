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

    [SerializeField]
    private Animator animatorDraw;

    // 캔버스에 표시할 Sprite
    [SerializeField]
    private GameObject drawCanvas_Default;
    [SerializeField]
    private GameObject drawCanvas_Color;

    [SerializeField]
    private Material M_picture;

    private Pitcure curPicture;

    private SceneManager sceneManager;

    private void Start()
    {
        question.text = "";

        curPicture = null;
        selectColorUI.SetActive(false);
        InitSelectPictureUI();
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
            drawCanvas_Color.GetComponent<SpriteRenderer>().sprite = curPicture.GetPictureColor();
            drawCanvas_Color.GetComponent<SpriteRenderer>().material = M_picture;
            drawCanvas_Color.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor1", mainColor);
            drawCanvas_Color.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor2", subColor);

            StartCoroutine(ShowCheckCanvasUI());
        }
    }

    // 그림 그리고 난 후
    public void ReDraw()
    {
        checkCanvasUI.SetActive(false);
        if (sceneManager.GetCurSceneIndex() == 1)
        {
            sceneManager.ThinkPicture(question, "What's Draw?", selectPictureUI, false);
        }
        else if (sceneManager.GetCurSceneIndex() == 2)
        {
            sceneManager.ThinkPicture(question, "What's Color?", selectColorUI, true);
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
            // SceneManager에서 엔딩 보기
            sceneManager.Ending_01();
            //playerAnim.SetTrigger("Done");
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
}
