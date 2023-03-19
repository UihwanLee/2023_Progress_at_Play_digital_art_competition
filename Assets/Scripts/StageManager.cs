using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    // 현재 StageName
    private string curStageName;

    // STAGE 단계
    private bool clearSTAGE1;
    private bool clearSTAGE2;
    private bool clearSTAGE3;

    // STAZE1 : Gloabal Warmming
    [Header("STAGE1")]
    [SerializeField]
    private GameObject STAGE_BG;
    [SerializeField]
    private GameObject SNOW_EFFECT;
    [SerializeField]
    private GameObject stage1_field1;
    [SerializeField]
    private GameObject stage1_field2;
    [SerializeField]
    private Vector3 stage1SpawnPos; // (165.0f, 4.0f, 0.0f) 기존 플레이어 위치 (-2, -1, 0)


    // Scipts
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private ScenenManager sceneManager;

    // 싱글톤으로 관리

    // Start is called before the first frame update
    void Start()
    {
        clearSTAGE1 = false;
        clearSTAGE2 = false;
        clearSTAGE3 = false;

        SNOW_EFFECT.SetActive(false);

        //stage1_field1.SetActive(false);
        stage1_field2.SetActive(false);

        curStageName = null;
    }

    public void SpawnPlayerStage(string _stageName)
    {
        switch (_stageName)
        {
            case "Global Warmming":
                // 로딩 장면 + ChatGPT 설명 + PlayerPos 이동
                uiManager.SetSendUIActive(false);
                player.MovePlayerPos(stage1SpawnPos);
                player.SetPlayerSpeed(2f);
                sceneManager.Anim_STAGE1_Field1();
                break;
            case "Inflaction":
                break;
            default:
                break;
        }
    }

    // 스테이즈 클리어 상태를 가지고 해당 스테이즈에 들어갈 수 있는지 판단하는 함수
    public bool CheckEnterSTAGE(string _stageName)
    {
        bool canEnter = false;
        switch (_stageName)
        {
            case "Global Warmming":
                canEnter = !clearSTAGE1;
                break;
            case "Inflaction":
                canEnter = (clearSTAGE1 && !clearSTAGE2 && !clearSTAGE3);
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
                
                break;
            default:
                break;
        }

        return field;
    }

    public void SetCurStageName(string _name) { curStageName = _name; }
    public string GetCurStageName() { return curStageName; }
}
