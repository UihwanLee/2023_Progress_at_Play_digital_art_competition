using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    // Update is called once per frame
    void Update()
    {
        float playerPosX = this.player.transform.position.x + 1f;
        transform.position = new Vector3(playerPosX, transform.position.y, transform.position.z);
    }
}
