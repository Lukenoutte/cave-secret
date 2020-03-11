using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private GameObject bixinho = null;
    private GameObject mainMenu;
    private GameObject bixinho1Menu;
    private GameObject bixinho2Menu;
    private string animName;
    private bool animOn;
    public int idAnim;
    // Start is called before the first frame update
    void Start()
    {
        animOn = false;
        bixinho1Menu = GameObject.Find("/Canvas/MainMenu/Play/Bixinho1Menu");
        bixinho2Menu = GameObject.Find("/Canvas/MainMenu/Bixinho2Menu");
        mainMenu = GameObject.Find("/Canvas/MainMenu");
        idAnim = Random.Range(1, 3);

        if (idAnim == 1)
        {
            bixinho1Menu.SetActive(true);
            animOn = true;
        }

        if (idAnim == 2)
        {
            bixinho2Menu.SetActive(true);
            animOn = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null)
            {
                animName = hit.collider.gameObject.name.ToString();
                bixinho = GameObject.Find(animName);
                if (hit.collider.tag == "Bixinho")
                {
                    StartCoroutine(eraseAnim(bixinho));
                }
            }
        }

        if (mainMenu.active == false)
        {
            animOn = false;
            bixinho1Menu.SetActive(false);
            bixinho2Menu.SetActive(false);
        }

        if (animOn == false && mainMenu.active == true)
        {
            int oldId = idAnim;
            idAnim = Random.Range(1, 3);
            while (oldId == idAnim)
            {
                idAnim = Random.Range(1, 3);
            }


            if (idAnim == 1 && animOn == false && !bixinho2Menu.active)
            {
                animOn = true;
                bixinho1Menu.SetActive(true);
                


            }
            else if (idAnim == 2 && animOn == false && !bixinho1Menu.active)
            {
                animOn = true;
                bixinho2Menu.SetActive(true);
               
            }
        }
    }

    private IEnumerator eraseAnim(GameObject anim)
    {
        GameObject aux = anim;
        aux.GetComponent<Animator>().SetBool("clicked", true);


        yield return new WaitForSeconds(3);
        aux.SetActive(false);
        animOn = false;
               
    }



}
