using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ������ ��� �ؽ�Ʈ�� �����ϴ� ��ũ��Ʈ

public class UIManager : MonoBehaviour
{
    private string LANGUAGE;

    // UI
    [Header("UI")]
    [SerializeField]
    private GameObject purposeUI;
    [SerializeField]
    private GameObject sendUI;
    [SerializeField]
    private GameObject interectUI;
    [SerializeField]
    private GameObject playerMessageUI;

    // Money System UI
    [SerializeField]
    private GameObject moneySystemUI;
    [SerializeField]
    private GameObject busCost;
    [SerializeField]
    private GameObject myMoney;
    [SerializeField]
    private GameObject limitTime;

    [SerializeField]
    private GameObject skipUI;
    
    // Purpose Text ����
    private int curPurposIndex;
    [SerializeField]
    private TextMeshProUGUI purposeText;
    [SerializeField]
    private Message[] purposeMessageArr;

    // ChatGPT Text ����
    private int curChatAIIndex;
    [SerializeField]
    private Message[] chatAIMessageArr;

    // Player Text ����
    private int curPlayerIndex;
    [SerializeField]
    private TextMeshProUGUI playerText;
    [SerializeField]
    private Message[] playerMessageArr;

    // Object Text ���� : ������Ʈ�� Interect ��츸 ���ϰ� �ൿ
    private int curObjIndex; // ���� ������Ʈ �ε���
    private int curObjMessageIndex; // ���� ������Ʈ�� ���� �޼��� �ε���
    [SerializeField]
    private GameObject[] objText;
    [SerializeField]
    private Message[] objMessageArr;

    // Scripts
    [SerializeField]
    private ChatGPTController chatGPTController;

    // Start is called before the first frame update
    void Start()
    {
        // ��ǥ ����
        curPurposIndex = 4;
        curChatAIIndex = 3;
        curPlayerIndex = 5;

        NextPurpose();

        LANGUAGE = "_KOR";

        purposeUI.SetActive(true);
        sendUI.SetActive(true);
        interectUI.SetActive(false);
        playerMessageUI.SetActive(false);
        moneySystemUI.SetActive(false);

        for (int i=0; i<objText.Length; i++)
        {
            objText[i].SetActive(false);
        }

        skipUI.SetActive(false);
    }

    public void SetSendUIActive(bool _active)
    {
        sendUI.SetActive(_active);
    }

    public void SetInterectUI(bool _active)
    {
        interectUI.SetActive(_active);
    }

    public void SetPurposeUI(bool _active)
    {
        purposeUI.SetActive(_active);
    }

    public void SetPlayerMessageUI(bool _active)
    {
        playerMessageUI.SetActive(_active);
    }

    public void SetSkipUI(bool _active)
    {
        skipUI.SetActive(_active);
    }

    public void SetMoneySystemUI(bool _active)
    {
        moneySystemUI.SetActive(_active);
    }

    public void NextPurpose()
    {
        if (curPurposIndex >= purposeMessageArr.Length || !purposeUI.activeSelf) return;

        purposeText.text = "���� ��ǥ: " + purposeMessageArr[curPurposIndex].GetMessage();
        curPurposIndex++;
    }
    public void AddPurposeIndex() { curPurposIndex++; }

    public void NextChatAI()
    {
        TextMeshProUGUI textMeshProGUI = new TextMeshProUGUI();
        textMeshProGUI.text = chatAIMessageArr[curChatAIIndex].GetMessage();
        chatGPTController.CheckSendMessageGUI(textMeshProGUI);

        curChatAIIndex++;
    }


    public void NextPlayerMessage()
    {
        playerMessageUI.SetActive(true);
        if (curPlayerIndex >= playerMessageArr.Length || !playerMessageUI.activeSelf) return;

        StartCoroutine(TypingCoroutine(playerMessageUI, playerText, playerMessageArr[curPlayerIndex].GetMessage()));

        curPlayerIndex++;
    }
    public void AddPlayerIndex() { curPurposIndex++; }

    // �Ϲ� ������Ʈ�� ���ϱ�
    public void ObjTalking(int _objIndex, int _objMessageIndex)
    {
        if (!objText[_objIndex] || !objMessageArr[_objMessageIndex]) return;

        Debug.Log("ObjTalking");
        objText[_objIndex].SetActive(true);
        StartCoroutine(TypingCoroutine(objText[_objIndex], objText[_objIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>(), objMessageArr[_objMessageIndex].GetMessage()));
    }

    public void SetCurObjIndex(int _index) { curObjIndex = _index; }
    public void SetCurObjMessageIndex(int _index) { curObjMessageIndex = _index; }

    IEnumerator TypingCoroutine(GameObject _ui, TextMeshProUGUI _messageText, string _text)
    {
        Debug.Log("ObjTalkingMessage");
        for (int j = 0; j <= _text.Length; ++j)
        {
            _messageText.text = _text.Substring(0, j);

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(3.0f);
        _messageText.text = "";
        yield return new WaitForSeconds(0.5f);
        _ui.SetActive(false);
    }


    // ���� ���� ��ư
    public void QuickGame()
    {
        Application.Quit();
    }
}
