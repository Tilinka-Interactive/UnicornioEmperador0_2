using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public Text bigEyeIndicator;
    public GameObject Emperor;
    void Update()
    {
        if (Emperor != null)
        {
            Vector3 position = transform.position;
            position.x = Emperor.transform.position.x+0.5f;
            position.y = Emperor.transform.position.y+1;
            transform.position = position;
        }


    }

    public void bigEye()
    { 
        
  
    }

    public void ActivatePowerBigEye()
    {
        
        StartCoroutine(PlayCounter(10.0f));
    }

    public void StopPowerBigEye()
    {

    }

    public IEnumerator PlayCounter(float powerTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < powerTime)
        {
            elapsedTime += Time.deltaTime;
            bigEyeIndicator.text = Mathf.RoundToInt(powerTime - elapsedTime).ToString();
            yield return null;
        }
        StopPowerBigEye();
    }
}
