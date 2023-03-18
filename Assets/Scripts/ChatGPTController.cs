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

    // SendMessageObject
    [SerializeField]
    private GameObject sendMessageInputField;

    // WaitForASecond
    [SerializeField]
    private GameObject waitForASecond;

    // ChatGPT 답변
    [SerializeField]
    private GameObject responseMessage;
    private int maxMessageSize;

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

        // 말풍선 메세지 최대 크기
        // 한국어 기준으로 최대 사이즈를 정할 수 있도록 한다.
        maxMessageSize = 104;

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

        //sendMessage.GetComponent<TextMeshProUGUI>().text = "";
        //Debug.Log(sendMessage.GetComponent<TextMeshProUGUI>().text);
    }

    public void CheckSendMessageGUI(TextMeshProUGUI sendMessage)
    {
        string message = sendMessage.text;

        // 메세지를 답장받고 있는 중이라면 대기
        if (isResponsing == false)
        {
            if (message.Length >= 2)
            {
                isResponsing = true;
                chatGPTConversation.GetComponent<ChatGPTWrapper.ChatGPTConversation>().SendToChatGPT(sendMessage);
                sendMessage.text = "";
            }
        }
    }

    // ChatGPT의 답변을 받아 텍스트에 표시하기
    public void setText(string _message)
    {
        responseUI.SetActive(true);
        if (responseUI.activeSelf)
        {
            waitForASecond.SetActive(false);
            responseMessage.SetActive(true);

            // 받은 메세지 크기에 따라 문단 나누고
            // (선택사항) 말풍선크기도 바꿔주기
            StartCoroutine(TypingCoroutine(_message));
            //responseMessage.text = _message;
        }
    }

    IEnumerator TypingCoroutine(string _text)
    {
        responseMessage.GetComponent<TextMeshProUGUI>().text = "";

        // Text가 길때 말풍선 갱신 처리
        // 글자 수 별로 Text를 나누어 해당 수만큼 갱신하며 처리
        int arraySize = (_text.Length % maxMessageSize == 0) ? _text.Length / maxMessageSize : (_text.Length/maxMessageSize) + 1; 
        string[] messageArray = new string[arraySize];

        for (int i=0; i< arraySize; ++i)
        {
            // 0 103 207
            int startIndex = (i==0) ? 0 : (maxMessageSize * i) - 1;
            int endIndex = (i + 1 != arraySize) ? maxMessageSize : _text.Length - (maxMessageSize * i);

            messageArray[i] = _text.Substring(startIndex, endIndex);
        }

        foreach(string message in messageArray)
        {
            responseMessage.GetComponent<TextMeshProUGUI>().text = "";
            for (int j = 0; j <= message.Length; ++j)
            {
                responseMessage.GetComponent<TextMeshProUGUI>().text = message.Substring(0, j);

                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.8f);
        }

        yield return new WaitForSeconds(2.0f);
        responseMessage.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.0f);
        responseMessage.SetActive(false);
        responseUI.SetActive(false);

        isResponsing = false;
    }

    // 현재 타이핑하고 있는지 판단하는 함수
    public bool IsTyping()
    {
        if (sendMessageInputField.GetComponent<TMP_InputField>().isFocused) return true;
        else return false;
    }

    public void OnCreate()
    {
        // 공백체크
        //ChatGPTWrapper.ChatGPTConversation
    }
}
