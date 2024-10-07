using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Die
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyStatus status;

    public Transform target;
    public int maxHealth;

    protected virtual void Awake()
    {
        maxHealth = status.Health;
    }

    public void OnEnable()
    {
        status.Health = maxHealth;
    }

    public virtual void Init(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void EnemyDie()
    {
        // �״� �ִϸ��̼� ������ 3�� �� ���� ���� ����
    }
}