using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [HideInInspector] public GameObject currentEnemy = null;

    [HideInInspector] public Queue<GameObject> enemyPool = new Queue<GameObject>();
    [HideInInspector] public List<GameObject> spawnEnemy = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Managers.ImportCsv.LoadDatas(SheetType.Enemy));
        Invoke("MakeEnemySpawner", 1f);
    }

    private void MakeEnemySpawner()
    {
        GameObject spawner = (GameObject)Resources.Load("EnemySpawn");
        Instantiate(spawner);
    }
}
