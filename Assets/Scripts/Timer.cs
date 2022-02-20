using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text UITexto;
    private int contador = 0;
    public int tiempoRestante = 20;
    public GameMenu outGame;
    public GameObject MenuBack;
    public GameObject JoyStick;
    private void Awake()
    {
        InvokeRepeating("Cronometro", 0f, 1f);
        UITexto.text = Time.time.ToString();
    }
    
    private void Cronometro() 
    {
        contador++;
        if (tiempoRestante - contador >= 0)
        {
            UITexto.text = (tiempoRestante - contador).ToString();
        }
        else
        {
            MenuBack.SetActive(true);
            JoyStick.SetActive(false);
        } 
            
    }
}
