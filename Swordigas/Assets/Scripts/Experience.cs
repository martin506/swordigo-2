using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    string xp;

    public Text xpText;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        xp = player.GetExperience().ToString();

        xpText.text = xp;
    }
}
