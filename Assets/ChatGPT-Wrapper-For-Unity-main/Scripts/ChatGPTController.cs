using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    ChatGPT 컨트롤러 스크립트
*/

public class ChatGPTController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // ChatGPT 이동 변수
    [SerializeField]
    private float speed = 5.0f;
    private float smootingX = 0.01f;
    private float smootingY = 0.03f;
    private float targetPosX;
    private float targetPosY;

    private bool isMoveable;

    // ChatGPT 답변
    [SerializeField]
    private TextMeshProUGUI responseMessage;

    // Start is called before the first frame update
    void Start()
    {
        isMoveable = true;

        // 변수 초기화
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
            // Player를 따라가는 함수
            targetPosX = this.player.transform.position.x + -1.0f;
            targetPosY = this.player.transform.position.y + 1.0f;
            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(transform.position.y, targetPosY, smootingY);
            transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);
        }
    }

    // ChatGPT의 답변을 받아 텍스트에 표시하기
    public void setText(string _message)
    {
        responseMessage.text = _message;
    }
}
