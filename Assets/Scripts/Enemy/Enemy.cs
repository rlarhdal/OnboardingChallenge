using System;
using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Hit,
    Die
}

public class Enemy : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private bool isMoving;
    private Vector2 GizmosPos;
    private Vector2 startPos = new Vector2(11, 0);
    private WaitForSeconds deathLoading = new WaitForSeconds(3f);

    public EnemyStatus status;

    private Animator animator;
    private Transform trans;
    private Health health;

    private void OnEnable()
    {
        EnemyInit();

        health.OnHealthChanged += TakeDamage;
        health.OnDeath += Die;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= TakeDamage;
        health.OnDeath -= Die;
    }

    public void Awake()
    {
        trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    //private void Start()
    //{
    //    EnemyInit();
    //}

    private void Update()
    {
        if (isMoving)
        {
            trans.Translate(Vector2.left * status.Speed * Time.deltaTime * 2f);
        }
    }

    void EnemyInit()
    {
        gameObject.SetActive(true);
        health.currenHealth = status.Health;
        isMoving = true;
    }

    private void TakeDamage(float amount)
    {
        animator.SetTrigger("TakeDamage");
    }

    private void Die()
    {
        // 죽는 애니메이션 끝나고 3초 후 다음 몬스터 스폰
        animator.SetBool("Death", true);
        Invoke("Deactivate", 3f);
    }

    private void Deactivate()
    {
        gameObject.transform.position = startPos;
        gameObject.SetActive(false);
        Managers.Game.enemyPool.Enqueue(Managers.Game.currentEnemy);
        Managers.Game.currentEnemy = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
        }
    }
}