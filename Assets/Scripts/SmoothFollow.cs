using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private float targetPosX;
    private float targetPosY;

    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private Vector3 movePosX;
    [SerializeField]
    private Vector3 movePosY;
    [SerializeField]
    private float smootingX; // = 0.01f;
    [SerializeField]
    private float smootingY; // = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        // 변수 초기화
        targetPosX = 0.0f;
        targetPosY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        // ChatGPT 말풍선 움직임 확인
        if (this.gameObject.activeSelf && obj != null)
        {
            targetPosX = Camera.main.WorldToScreenPoint(obj.transform.position + movePosX).x;
            targetPosY = Camera.main.WorldToScreenPoint(obj.transform.position + movePosY).y;

            float smoothPosX = Mathf.Lerp(this.transform.position.x, targetPosX, smootingX);
            float smoothPosY = Mathf.Lerp(this.transform.position.y, targetPosY, smootingY);
            this.transform.position = new Vector3(smoothPosX, smoothPosY, 0.0f);

        }
    }
}
