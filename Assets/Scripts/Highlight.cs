using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private float originSize;
    private float highlightSize;

    // Start is called before the first frame update
    void Start()
    {
        originSize = this.transform.GetChild(0).transform.localScale.x;
        highlightSize = 0.8f;

        // Highligh Sprite Enable false
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }


    // 플레이어 다가왔을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Lecture 크기 키우기
            this.transform.GetChild(0).transform.localScale = new Vector3(highlightSize, highlightSize, 1);

            // Highligh Sprite Enable
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Lecture 크기 원상복귀
            this.transform.GetChild(0).transform.localScale = new Vector3(originSize, originSize, 1);

            // Highligh Sprite Enable false
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
