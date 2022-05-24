using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int gameScore = 0;

    [System.NonSerialized]
    public bool gameStart = false;
    [System.NonSerialized]
    public bool playerDead = false;
    [System.NonSerialized]
    public bool gamePaused = false;

    public Text readyText;
    public Text scoreText;

    static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this.gameObject);
    }

    void Start()
    {
        StartCoroutine(ShowReadyText());
        SetPlayerScoreText();
    }

    void Update()
    {
       Time.timeScale = gamePaused ? 0.0f : 1.0f;
    }

    public void PlayerScored(int amount)
    {
        gameScore += amount;
    }

    void SetPlayerScoreText()
    {
        scoreText.text = "Score : " + gameScore.ToString();
    }

    public void PlayerDead(){
        playerDead = true;
    }

    IEnumerator ShowReadyText()
    {
        int cnt = 0;
        gameStart = false;
        while (cnt < 3)
        {
            readyText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            readyText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ++cnt;
        }
        gameStart = true;
    }
}