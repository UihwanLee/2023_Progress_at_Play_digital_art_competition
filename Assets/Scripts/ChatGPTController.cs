using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ChatGPTWrapper;

/*
    ChatGPT ��Ʈ�ѷ� ��ũ��Ʈ
*/

public class ChatGPTController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject responseUI;

    // ChatGPT �̵� ����
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

    // ChatGPT �亯
    [SerializeField]
    private GameObject responseMessage;

    [SerializeField]
    private GameObject chatGPTConversation;

    private bool isResponsing;

    // Start is called before the first frame update
    void Start()
    {
        isMoveable = true;

        // ���� �ʱ�ȭ
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

    // ChatGPT ������Ʈ ������ Ȯ��
    private void CheckMovement()
    {
        if(isMoveable)
        {
            // Player�� ���󰡴� �Լ�
            targetPosX = this.player.transform.position.x + -1.0f;
            targetPosY = this.player.transform.position.y + 1.0f;
            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(transform.position.y, targetPosY, smootingY);
            transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);

            // ChatGPT ��ǳ�� ������ Ȯ��
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

    // ChatGPT���� ���� �޼����� �˻��ϰ� ������
    public void CheckSendMessage(GameObject sendMessage)
    {
        string message = sendMessage.GetComponent<TextMeshProUGUI>().text;

        // �޼����� ����ް� �ִ� ���̶�� ���
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

    // ChatGPT�� �亯�� �޾� �ؽ�Ʈ�� ǥ���ϱ�
    public void setText(string _message)
    {
        if (responseUI.activeSelf)
        {
            waitForASecond.SetActive(false);
            responseMessage.SetActive(true);

            // ���� �޼��� ũ�⿡ ���� ���� ������
            // (���û���) ��ǳ��ũ�⵵ �ٲ��ֱ�
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
            
            // Overflow�� ���� ��ǳ���� ���� �Ѵ�.
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
        // ����üũ
        //ChatGPTWrapper.ChatGPTConversation
    }
}
