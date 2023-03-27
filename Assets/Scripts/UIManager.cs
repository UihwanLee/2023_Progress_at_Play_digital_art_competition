using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Pitcure[] pictures;

    // �ܰ�
    // 1) �ر׸�
    // 2) ��ĥ
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
    // ĵ������ ǥ���� Sprite
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

    // �ر׸� ĵ���� ���� ��
    public void SelectPicture(GameObject _pictureImage)
    {
        // �׸� �׸��� �ִϸ��̼� �۵� �� �׸� �׸���
        curPicture = _pictureImage.GetComponent<SelectPicture>().GetCurPicture();
        drawCanvas_Default.GetComponent<SpriteRenderer>().sprite = _pictureImage.transform.GetChild(0).GetComponent<Image>().sprite;
        animatorDraw.SetTrigger("Draw_Default");

        question.text = "";
        InitSelectPictureUI();

        // �� �׸� �� �����ֱ�
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
            // SceneManager���� ���� ����
            playerAnim.SetTrigger("Done");
        }
    }
}
