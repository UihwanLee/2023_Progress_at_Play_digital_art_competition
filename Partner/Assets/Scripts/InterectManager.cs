using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectManager : MonoBehaviour
{
    [SerializeField]
    private Interect curInterctObj;

    // Scripts
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private RopeManager[] ropeManager;

    // Interct ����

    [Header("STAGE1")]
    // ���Ѹ���
    private Interect waterPail;
    private bool waterPailEmpty;
    [SerializeField]
    private Sprite[] waterPailSprites;

    // ����
    private int curSproutSpriteIndex;
    [SerializeField]
    private Sprite[] sproutSprites;
    [SerializeField]
    private Sprite[] sproutHighlightSprites;


    [Header("STAGE2")]
    [SerializeField]
    private GameObject cafe;
    [SerializeField]
    private MoneySystem moneySystem;
    private bool isWorking;

    // Scripts
    [Header("Scripts")]
    [SerializeField]
    private ScenenManager sceneManager;
    [SerializeField]
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        curInterctObj = null;

        waterPail = null;
        waterPailEmpty = true;

        curSproutSpriteIndex = 0;

        isWorking = false;
    }

    public void Interect()
    {
        if (curInterctObj.changeSprite != null) curInterctObj.ChangeSprite();
        AciveInterect(curInterctObj.GetObjName());
    }

    private void AciveInterect(string _objName)
    {
        switch (_objName)
        {
            case "RopeDevice1":
                player.SetInterect(false);
                curInterctObj.isActive = true;
                ropeManager[0].GenerateRope();
                break;
            case "Water_Pail":
                {
                    // Follow ��� Ű��
                    this.waterPail = curInterctObj;
                    curInterctObj.gameObject.GetComponent<SmoothFollow_Object>().enabled = true;
                    curInterctObj.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    //curInterctObj.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    //curInterctObj.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                }
                break;
            case "Pump":
                {
                    // Player�� Water_Pail ������Ʈ�� ������ �ְ� && Water_Pail�� �����������
                    if(this.waterPail!=null && waterPailEmpty)
                    {
                        this.waterPail.gameObject.GetComponent<SpriteRenderer>().sprite = waterPailSprites[1];
                        waterPailEmpty = false;
                    }
                }
                break;
            case "Sprout":
                {
                    // Player�� Water_Pail ������Ʈ�� ������ �ְ� && Water_Pail�� �� ä������ ��
                    if (this.waterPail && !waterPailEmpty)
                    {
                        this.waterPail.gameObject.GetComponent<SpriteRenderer>().sprite = waterPailSprites[0];
                        waterPailEmpty = true;

                        // ���� Ű���
                        curSproutSpriteIndex++;

                        // ���� �� �ڶ��� �� Anim_Stage1_Field3 ���
                        if (curSproutSpriteIndex >= 3)
                        {
                            this.waterPail.gameObject.SetActive(false);
                            sceneManager.Anim_STAGE1_Field3(this.curInterctObj);
                            curInterctObj.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        }
                        else
                        {
                            this.curInterctObj.gameObject.GetComponent<SpriteRenderer>().sprite = sproutSprites[curSproutSpriteIndex];
                            this.curInterctObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sproutHighlightSprites[curSproutSpriteIndex];
                            this.curInterctObj.transform.GetChild(1).transform.position = new Vector3(this.curInterctObj.transform.GetChild(1).transform.position.x,
                                this.curInterctObj.transform.GetChild(1).transform.position.y - 1f, this.curInterctObj.transform.GetChild(1).transform.position.z);
                        }
                    }
                }
                break;
            case "BusDoor":
                {
                    // ù Interect��
                    if (this.curInterctObj.isInterect == false)
                    {
                        // Scene ����
                        sceneManager.Anim_STAGE2_Field1();

                        // Cafe Interect Ȱ��ȭ
                        cafe.GetComponent<BoxCollider2D>().enabled = true;
                        cafe.GetComponent<Interect>().enabled = true;
                    }
                    else
                    {
                        if(moneySystem == null) break;

                        // ���� ��뺸�� ���� ��
                        if (moneySystem.GetCurBusCost() > player.GetMoney())
                        {
                            uiManager.ObjTalking(curInterctObj.index, 3);
                        }
                        // ���� ����� ���� ���� ��
                        else
                        {
                            // Scene ����
                        }
                    }
                    this.curInterctObj.isInterect = true;
                }
                break;
            case "cafe":
                {
                    if (Input.GetKey(KeyCode.Space))
                    {

                    }
                    else
                    {

                    }
                }
                break;
            default:
                break;
        }
    }

    public void SetCurInterctObject(Interect _obj) { curInterctObj = _obj; }
    public Interect GetCurInterctObj() { return curInterctObj; }

    public bool IsWorking() { return isWorking; }
    public void SetIsWorking(bool _isWorking) { isWorking = _isWorking; }
}
