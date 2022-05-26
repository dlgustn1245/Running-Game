using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Text countDownText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.gamePaused)
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        GameManager.Instance.gamePaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        StartCoroutine(CountDown());
        pauseMenu.SetActive(false);
    }

    public void ToTitleButton()
    {
        GameManager.Instance.gamePaused = false;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("기말_2017136099_이현수");
    }

    IEnumerator CountDown()
    {
        countDownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1.0f);
        }
        countDownText.gameObject.SetActive(false);
        GameManager.Instance.gamePaused = false;
    }
}