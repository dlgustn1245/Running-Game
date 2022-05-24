using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScene : MonoBehaviour
{
    public Text[] scoreList;
    public Text scoreText;
    public Text newRecord;

    int score;

    void Awake() 
    {
        //PlayerPrefs.DeleteAll();
        InitRanking();
    }

    void Start()
    {
        score = GameManager.gameScore;
        scoreText.text = "Score : " + score.ToString();
        SaveScore();
        LoadScore();
    }

    void InitRanking(){
        if (!PlayerPrefs.HasKey("BestScore")) PlayerPrefs.SetInt("BestScore", 0);
        if (!PlayerPrefs.HasKey("SecondScore")) PlayerPrefs.SetInt("SecondScore", 0);
        if (!PlayerPrefs.HasKey("ThirdScore")) PlayerPrefs.SetInt("ThirdScore", 0);
    }

    void SaveScore(){
        int best = PlayerPrefs.GetInt("BestScore");
        int second = PlayerPrefs.GetInt("SecondScore");
        int third = PlayerPrefs.GetInt("ThirdScore");

        if(score == best) return;
        if(score < best){
            if(score < second){
                if(score < third) return;
                PlayerPrefs.SetInt("ThirdScore", score);
                return;
            }
            PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
            PlayerPrefs.SetInt("SecondScore", score);
            return;
        }
        StartCoroutine(NewRecord());
        PlayerPrefs.SetInt("ThirdScore", PlayerPrefs.GetInt("SecondScore"));
        PlayerPrefs.SetInt("SecondScore", PlayerPrefs.GetInt("BestScore"));
        PlayerPrefs.SetInt("BestScore", score);
    }

    void LoadScore(){
        scoreList[0].text = PlayerPrefs.GetInt("BestScore").ToString();
        scoreList[1].text = PlayerPrefs.GetInt("SecondScore").ToString();
        scoreList[2].text = PlayerPrefs.GetInt("ThirdScore").ToString();
    }

    IEnumerator NewRecord(){
        while(true){
            newRecord.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            newRecord.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
