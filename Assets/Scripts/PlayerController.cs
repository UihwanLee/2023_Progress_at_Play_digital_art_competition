using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Player 컨트롤러 스크립트
    Player 이동, 회전, 애니메이션 등을 처리하는 스크립트이다.
 */

public class PlayerController : MonoBehaviour
{
    // Player 변수
    private Rigidbody2D rb;

    [SerializeField]
    private float characterSize = 1.35f;

    private bool isMoveable;

    [SerializeField]
    private float speed = 0.2f;
    private float smooting = 0.1f;
    private float targetPosX;

    [SerializeField]
    private float jumpForce = 10.0f;
    private float fallMultiplier = 2.5f; // The multiplier for the player's falling velocity
    private float lowJumpMultiplier = 2f;

    // Stage 변수
    private bool isStageOn;
    private string curStageName;

    [SerializeField]
    private bool isGround;

    [SerializeField]
    private GameObject chatGPTController;

    // Scripts
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private ScenenManager sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        isMoveable = true;

        isStageOn = false;
        curStageName = null;

        targetPosX = transform.position.x; 
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMovement();
    }

    // 이동 체크 함수
    private void CheckMovement()
    {
        bool isTyping = chatGPTController.GetComponent<ChatGPTController>().IsTyping();
        bool isLoading = sceneManager.isLoading();


        // i) 움직일 수 있을 때
        // i) 타이핑 중이 아닐때
        // i) 로딩 중이 아닐 때
        if (isMoveable && !isTyping && !isLoading)
        {
            //bool isMove = false;

            // 점프
            if ((Input.GetKeyDown(KeyCode.Space) && isGround))
            {
                // 스테이즈 들어가고 나서도 점프할 수 있게 해줘야함
                // 스테이즈 체크 
                if(isStageOn && stageManager.GetCurStageName() != null)
                {
                    sceneManager.LoadStage();
                }
                else rb.velocity = Vector2.up * jumpForce;
            }

            if (rb.velocity.y < 0)
            { // Check if the player is falling
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // Increase the player's falling velocity
            }
            else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
            { // Check if the player is falling but not holding the jump button
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // Decrease the player's jump velocity
            }

            // 좌우 이동
            float dir = 0;
            if (Input.GetKey(KeyCode.RightArrow)) dir = 1.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) dir = -1.0f;


            // 방향에 따라 이미지 반전
            if (dir != 0)
            {
                transform.localScale = new Vector3(dir * characterSize, characterSize, 1);

                targetPosX += dir * speed * Time.deltaTime;
            }

            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smooting);
            transform.position = new Vector3(smoothPosX, transform.position.y, transform.position.z);
        }
    }

    public void MovePlayerPos(Vector3 _Pos) { transform.position = _Pos; targetPosX = transform.position.x; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isStageOn = true;
            stageManager.SetCurStageName(collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isStageOn = false;
            stageManager.SetCurStageName(null);
        }
    }
}
