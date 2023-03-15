using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChatGPTWrapper;

public class Send : MonoBehaviour
{
    public ChatGPTConversation chatGPT;
    // Start is called before the first frame update
    void Start()
    {
        chatGPT = GetComponent<ChatGPTConversation>();
    }

    public void sendMessage(TextMeshProUGUI _message)
    {
        //chatGPT.SendToChatGPT(_message);
    }
}
