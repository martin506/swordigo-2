using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] float durationToDeath;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, durationToDeath);
    }
}
