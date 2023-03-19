using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float dir = player.GetPlayerDir();
            Vector3 movePos = new Vector3(player.transform.position.x - (dir * 2.0f), player.transform.position.y + 1.0f, player.transform.position.z);
            player.SetBlocking(true);
            player.MovePlayerPos(movePos);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.SetBlocking(false);
        }
    }
}
