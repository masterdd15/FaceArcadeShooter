using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    //public GameObject enemyPrefab;
    public GameObject[] listEnemyPrefab;

    [SerializeField] float enemyIntervals;

    [SerializeField] CursorLogic player;

    public bool isSpawning = false;
    [SerializeField] int randomValue;
    [SerializeField] int maxSpeed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CursorLogic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        Debug.Log(listEnemyPrefab.Length);
        randomValue = Random.Range(0, listEnemyPrefab.Length);
        StartCoroutine(spawnEnemy(randomValue, enemyIntervals, 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (player.curLives >= 0)
        {
            if (!isSpawning)
            {
                randomValue = Random.Range(0, listEnemyPrefab.Length);
                float newInterval = Random.Range(1, enemyIntervals);
                float newSpeed = Random.Range(1f, maxSpeed);
                StartCoroutine(spawnEnemy(randomValue, enemyIntervals, newSpeed));
            }
        }
    }

    private IEnumerator spawnEnemy(int randomIndex, float interval, float newSpeed)
    {
        isSpawning = true;
        yield return new WaitForSeconds(interval);
        Debug.Log(randomIndex);
        GameObject newEnemy = Instantiate(listEnemyPrefab[randomIndex], this.transform.position, Quaternion.identity);
        newEnemy.GetComponent<NavMeshAgent>().speed = newSpeed;
        Debug.Log(newSpeed);
        isSpawning = false;
    }
}
