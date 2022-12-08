using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //public GameObject enemyPrefab;
    public GameObject[] listEnemyPrefab;

    [SerializeField] float enemyIntervals;

    public bool isSpawning = false;
    [SerializeField] int randomValue;
    
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        Debug.Log(listEnemyPrefab.Length);
        randomValue = Random.Range(0, listEnemyPrefab.Length);
        StartCoroutine(spawnEnemy(randomValue, enemyIntervals));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            randomValue = Random.Range(0, listEnemyPrefab.Length);
            float newInterval = Random.Range(1, enemyIntervals);
            StartCoroutine(spawnEnemy(randomValue, enemyIntervals));
        }
    }

    private IEnumerator spawnEnemy(int randomIndex, float interval)
    {
        isSpawning = true;
        yield return new WaitForSeconds(interval);
        Debug.Log(randomIndex);
        GameObject newEnemy = Instantiate(listEnemyPrefab[randomIndex], this.transform.position, Quaternion.identity);
        isSpawning = false;
    }
}
