using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Pitcure[] pictures;

    // 단계
    // 1) 밑그림
    // 2) 색칠
    private int stage;

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
    [SerializeField]
    private GameObject checkCanvasUI;

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

    [SerializeField]
    private Animator playerAnim;

    private void Start()
    {
        stage = 0;
        question.text = "";

        curPicture = null;
        selectColorUI.SetActive(false);
        InitSelectPictureUI();
        checkCanvasUI.SetActive(false);

        etcs[0].SetActive(false);
        etcs[1].SetActive(false);
        drawCanvas_Default.GetComponent<SpriteRenderer>().sprite = null;
        drawCanvas_Color.GetComponent<SpriteRenderer>().sprite = null;

        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sceneManager.ThinkPicture(question, "What's Draw?", selectPictureUI);
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

        checkCanvasUI.SetActive(true);

        //sceneManager.ThinkPicture(question, "What's Color?", selectColorUI);
    }

    public void SetSelectPictureUI(bool _active)
    {
        selectPictureUI.SetActive(_active);
    }

    public void SelectColors(Image _colors)
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
            drawCanvas_Color.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor1", Color.green);
            drawCanvas_Color.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor2", _colors.color);

            checkCanvasUI.SetActive(true);
        }
    }

    public void SetSelectColorUI(bool _active)
    {
        selectColorUI.SetActive(_active);
    }

    public void SetETCUI(bool _active1, bool _active2)
    {
        etcs[0].SetActive(_active1);
        etcs[1].SetActive(_active2);
    }

    public void ReDraw()
    {
        checkCanvasUI.SetActive(false);
        if(stage == 0) sceneManager.ThinkPicture(question, "What's Draw?", selectPictureUI);
        else if (stage == 1) sceneManager.ThinkPicture(question, "What's Color?", selectColorUI);
    }


    public void Done()
    {
        checkCanvasUI.SetActive(false);
        if (stage == 0)
        {
            sceneManager.ThinkPicture(question, "What's Color?", selectColorUI);
            stage++;
        }
        else if (stage == 1)
        {
            // SceneManager에서 엔딩 보기
            playerAnim.SetTrigger("Done");
        }
    }
}
