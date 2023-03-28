using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextStuff : MonoBehaviour
{
    TextMeshProUGUI sendMessage;
    private int resource;
    // Start is called before the first frame update
    void Start()
    {
        sendMessage = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    // ChatGPT ´äº¯ 
    public void setText(string _message)
    {
        sendMessage.text = _message;
    }

    public void resetInfieldText()
    {

    }
}
