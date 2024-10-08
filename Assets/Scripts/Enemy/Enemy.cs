using UnityEngine;

public enum EnemyState
{
    Idle,
    Hit,
    Die
}

public class Enemy : MonoBehaviour
{
    public EnemyStatus status;

    public int maxHealth;

    public void Awake()
    {
        maxHealth = status.Health;
    }

    private void EnemyDie()
    {
        // �״� �ִϸ��̼� ������ 3�� �� ���� ���� ����
    }
}