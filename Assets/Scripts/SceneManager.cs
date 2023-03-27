using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

    }



    public void ThinkPicture(Text _textUI, string _text, GameObject _selectUI)
    {
        StartCoroutine(ThinkPictureCoroutine(_textUI, _text, _selectUI));
    }

    IEnumerator ThinkPictureCoroutine(Text _textUI, string _text, GameObject _selectUI)
    {
        yield return new WaitForSeconds(1f);
        uiManager.SetETCUI(true, false);
        yield return new WaitForSeconds(0.5f);
        uiManager.SetETCUI(true, true);
        yield return new WaitForSeconds(0.6f);
        uiManager.SetSelectCanvasUI(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Typing(_textUI, _text));
        yield return new WaitForSeconds(1f);
        _selectUI.SetActive(true);
    }

    // 자막 타이핑 이펙트
    IEnumerator Typing(Text _textUI, string _text)
    {
        _textUI.text = "";
        for (int i = 0; i <= _text.Length; i++)
        {
            _textUI.text = _text.Substring(0, i);
            //textBox.GetComponent<Text>().text = text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
