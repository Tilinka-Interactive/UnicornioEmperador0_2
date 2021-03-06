using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text UITexto;
    [SerializeField] int seg;
    private float restante;
    private bool enMarcha;
    public GameMenu outGame;
    public GameObject MenuBack;
    public GameObject JoyStick;
    public PlayerMovement emperor;

    public void StartCrono()
    {
        restante = (seg);
        enMarcha = true;
    }

    private void Update()
    {
        if (enMarcha) 
        {
            restante -= Time.deltaTime;
            if (restante < 1)
            {
                enMarcha = false;
                MenuBack.SetActive(true);
                JoyStick.SetActive(false);
                emperor.TimesOut();
            }
            int tempTime = Mathf.FloorToInt(restante);
            UITexto.text = tempTime.ToString();
        }
    }

    public void StopCrono() 
    {
        enMarcha = false;
    }
}
