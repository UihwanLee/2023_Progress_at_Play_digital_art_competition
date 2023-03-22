using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interect : MonoBehaviour
{
    [SerializeField]
    private string objectName;

    // ������Ʈ Message ó�� ����
    // ���� ������Ʈ �����ϰ� -1�� �ʱ�ȭ
    // ���� ������Ʈ�� ������� �ε��� �ʱ�ȭ
    public int index; 

    private SpriteRenderer spriteRenderer;

    // ��ȣ�ۿ� �� Sprtie �ٲ� �̹���
    public Sprite changeSprite;

    // Scritpt
    private InterectManager interectManager;
    private UIManager uiManager;

    public bool isActive;
    public bool isInterect;

    // Start is called before the first frame update
    void Start()
    {
        this.isInterect = false;
        this.isActive = false;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.interectManager = GameObject.Find("InterectManager").GetComponent<InterectManager>();
        this.uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Highligh Sprite Enable false
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            // Highligh Sprite Enable
            if (!isActive) this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            if (!isActive) this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            collision.GetComponent<PlayerController>().SetInterect(true);
            interectManager.SetCurInterctObject(this);

            if (!isActive) this.uiManager.SetInterectUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            // Highligh Sprite Enable false
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            collision.GetComponent<PlayerController>().SetInterect(false);
            interectManager.SetCurInterctObject(null);

            this.uiManager.SetInterectUI(false);
        }
    }

    public void ChangeSprite() { this.spriteRenderer.sprite = changeSprite; }
    public string GetObjName() { return objectName; }
}
