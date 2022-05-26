using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public int jumpPower;
    public Camera theCamera;

    Rigidbody rb;
    Animator anim;
    bool isJumping;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        isJumping = false;
    }

    void Update()
    {
        anim.SetBool("GameStart", GameManager.Instance.gameStart);

        if (Input.GetMouseButtonDown(0))
        {
            DestroyObstacle();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isJump");
        }

        PlayerRotate();
        PlayerFallDown();
        PlayerJump();
    }

    void PlayerFallDown()
    {
        if (this.transform.position.y <= -3.0f)
        {
            GameManager.Instance.playerDead = true;
        }
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead) return;

        Vector3 moveVector = this.transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    void PlayerRotate()
    {
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Rotate(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Rotate(0, 90, 0);
        }
    }

    void PlayerJump()
    {
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else return;
        }
    }

    void DestroyObstacle()
    {
        if (!GameManager.Instance.gameStart) return;

        Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, int.MaxValue))
        {
            Transform hitObj = hitInfo.collider.transform;
            if (hitObj.gameObject.CompareTag("Obstacle"))
            {
                Destroy(hitObj.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}