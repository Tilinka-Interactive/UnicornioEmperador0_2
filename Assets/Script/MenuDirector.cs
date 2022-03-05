using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDirector : MonoBehaviour
{
    private string xPrefsName = "x";
    private string yPrefsName = "y";
    private string timePrefsName = "time";
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

}
