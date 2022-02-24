using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDirector : MonoBehaviour
{
    private string xPrefsName = "x";
    private string yPrefsName = "y";
    private string timePrefsName = "time";

    public void setMazeSize(float x)
    {
        PlayerPrefs.SetInt(xPrefsName, Mathf.FloorToInt(x));
        PlayerPrefs.SetInt(yPrefsName, Mathf.FloorToInt(x));
        PlayerPrefs.SetInt(timePrefsName, (Mathf.FloorToInt(x)*4)+2);
    }

    public void setTime() 
    { 
    }

}
