using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        StartCoroutine(Managers.ImportCsv.LoadDatas(SheetType.Enemy));
        Invoke("SpawnEnemy", 1);
    }

    void SpawnEnemy()
    {
        if (Managers.Game.currentEnemy != null) return;

        Instantiate(Managers.ImportCsv.enemyObjects[Managers.Game.spawnCount]);
    }
}
