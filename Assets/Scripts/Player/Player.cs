using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    [Header("Overlap")]
    float radius = 2f;
    [SerializeField] LayerMask layerMask;
    Vector2 GizmosPos;
    Collider2D col;

    [Header("Player Status")]
    private int attackPower = 100;
    private float currentTime = 0;
    private readonly float delayTime = 1f;

    private WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(0.1f);

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GizmosPos = new Vector2 (transform.position.x, transform.position.y + .5f);
    }

    private void Update()
    {
        col = Physics2D.OverlapCircle(GizmosPos, radius, layerMask);

        if (col == null)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            Managers.Game.currentEnemy = col.gameObject;
            UpdateAttack();
        }
    }

    private void UpdateAttack()
    {
        if (Managers.Game.currentEnemy.GetComponent<Health>().currenHealth > 0)
        {
            animator.SetBool("Walking", false);

            currentTime += Time.deltaTime;

            if (currentTime > delayTime)
            {
                animator.SetTrigger("Attack");
                Invoke("Attack", .5f);
                currentTime = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GizmosPos, radius);
    }

    public void Attack()
    {
        Managers.Game.currentEnemy.GetComponent<Health>().TakeDamage(attackPower);
    }
}
