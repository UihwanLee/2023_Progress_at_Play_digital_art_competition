using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ChatGPTWrapper;

/*
    ChatGPT 컨트롤러 스크립트
*/

public class ChatGPTController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject responseUI;

    // ChatGPT 이동 변수
    [SerializeField]
    private float speed = 5.0f;
    private float smootingX = 0.01f;
    private float smootingY = 0.03f;
    private float targetPosX;
    private float targetPosY;

    private bool isMoveable;

    // WaitForASecond
    [SerializeField]
    private GameObject waitForASecond;

    // ChatGPT 답변
    [SerializeField]
    private GameObject responseMessage;

    [SerializeField]
    private GameObject chatGPTConversation;

    private bool isResponsing;

    // Start is called before the first frame update
    void Start()
    {
        isMoveable = true;

        // 변수 초기화
        targetPosX = 0.0f;
        targetPosY = 0.0f;

        isResponsing = false;

        waitForASecond.SetActive(false);
        responseMessage.SetActive(false);
        responseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    // ChatGPT 오브젝트 움직임 확인
    private void CheckMovement()
    {
        if(isMoveable)
        {
            // Player를 따라가는 함수
            targetPosX = this.player.transform.position.x + -1.0f;
            targetPosY = this.player.transform.position.y + 1.0f;
            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(transform.position.y, targetPosY, smootingY);
            transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);

            // ChatGPT 말풍선 움직임 확인
            if (responseUI.activeSelf)
            { 
                targetPosX = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0)).x;
                targetPosY = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2.4f, 0)).y;
                smoothPosX = Mathf.Lerp(responseUI.transform.position.x, targetPosX, smootingX);
                smoothPosY = Mathf.Lerp(responseUI.transform.position.y, targetPosY, smootingY);
                responseUI.transform.position = new Vector3(smoothPosX, smoothPosY, 0.0f);

                //responseUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
            }
        }
    }

    // ChatGPT에게 보낼 메세지를 검사하고 보내기
    public void CheckSendMessage(GameObject sendMessage)
    {
        string message = sendMessage.GetComponent<TextMeshProUGUI>().text;

        // 메세지를 답장받고 있는 중이라면 대기
        if (isResponsing == false)
        {
            if (message.Length >= 2)
            {
                isResponsing = true;

                responseUI.SetActive(true);
                waitForASecond.SetActive(true);
                chatGPTConversation.GetComponent<ChatGPTWrapper.ChatGPTConversation>().SendToChatGPT(sendMessage.GetComponent<TextMeshProUGUI>());
                sendMessage.GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        sendMessage.GetComponent<TextMeshProUGUI>().text = "";
        //Debug.Log(sendMessage.GetComponent<TextMeshProUGUI>().text);
    }

    // ChatGPT의 답변을 받아 텍스트에 표시하기
    public void setText(string _message)
    {
        if (responseUI.activeSelf)
        {
            waitForASecond.SetActive(false);
            responseMessage.SetActive(true);

            // 받은 메세지 크기에 따라 문단 나누고
            // (선택사항) 말풍선크기도 바꿔주기
            StartCoroutine(_typing(_message));
            //responseMessage.text = _message;
        }
    }

    IEnumerator _typing(string _text)
    {
        responseMessage.GetComponent<TextMeshProUGUI>().text = "";
        float textHeight;
        float rectHeight = responseMessage.GetComponent<RectTransform>().rect.height;
        int startIndex = 1;
        float currentPageHeight = 0.0f;
        for (int i=0; i<= _text.Length; ++i)
        {
            responseMessage.GetComponent<TextMeshProUGUI>().text = _text.Substring(startIndex-1, i);
            
            // Overflow시 다음 말풍선에 적게 한다.
            textHeight = responseMessage.GetComponent<TextMeshProUGUI>().preferredHeight;
            if (textHeight > rectHeight + currentPageHeight)
            {
                currentPageHeight += textHeight;
                responseMessage.GetComponent<TextMeshProUGUI>().text = "";
                startIndex = i;
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2.0f);
        responseMessage.GetComponent<TextMeshProUGUI>().text = "";
        responseMessage.SetActive(false);
        responseUI.SetActive(false);

        isResponsing = false;
    }

    public void ActiveMessage()
    {
        responseUI.SetActive(true);
    }

    public void OnCreate()
    {
        // 공백체크
        //ChatGPTWrapper.ChatGPTConversation
    }
}
