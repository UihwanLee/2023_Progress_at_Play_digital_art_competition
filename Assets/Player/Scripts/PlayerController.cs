using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Player ��Ʈ�ѷ� ��ũ��Ʈ
    Player �̵�, ȸ��, �ִϸ��̼� ���� ó���ϴ� ��ũ��Ʈ�̴�.
 */

public class PlayerController : MonoBehaviour
{
    // Player ����
    private Rigidbody2D rb;

    [SerializeField]
    private float characterSize = 1.35f;

    private bool isMoveable;

    [SerializeField]
    private float speed = 10.0f;
    private float smooting = 0.1f;
    private float targetPosX;

    [SerializeField]
    private float jumpForce = 10.0f;
    private float fallMultiplier = 2.5f; // The multiplier for the player's falling velocity
    private float lowJumpMultiplier = 2f;

    [SerializeField]
    private bool isGround;



    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        isMoveable = true;

        targetPosX = transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMovement();
    }

    // �̵� üũ �Լ�
    private void CheckMovement()
    {
        if(isMoveable)
        {
            //bool isMove = false;

            // ����
            if ((Input.GetKeyDown(KeyCode.Space) && isGround))
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (rb.velocity.y < 0)
            { // Check if the player is falling
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // Increase the player's falling velocity
            }
            else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
            { // Check if the player is falling but not holding the jump button
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // Decrease the player's jump velocity
            }

            // �¿� �̵�
            float dir = 0;
            if (Input.GetKey(KeyCode.RightArrow)) dir = 1.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) dir = -1.0f;


            // ���⿡ ���� �̹��� ����
            if (dir != 0)
            {
                transform.localScale = new Vector3(dir * characterSize, characterSize, 1);

                targetPosX += dir * speed * Time.deltaTime;
            }

            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smooting);
            transform.position = new Vector3(smoothPosX, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGround = false;
    }
}
