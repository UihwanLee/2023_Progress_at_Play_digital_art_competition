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

    // Interct 변수

    [Header("STAGE1")]
    // 물뿌리개
    private Interect waterPail;
    private bool waterPailEmpty;
    [SerializeField]
    private Sprite[] waterPailSprites;

    // 새싹
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
                    // Follow 기능 키기
                    this.waterPail = curInterctObj;
                    curInterctObj.gameObject.GetComponent<SmoothFollow_Object>().enabled = true;
                    curInterctObj.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    //curInterctObj.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    //curInterctObj.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                }
                break;
            case "Pump":
                {
                    // Player가 Water_Pail 오브젝트를 가지고 있고 && Water_Pail이 비워져있을떄
                    if(this.waterPail!=null && waterPailEmpty)
                    {
                        this.waterPail.gameObject.GetComponent<SpriteRenderer>().sprite = waterPailSprites[1];
                        waterPailEmpty = false;
                    }
                }
                break;
            case "Sprout":
                {
                    // Player가 Water_Pail 오브젝트를 가지고 있고 && Water_Pail이 다 채워졌을 때
                    if (this.waterPail && !waterPailEmpty)
                    {
                        this.waterPail.gameObject.GetComponent<SpriteRenderer>().sprite = waterPailSprites[0];
                        waterPailEmpty = true;

                        // 새싹 키우기
                        curSproutSpriteIndex++;

                        // 만약 다 자랐을 시 Anim_Stage1_Field3 재생
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
                    // 첫 Interect시
                    if (this.curInterctObj.isInterect == false)
                    {
                        // Scene 가동
                        sceneManager.Anim_STAGE2_Field1();

                        // Cafe Interect 활성화
                        cafe.GetComponent<BoxCollider2D>().enabled = true;
                        cafe.GetComponent<Interect>().enabled = true;
                    }
                    else
                    {
                        if(moneySystem == null) break;

                        // 버스 비용보다 적을 시
                        if (moneySystem.GetCurBusCost() > player.GetMoney())
                        {
                            uiManager.ObjTalking(curInterctObj.index, 3);
                        }
                        // 버스 비용을 만족 했을 시
                        else
                        {
                            // Scene 가동
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
