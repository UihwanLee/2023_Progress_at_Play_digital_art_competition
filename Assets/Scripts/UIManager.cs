using System.Collections;
using System.Collections.Generic;
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

    // ĵ������ ǥ���� Sprite
    [SerializeField]
    private GameObject drawCanvas;

    [SerializeField]
    private Material M_picture;

    private Pitcure curPicture;

    private void Start()
    {
        question.text = "What's Draw?";

        curPicture = null;
        selectColorUI.SetActive(false);
        //selectPictureUI.SetActive(false);
        //selectCanvasUI.SetActive(false);
        drawCanvas.GetComponent<SpriteRenderer>().sprite = null;
    }

    // �ر׸� ĵ���� ���� ��
    public void SelectPicture(GameObject _pictureImage)
    {
        // �׸� �׸��� �ִϸ��̼� �۵� �� �׸� �׸���
        curPicture = _pictureImage.GetComponent<SelectPicture>().GetCurPicture();
        drawCanvas.GetComponent<SpriteRenderer>().sprite = _pictureImage.transform.GetChild(0).GetComponent<Image>().sprite;

        question.text = "What's Color";
        selectPictureUI.SetActive(false);
        selectCanvasUI.SetActive(false);

        StartCoroutine(ShowSelectPictureUI());
    }

    public void SetSelectCanvasUI(bool _active)
    {
        selectCanvasUI.SetActive(_active);
    }

    IEnumerator ShowSelectPictureUI()
    {
        yield return new WaitForSeconds(5f);


        selectCanvasUI.SetActive(true);
        selectColorUI.SetActive(true);
    }

    public void SelectColors(Image _colors)
    {
        selectColorUI.SetActive(false);
        selectCanvasUI.SetActive(false);

        if(curPicture != null)
        {
            drawCanvas.GetComponent<SpriteRenderer>().sprite = curPicture.GetPictureColor();
            drawCanvas.GetComponent<SpriteRenderer>().material = M_picture;
            drawCanvas.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor1", Color.green);
            drawCanvas.GetComponent<SpriteRenderer>().material.SetColor("_PictureColor2", _colors.color);
        }
    }
}
