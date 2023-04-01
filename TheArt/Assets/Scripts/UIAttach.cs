using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float orthoOrg;
    private float orthoCurr;
    private Vector3 scaleOrg;
    private Vector3 posOrg;


    // Start is called before the first frame update
    void Start()
    {
        // 변수 초기화
        targetPosX = 0.0f;
        targetPosY = 0.0f;

        smootingX = 0.1f;
        smootingY = 0.1f;


        orthoOrg = Camera.main.orthographicSize;
        orthoCurr = orthoOrg;
        scaleOrg = transform.localScale;
        posOrg = Camera.main.WorldToViewportPoint(transform.position);
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
            this.transform.position = new Vector3(targetPosX, targetPosY, 0.0f);

        }
    }

    private void SetScale()
    {
        var osize = Camera.main.orthographicSize;
        if (orthoCurr != osize)
        {
            transform.localScale = scaleOrg * osize / orthoOrg;
            orthoCurr = osize;
            transform.position = Camera.main.ViewportToWorldPoint(posOrg);
        }
    }
}
