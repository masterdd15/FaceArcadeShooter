using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;

    [SerializeField] float enemyIntervals = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(enemyIntervals, enemyPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
