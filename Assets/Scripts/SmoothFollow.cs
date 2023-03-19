using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private float smootingX = 0.01f;
    private float smootingY = 0.03f;
    private float targetPosX;
    private float targetPosY;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        // 변수 초기화
        targetPosX = 0.0f;
        targetPosY = 0.0f;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        // ChatGPT 말풍선 움직임 확인
        if (this.gameObject.activeSelf && player != null)
        {
            targetPosX = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 3f, 0)).x;
            targetPosY = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 2.4f, 0)).y;

            float smoothPosX = Mathf.Lerp(this.transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(this.transform.position.y, targetPosY, smootingY);
            this.transform.position = new Vector3(smoothPosX, smoothPosY, 0.0f);

        }
    }
}
