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
    private HingeJoint2D hj;
    private Animator anim;

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

    // Stage ����
    private bool isStageOn;
    private string curStageName;

    // ���� ����
    [SerializeField]
    private bool isGround;

    // Rope ����
    private bool isRopeAttached;
    private bool isClimbing;
    private Transform attachedTo;
    private Rigidbody2D curAttachedRope;
    private float pushForce = 10f;

    private GameObject disregard;
    public GameObject pulleySelected = null;

    [SerializeField]
    private GameObject chatGPTController;

    // Scripts
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private ScenenManager sceneManager;
    [SerializeField]
    private RopeManager ropeManager;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.hj = GetComponent<HingeJoint2D>();
        this.anim = GetComponent<Animator>();

        hj.enabled = false;
        isMoveable = true;

        isStageOn = false;
        curStageName = null;

        isGround = false;
        isClimbing = false;
        isRopeAttached = false;
        attachedTo = null;

        curAttachedRope = null;

        targetPosX = transform.position.x; 
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMovement();
        CheckClimbing();
    }

    // �̵� üũ �Լ�
    private void CheckMovement()
    {
        bool isTyping = chatGPTController.GetComponent<ChatGPTController>().IsTyping();
        bool isLoading = sceneManager.isLoading();

        // i) ������ �� ���� ��
        // i) Ÿ���� ���� �ƴҶ�
        // i) �ε� ���� �ƴ� ��
        // i) ���� ��� ���� �ƴ� ��
        if (isMoveable && !isTyping && !isLoading && !isClimbing)
        {
            //bool isMove = false;

            // ����
            if ((Input.GetKeyDown(KeyCode.Space) && isGround))
            {
                // �������� ���� ������ ������ �� �ְ� �������
                // �������� üũ 
                if (isStageOn && stageManager.GetCurStageName() != null)
                {
                    sceneManager.LoadStage();
                }
                else
                {
                    this.anim.SetTrigger("jump");
                    rb.velocity = Vector2.up * jumpForce;
                }
            }

            if((Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if(isRopeAttached && curAttachedRope != null)
                {
                    AttachRope(curAttachedRope);
                }
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
                anim.SetBool("walk", true);
                transform.localScale = new Vector3(dir * characterSize, characterSize, 1);

                targetPosX += dir * speed * Time.deltaTime;
            }
            else anim.SetBool("walk", false);

            float smoothPosX = Mathf.Lerp(transform.position.x, targetPosX, smooting);
            rb.transform.position = new Vector3(smoothPosX, transform.position.y, transform.position.z);
        }
    }

    private void CheckClimbing()
    {
        if (isClimbing)
        {
            anim.SetBool("walk", false);
            anim.SetBool("climb", true);
            pushForce = Random.Range(5f, 40f);
            rb.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Slide(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Slide(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetachRope();
            }
        }
    }

    private void AttachRope(Rigidbody2D rope)
    {
        rope.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = rope;
        hj.enabled = true;
        isClimbing = true;
        attachedTo = rope.gameObject.transform.parent;
    }

    private void DetachRope()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        isClimbing = false;
        hj.enabled = false;
        hj.connectedBody = null;

        anim.SetBool("climb", false);
    }

    public void Slide(int dir)
    {
        RopeSegment connection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (dir > 0)
        {
            if (connection.connectedAbove != null)
            {
                if (connection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = connection.connectedAbove;
                }
            }
        }
        else
        {
            if (connection.connectedBelow != null)
            {
                newSeg = connection.connectedBelow;
            }
        }

        if (newSeg != null)
        {
            transform.position = newSeg.transform.position;
            connection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }

    public void SetPlayerSpeed(float _speed) { speed = _speed; }
    public void MovePlayerPos(Vector3 _Pos) { transform.position = _Pos; targetPosX = transform.position.x; }

    public bool GetClimbing() { return isClimbing; }

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
        if(!isClimbing)
        {
            if(collision.gameObject.tag == "Rope"){
                isRopeAttached = true;
                if (attachedTo != collision.gameObject.transform.parent){
                    if(disregard == null || collision.gameObject.transform.parent.gameObject != disregard){
                        curAttachedRope = collision.gameObject.GetComponent<Rigidbody2D>();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            isStageOn = false;
            stageManager.SetCurStageName(null);
        }
        if (!isClimbing)
        {
            if (collision.gameObject.tag == "Rope")
            {
                isRopeAttached = false;
            }
        }
    }
}
