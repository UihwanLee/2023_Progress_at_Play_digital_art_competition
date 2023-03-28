using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool isMovalbe;

    private void Start()
    {
        isMovalbe = false;
    }

    // Update is called once per frame
    private void Update()
    {
        LobbyMode();
    }

    private void LobbyMode()
    {
        isMovalbe = !(this.player.GetComponent<PlayerController>().GetAnim());
        if (isMovalbe)
        {
            float playerPosX = this.player.transform.position.x + 1f;
            float playerPosY = (this.player.GetComponent<PlayerController>().GetClimbing()) ? this.player.transform.position.y + 1f : transform.position.y;
            transform.position = new Vector3(playerPosX, playerPosY, transform.position.z);
        }
    }


    public void MoveCamera(Vector3 _pos)
    {
        transform.position = new Vector3(_pos.x + 1f, _pos.y + 1f, transform.position.z);
    }

    public void AnimCamera(Vector3 _startPos, Vector3 _endPos)
    {
        MoveCamera(_startPos);
        float smoothPosX = Mathf.Lerp(transform.position.x, _endPos.x, 0.01f);
        float smoothPosY = Mathf.Lerp(transform.position.y, _endPos.y, 0.01f);
        transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);
    }
}
