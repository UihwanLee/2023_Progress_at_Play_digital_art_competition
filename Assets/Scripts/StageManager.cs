using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ���� �÷��̾� ��ġ (-2, -1, 0)

    [SerializeField]
    private GameObject SNOW_EFFECT;

    // SpawnPos
    [SerializeField]
    private Vector3 stage1SpawnPos; // (165.0f, 4.0f, 0.0f)

    // ���� StageName
    private string curStageName;

    // Scipts
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private MessageManager messageManager;

    // �̱������� ����

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
                // �ε� ��� + ChatGPT ���� + PlayerPos �̵�
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
