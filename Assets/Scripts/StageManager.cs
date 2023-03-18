using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 기존 플레이어 위치 (-2, -1, 0)

    [SerializeField]
    private GameObject SNOW_EFFECT;

    // SpawnPos
    [SerializeField]
    private Vector3 stage1SpawnPos; // (165.0f, 4.0f, 0.0f)

    // 현재 StageName
    private string curStageName;

    // Scipts
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private MessageManager messageManager;

    // 싱글톤으로 관리

    // Start is called before the first frame update
    void Start()
    {
        SNOW_EFFECT.SetActive(false);

        curStageName = null;
    }

    public void SpawnPlayerStage(string _stageName)
    {
        switch (_stageName)
        {
            case "Global Warmming":
                // 로딩 장면 + ChatGPT 설명 + PlayerPos 이동
                SNOW_EFFECT.SetActive(true);
                player.MovePlayerPos(stage1SpawnPos);
                player.SetPlayerSpeed(2f);
                cameraController.MoveCamera(stage1SpawnPos);

                messageManager.SendMessage();
                break;
            case "Inflaction":
                break;
        }
    }

    public void SetCurStageName(string _name) { curStageName = _name; }
    public string GetCurStageName() { return curStageName; }
}
