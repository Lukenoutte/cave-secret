using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SaveManager.instance != null) { 
        if (SaveManager.instance.state.playedTuto)
        {
            StartCoroutine(GoToMenuDelay());
        }
        else
        {
            StartCoroutine(GoToAnimationDelay());
        }
        }
    }


    private IEnumerator GoToMenuDelay()
    {

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("MenuMain");
    }
    private IEnumerator GoToAnimationDelay()
    {

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Animation1");
    }
}
