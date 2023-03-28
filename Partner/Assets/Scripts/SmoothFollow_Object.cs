using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow_Object : MonoBehaviour
{
    [SerializeField]
    Vector3 size;

    // Player와 떨어져있는 정도
    [SerializeField]
    Vector2 distX;
    [SerializeField]
    float distY;

    private float curDist = 0.0f;

    // 바라보고 있는 방향(왼쪽: -1, 오른쪽: 1)
    [SerializeField]
    float dir;

    // Player 따라가는 속도
    [SerializeField]
    private float smootingX;
    [SerializeField]
    private float smootingY;

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
        if (this.gameObject.activeSelf && player != null)
        {
            // 이미지 반전
            if(player.GetPlayerDir()!=0) this.transform.localScale = new Vector3(player.GetPlayerDir() * dir * size.x, size.y, size.z);


            if (player.GetComponent<PlayerController>().GetPlayerDir() > 0) curDist = distX.x;
            if (player.GetComponent<PlayerController>().GetPlayerDir() < 0) curDist = distX.y;
            targetPosX = this.player.transform.position.x + curDist;
            targetPosY = this.player.transform.position.y + distY;

            float smoothPosX = Mathf.Lerp(this.transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(this.transform.position.y, targetPosY, smootingY);
            this.transform.position = new Vector3(smoothPosX, smoothPosY, 0.0f);
        }
    }
}
