using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if(Managers.Game.currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    public void Init()
    {
        // �� ���� �ε�
        MakeEnemyPool();
        SpawnEnemy();
    }

    private void MakeEnemyPool()
    {
        for (int i = 0; i < Managers.Game.spawnEnemy.Count; i++)
        {
            GameObject obj = Instantiate(Managers.Game.spawnEnemy[i]);
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(false);
            Managers.Game.enemyPool.Enqueue(obj);
        }
    }

    void SpawnEnemy()
    {
        if (Managers.Game.enemyPool.Count == 0) return;

        if (Managers.Game.currentEnemy == null)
        {
            Managers.Game.currentEnemy = Managers.Game.enemyPool.Dequeue();
            Managers.Game.currentEnemy.SetActive(true);
        }
    }

}
