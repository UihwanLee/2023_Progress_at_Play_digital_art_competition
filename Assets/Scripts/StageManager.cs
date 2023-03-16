using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ���� �÷��̾� ��ġ (-2, -1, 0)

    // SpawnPos
    private Vector3 stage1SpawnPos;

    // ���� StageName
    private string curStageName;

    // Scipts
    [SerializeField]
    private PlayerController player;

    // �̱������� ����

    // Start is called before the first frame update
    void Start()
    {
        stage1SpawnPos = new Vector3(-40.0f, -56.0f, 0.0f);

        curStageName = null;
    }

    public void SpawnPlayerStage(string _stageName)
    {
        switch (_stageName)
        {
            case "Global Warmming":
                // �ε� ��� + ChatGPT ���� + PlayerPos �̵�
                player.MovePlayerPos(stage1SpawnPos);
                break;
            case "Inflaction":
                break;
        }
    }

    public void SetCurStageName(string _name) { curStageName = _name; }
    public string GetCurStageName() { return curStageName; }
}
