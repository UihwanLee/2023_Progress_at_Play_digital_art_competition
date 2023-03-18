using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ������ ��� �ؽ�Ʈ�� �����ϴ� ��ũ��Ʈ

public class MessageManager : MonoBehaviour
{
    private string LANGUAGE;

    [SerializeField]
    private Message[] STAGE1_MESSAGES;

    // Scripts
    [SerializeField]
    private ChatGPTController chatGPTController;

    // Start is called before the first frame update
    void Start()
    {
        LANGUAGE = "_KOR";
    }

   public void SendMessage()
    {
        TextMeshProUGUI textMeshProGUI = new TextMeshProUGUI();
        textMeshProGUI.text = STAGE1_MESSAGES[0].GetMessage();
        chatGPTController.CheckSendMessageGUI(textMeshProGUI);
    }
}
