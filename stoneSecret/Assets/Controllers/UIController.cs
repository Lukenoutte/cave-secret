using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public void easyButton()
    {
        SceneManager.LoadScene("GameEasy");
    }
    public void mediumButton()
    {
        SceneManager.LoadScene("GameMedium");
    }
    public void hardButton()
    {
        SceneManager.LoadScene("GameHard");
    }
}
