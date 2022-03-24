using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDirector : MonoBehaviour
{
    private string xPrefsName = "x";
    private string yPrefsName = "y";
    private string timePrefsName = "time";
    private int goldBet = 0;
    public Text goldBetText;
    private void Start()
    {
        //avoid creating a maze with size 0
        PlayerPrefs.SetInt(xPrefsName, 5);
        PlayerPrefs.SetInt(yPrefsName, 5);
    }
    public void setMazeSize(float x)
    {
        PlayerPrefs.SetInt(xPrefsName, Mathf.FloorToInt(x));
        PlayerPrefs.SetInt(yPrefsName, Mathf.FloorToInt(x));
        PlayerPrefs.SetInt(timePrefsName, (Mathf.FloorToInt(x)*4)+2);
    }

    public void setTime() 
    { 
    }

    public void setBet(string bet) {
        //PlayerPrefs.SetInt(bet);
    }

    public void plusBet(int gold) {
        if ((goldBet + gold) <= PlayerPrefs.GetInt("Gold", 0)){
            goldBet += gold;
            goldBetText.text = goldBet.ToString();
        }
        else Debug.Log("Insuficient gold");
    }

    public void clearBet() {
        goldBet = 0;
        goldBetText.text = "0";
    }

    public void setArcadeLevel() {
        //PlayerPrefs.SetInt(xPrefsName, Mathf.FloorToInt(x));
        //PlayerPrefs.SetInt(yPrefsName, Mathf.FloorToInt(x));
    }
}
