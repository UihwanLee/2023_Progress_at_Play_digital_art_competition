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
        LobbyMode();
    }

    private void LobbyMode()
    {
        float playerPosX = this.player.transform.position.x + 1f;
        float playerPosY = (this.player.GetComponent<PlayerController>().GetClimbing()) ? this.player.transform.position.y + 1f : transform.position.y;
        transform.position = new Vector3(playerPosX, playerPosY, transform.position.z);
    }


    public void MoveCamera(Vector3 _pos)
    {
        transform.position = new Vector3(_pos.x + 1f, _pos.y + 1f, transform.position.z);
    }
}
