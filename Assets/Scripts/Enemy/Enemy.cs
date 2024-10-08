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
        // 죽는 애니메이션 끝나고 3초 후 다음 몬스터 스폰
    }
}