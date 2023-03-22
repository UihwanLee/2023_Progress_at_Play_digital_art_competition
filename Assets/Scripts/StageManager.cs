using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    // ���� StageName
    private string curStageName;

    // STAGE �ܰ�
    private int clearIndex;
    [SerializeField]
    private bool clearSTAGE1;
    private bool clearSTAGE2;
    private bool clearSTAGE3;

    // STAZE1 : Gloabal Warmming
    [Header("STAGE1")]
    [SerializeField]
    private GameObject STAGE1_BG;
    [SerializeField]
    private Sprite[] BG_ARR;
    [SerializeField]
    private GameObject SNOW_EFFECT;
    [SerializeField]
    private GameObject stage1_field1;
    [SerializeField]
    private GameObject stage1_field2;
    [SerializeField]
    private GameObject stage1_field3;
    [SerializeField]
    private Vector3 stage1SpawnPos; // (165.0f, 4.0f, 0.0f) ���� �÷��̾� ��ġ (-2, -1, 0)

    // STAZE1 : Gloabal Warmming
    [Header("STAGE2")]
    [SerializeField]
    private GameObject stage2_field1;
    [SerializeField]
    private GameObject stage2_field2;
    [SerializeField]
    private GameObject stage2_field3; 
    [SerializeField]
    private Vector3 stage2SpawnPos; // (285.0f, 4.0f, 0.0f)



    // Lobby CurPos
    private Vector3 curPlayerLobbyPos;


    // Scipts
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private ScenenManager sceneManager;

    // �̱������� ����

    // Start is called before the first frame update
    void Start()
    {
        clearIndex = 0;

        clearSTAGE1 = false;
        clearSTAGE2 = false;
        clearSTAGE3 = false;

        SNOW_EFFECT.SetActive(false);

        //stage1_field1.SetActive(false);
        stage1_field2.SetActive(false);
        stage1_field3.SetActive(false);

        curStageName = null;

        curPlayerLobbyPos = player.transform.position;
    }

    public void SpawnPlayerStage(string _stageName)
    {
        switch (_stageName)
        {
            case "Stage End":
                {
                    uiManager.SetSendUIActive(true);
                    player.MovePlayerPos(new Vector3(curPlayerLobbyPos.x, curPlayerLobbyPos.y + 1.0f, curPlayerLobbyPos.z));
                    cameraController.MoveCamera(new Vector3(curPlayerLobbyPos.x, curPlayerLobbyPos.y, curPlayerLobbyPos.z));
                    uiManager.NextPurpose();
                }
                break;
            case "Global Warmming":
                // �ε� ��� + ChatGPT ���� + PlayerPos �̵�
                uiManager.SetSendUIActive(false);
                curPlayerLobbyPos = player.transform.position;
                player.MovePlayerPos(stage1SpawnPos);
                player.SetPlayerSpeed(2f);
                sceneManager.Anim_STAGE1_Field1();
                break;
            case "Inflaction":
                uiManager.SetSendUIActive(false);
                curPlayerLobbyPos = player.transform.position;
                player.MovePlayerPos(stage2SpawnPos);
                cameraController.MoveCamera(player.transform.position);
                uiManager.NextPurpose();
                break;
            default:
                break;
        }
    }

    // �������� Ŭ���� �Լ�
    public void SetStageClear()
    {
        clearIndex++;
        switch(clearIndex)
        {
            case 1:
                clearSTAGE1 = true;
                break;
            case 2:
                clearSTAGE2 = true;
                break;
            case 3:
                clearSTAGE3 = true;
                break;
            default:
                break;
        }
    }

    // �������� Ŭ���� ���¸� ������ �ش� ������� �� �� �ִ��� �Ǵ��ϴ� �Լ�
    public bool CheckEnterSTAGE(string _stageName)
    {
        bool canEnter = false;
        switch (_stageName)
        {
            case "Stage End":
                canEnter = true;
                break;
            case "Global Warmming":
                canEnter = !clearSTAGE1;
                break;
            case "Inflaction":
                canEnter = true;
                //canEnter = (clearSTAGE1 && !clearSTAGE2 && !clearSTAGE3);
                break;
            case "LowBirthRate":
                canEnter = (clearSTAGE1 && clearSTAGE2 && !clearSTAGE3);
                break;
            default:
                break;
        }
        return canEnter;
    }

    public void SetEffect(string _effectName, bool _active)
    {
        switch (_effectName)
        {
            case "SNOW_EFFECT":
                SNOW_EFFECT.SetActive(_active);
                break;
            default:
                break;
        }
    }


    public GameObject GetStageField(string _fieldName)
    {
        GameObject field = null;
        switch (_fieldName)
        {
            case "STAGE_FIELD1":
                field = stage1_field1;
                break;
            case "STAGE_FIELD2":
                field = stage1_field2;
                break;
            case "STAGE_FIELD3":
                field = stage1_field3;
                break;
            default:
                break;
        }

        return field;
    }

    public void SetStage1BG(int index) { STAGE1_BG.GetComponent<SpriteRenderer>().sprite = BG_ARR[index]; }
    public GameObject GetStage1BG() { return STAGE1_BG; }

    public void SetCurStageName(string _name) { curStageName = _name; }
    public string GetCurStageName() { return curStageName; }
}
