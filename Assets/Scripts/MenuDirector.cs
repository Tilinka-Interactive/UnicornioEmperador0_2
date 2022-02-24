using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDirector : MonoBehaviour
{
    private string xPrefsName = "x";
    private string yPrefsName = "y";
    //private string timePrefsName = "time";

    public void setMazeSize(float x)
    {
        PlayerPrefs.SetInt(xPrefsName, Mathf.FloorToInt(x));
        Debug.Log(PlayerPrefs.GetInt(xPrefsName, 0));
        PlayerPrefs.SetInt(yPrefsName, Mathf.FloorToInt(x));
    }

    public void setTime() 
    { 
    }

}
