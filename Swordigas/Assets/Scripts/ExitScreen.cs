using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScreen : MonoBehaviour
{
    public GameObject exitScreen;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isOpen)
            {
                exitScreen.SetActive(false);
                isOpen = false;
                Time.timeScale = 1;
            }
            else
            {
                exitScreen.SetActive(true);
                isOpen = true;
                Time.timeScale = 0;
            }
        }
    }
}
