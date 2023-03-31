using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttach : MonoBehaviour
{
    private float targetPosX;
    private float targetPosY;

    [SerializeField]
    private GameObject attachObj;
    [SerializeField]
    private Vector3 movePosX;
    [SerializeField]
    private Vector3 movePosY;
    private float smootingX; // = 0.01f;
    private float smootingY; // = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        // 변수 초기화
        targetPosX = 0.0f;
        targetPosY = 0.0f;

        smootingX = 0.1f;
        smootingY = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Attach();
    }

    private void Attach()
    {
        if (this.gameObject.activeSelf && attachObj != null)
        {
            targetPosX = Camera.main.WorldToScreenPoint(attachObj.transform.position + movePosX).x;
            targetPosY = Camera.main.WorldToScreenPoint(attachObj.transform.position + movePosY).y;

            //float smoothPosX = Mathf.Lerp(this.transform.position.x, targetPosX, smootingX);
            //float smoothPosY = Mathf.Lerp(this.transform.position.y, targetPosY, smootingY);
            this.transform.position = new Vector3(targetPosX, targetPosY, 0.0f);

        }
    }
}
