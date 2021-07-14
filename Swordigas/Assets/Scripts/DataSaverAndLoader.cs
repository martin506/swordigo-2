using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaverAndLoader : MonoBehaviour
{
    public bool saveEnemyData = false;
    public bool loadEnemyData = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSaveEnemyData()
    {
        saveEnemyData = true;
        StartCoroutine("ResetSaveEnemyData");
    }

    public bool GetSaveEnemyData()
    {
        return saveEnemyData;
    }

    public void StartLoadEnemyData()
    {
        loadEnemyData = true;
        StartCoroutine("ResetLoadEnemyData");
    }

    public bool GetLoadEnemyData()
    {
        return loadEnemyData;
    }

    private IEnumerator ResetSaveEnemyData()
    {
        yield return new WaitForSeconds(0.1f);
        saveEnemyData = false;
    }

    private IEnumerator ResetLoadEnemyData()
    {
        yield return new WaitForSeconds(0.1f);
        loadEnemyData = false;
    }

}
