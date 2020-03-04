using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{

    private bool shaking;
    private float shakeVar;
    // Start is called before the first frame update
    void Start()
    {
        shakeVar = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            float step = 2 * Time.deltaTime;
            Vector3 newPos = Random.insideUnitSphere * (step * shakeVar);
            
            newPos.z = GetComponent<Transform>().position.z;
            GetComponent<Transform>().position = newPos;
        }
    }

    public void ShakeMe()
    {
        StartCoroutine(ShakeNow());

    }
    private IEnumerator ShakeNow()
    {

        Vector3 originalPos = GetComponent<Transform>().position;
        if (shaking == false)
        {
            shaking = true;
        }
        yield return new WaitForSeconds(0.5f);

        shaking = false;
        GetComponent<Transform>().position = originalPos;
    }
}
