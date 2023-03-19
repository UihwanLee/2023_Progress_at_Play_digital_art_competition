using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectManager : MonoBehaviour
{
    private Interect curInterctObj;

    // Scripts
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private RopeManager[] ropeManager;

    // Start is called before the first frame update
    void Start()
    {
        curInterctObj = null;
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
            default:
                break;
        }
    }


    public void SetCurInterctObject(Interect _obj) { curInterctObj = _obj; }
    public Interect GetCurInterctObj() { return curInterctObj; }
}
