using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtParticles : MonoBehaviour
{
    GameObject dirtPos;

    private void Start()
    {
        dirtPos = GameObject.Find("DirtSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = dirtPos.transform.position;
    }


}
