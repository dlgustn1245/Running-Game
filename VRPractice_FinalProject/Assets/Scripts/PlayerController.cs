using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public int canDestroyDistance;
    public AudioClip running;
    public Camera theCamera;

    Rigidbody rb;
    Animator anim;
    AudioSource audioSource;
    int width;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        width = Screen.width / 2;
    }

    void Update()
    {
        anim.SetBool("GameStart", GameManager.Instance.gameStart);
        if (GameManager.Instance.playerDead) anim.SetTrigger("PlayerDead");

        if (Input.GetMouseButtonDown(0))
        {
            DestroyObstacle();
        }

        PlayerRotate();
        PlayerFallDown();
        PlayerFootStep();

        if (Input.mousePosition.x > width)
        {
            PlayerMoveHorizontal();
        }
        else if (Input.mousePosition.x < width)
        {
            PlayerMoveHorizontal(-1);
        }
    }

    void PlayerMoveHorizontal(int moveright = 1)
    {
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead) return;

        rb.MovePosition(rb.position + this.transform.right * moveSpeed * 0.5f * moveright * Time.deltaTime);
    }

    void PlayerFootStep()
    {
        if (!GameManager.Instance.gamePaused && GameManager.Instance.gameStart && !GameManager.Instance.playerDead)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = running;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void PlayerFallDown()
    {
        if (this.transform.position.y < -3.0f && !GameManager.Instance.playerDead)
        {
            GameManager.Instance.PlayerDead();
        }
    }

    void FixedUpdate()
    {
        PlayerMoveForward();
    }

    void PlayerMoveForward()
    {
        if (!GameManager.Instance.gameStart || GameManager.Instance.playerDead) return;

        rb.MovePosition(rb.position + this.transform.forward * moveSpeed * Time.deltaTime);
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

    void DestroyObstacle()
    {
        if (!GameManager.Instance.gameStart) return;
        
        Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, canDestroyDistance))
        {
            GameObject hitObj = hitInfo.collider.gameObject;
            if (hitObj.gameObject.CompareTag("Obstacle"))
            {
                Destroy(hitObj.gameObject);
                GameManager.Instance.PlayerScored(1);
            }
        }
    }

    public void SlowMoveSpeed()
    {
        StartCoroutine(SlowPlayer());
    }

    IEnumerator SlowPlayer()
    {
        moveSpeed -= 5;

        yield return new WaitForSeconds(1.0f);

        moveSpeed += 5;
    }
}