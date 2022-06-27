using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Text scoreText;

    void Update()
    {
        if(GameManager.Instance.playerDead){
            GameOver();
        }
    }

    void GameOver(){
        StartCoroutine(PauseGame());
        gameOverMenu.SetActive(true);
        scoreText.text = "score : " + GameManager.gameScore.ToString();
    }

    public void SaveButton(){
        GameManager.Instance.gamePaused = false;
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene("GameOverScene");
    }

    public void QuitGameButton(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.gamePaused = true;
    }
}
