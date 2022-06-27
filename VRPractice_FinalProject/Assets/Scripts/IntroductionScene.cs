using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionScene : MonoBehaviour
{
    public void ToTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
