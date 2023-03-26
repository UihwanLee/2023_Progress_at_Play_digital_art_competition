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

    // 캔버스에 표시할 Sprite
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

    // 밑그림 캔버스 선택 시
    public void SelectPicture(GameObject _pictureImage)
    {
        // 그림 그리는 애니메이션 작동 후 그림 그리기
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
