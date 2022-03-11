using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    public Text currentGold;
    private void Start()
    {

        currentGold.text = PlayerPrefs.GetInt("Gold",0).ToString();
    }
    public void buyGold() {
        int gold = PlayerPrefs.GetInt("Gold", 0) + 50;
        PlayerPrefs.SetInt("Gold", gold);
        currentGold.text = gold.ToString();
    }
}
