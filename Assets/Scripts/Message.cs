using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Message", menuName = "New Message/message")]
public class Message : ScriptableObject
{
    [SerializeField]
    private string messageTitle;
    [SerializeField]
    private string message_KOR;
    [SerializeField]
    private string message_ENG;

    public enum MessageType
    {
        UI,
        Player,
        ChatGPT,
        ETC
    }

    public string GetMessage() 
    {
        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (uiManager.GetCurLanguage() == "_ENG") return message_ENG;
        else
        {
            return message_KOR;
        }
    }
}
