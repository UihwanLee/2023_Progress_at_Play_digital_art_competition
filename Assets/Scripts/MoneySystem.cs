using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI busCostText;
    [SerializeField]
    private TextMeshProUGUI myMoneyText;
    [SerializeField]
    private TextMeshProUGUI remainTimeText;

    private float limitTime; // 몇초마다 인플레이션 줄건지
    private float remainTime;
    private float busCost;
    private float inflationRate; // 인플레이션 얼마나 증가할건지

    private float playerGainTime; // 몇초마다 Player 돈을 줄것인지

    private PlayerController player;
    private InterectManager interectManager;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        interectManager = GameObject.Find("InterectManager").GetComponent<InterectManager>();

        limitTime = Random.Range(30f, 60f);
        remainTime = limitTime;
        busCost = 2000f;
        inflationRate = 1.1f;

        busCostText.text = busCost.ToString("F0");
        myMoneyText.text = player.GetMoney().ToString();
        remainTimeText.text = remainTime.ToString("F0");


        playerGainTime = 2f;
    }

    private void Update()
    {
        if(remainTime > 0)
        {
            remainTimeText.text = remainTime.ToString("F0");
            remainTime -= Time.deltaTime;
        }
        else
        {
            busCost = busCost * inflationRate;
            remainTime = limitTime;

            busCostText.text = busCost.ToString("F0");
            remainTimeText.text = remainTime.ToString("F0");

            remainTime = limitTime;
        }
        myMoneyText.text = player.GetMoney().ToString();

        // Working System 
        if (Input.GetKey(KeyCode.Space) && interectManager.GetCurInterctObj())
        {
            if(interectManager.GetCurInterctObj().GetObjName() == "Cafe")
            {
                interectManager.SetIsWorking(true);

                player.transform.position = new Vector3(298f, 4f, player.transform.position.z);

                // anim

                // 2초마다 50원을 벌어드림
                // 2초 슬라이드 생성(애니메이션으로 해결)
                if (playerGainTime > 0)
                {
                    playerGainTime -= Time.deltaTime;
                }
                else
                {
                    player.SetMoney(player.GetMoney() + 50);
                    playerGainTime = 2f;
                }
            }     
        }

        interectManager.SetIsWorking(false);
    }

    public float GetCurBusCost() { return busCost; }
}
