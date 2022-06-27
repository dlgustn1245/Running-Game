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
    [System.NonSerialized]
    public bool chaseStart = false;

    public Text readyText;
    public Text scoreText;

    /// <summary>
    /// singleton pattern
    /// </summary>
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
    }

    void Update()
    {
        Time.timeScale = gamePaused ? 0.0f : 1.0f;
        SetPlayerScoreText();
    }

    public void PlayerScored(int amount)
    {
        gameScore += amount;
    }

    void SetPlayerScoreText()
    {
        scoreText.text = "Score : " + gameScore.ToString();
    }

    public void PlayerDead()
    {
        playerDead = true;
        SoundManager.Instace.StopGameBGM();
        SoundManager.Instace.PlayGameSFX("gameOver");
    }

    IEnumerator ShowReadyText()
    {
        int cnt = 0;
        gameStart = false;
        SoundManager.Instace.PlayGameSFX("countDown", 0.7f);
        while (cnt < 3)
        {
            readyText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            readyText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ++cnt;
        }
        gameStart = true;
        StartCoroutine(ChasePlayerStart());
        yield return new WaitForSeconds(0.35f);
        SoundManager.Instace.PlayGameBGM(0.7f);
    }

    IEnumerator ChasePlayerStart()
    {
        yield return new WaitForSeconds(2.5f);
        chaseStart = true;
    }
}