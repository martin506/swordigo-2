using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    string money;

    public Text goldText;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        money = player.GetMoney().ToString();

        goldText.text = money;
    }
}
