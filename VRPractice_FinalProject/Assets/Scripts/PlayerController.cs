using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public Camera theCamera;

    Rigidbody rb;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyObstacle();
        }

        PlayerRotate();
        SetAnimValue();
        PlayerFallDown();
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
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead || GameManager.Instance.gamePaused) return;

        Vector3 moveVector = this.transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    void PlayerRotate()
    {
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead || GameManager.Instance.gamePaused) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Rotate(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Rotate(0, 90, 0);
        }
    }

    void SetAnimValue()
    {
        anim.SetBool("GameStart", GameManager.Instance.gameStart);
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
}