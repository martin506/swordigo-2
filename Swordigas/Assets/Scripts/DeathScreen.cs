using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;
    public bool timeToShowUp = false;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (timeToShowUp == true)
        {
            StartCoroutine("WaitAndShowDeathScreen");
            timeToShowUp = false;
        }
    }

    private IEnumerator WaitAndShowDeathScreen()
    {
        yield return new WaitForSeconds(1f);
        deathScreenUI.SetActive(true);
        Time.timeScale = 0.0000000000001f;
    }

    public void ItIsTimeToShowDeathScreen()
    {
        timeToShowUp = true;
    }

    public void StartPlayingAgain()
    {
        deathScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
