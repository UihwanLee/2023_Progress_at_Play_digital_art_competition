using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    ChatGPT ��Ʈ�ѷ� ��ũ��Ʈ
*/

public class ChatGPTController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // ChatGPT �̵� ����
    [SerializeField]
    private float speed = 5.0f;
    private float smootingX = 0.01f;
    private float smootingY = 0.03f;
    private float targetPosX;
    private float targetPosY;

    private bool isMoveable;

    // ChatGPT �亯
    [SerializeField]
    private TextMeshProUGUI responseMessage;

    // Start is called before the first frame update
    void Start()
    {
        isMoveable = true;

        // ���� �ʱ�ȭ
        targetPosX = 0.0f;
        targetPosY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        if(isMoveable)
        {
            // Player�� ���󰡴� �Լ�
            targetPosX = this.player.transform.position.x + -1.0f;
            targetPosY = this.player.transform.position.y + 1.0f;
            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(transform.position.y, targetPosY, smootingY);
            transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);
        }
    }

    // ChatGPT�� �亯�� �޾� �ؽ�Ʈ�� ǥ���ϱ�
    public void setText(string _message)
    {
        responseMessage.text = _message;
    }
}
