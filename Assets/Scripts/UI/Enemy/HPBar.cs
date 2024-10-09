using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;
    private Health health;

    private void OnEnable()
    {
        Init();

        health.OnHealthChanged += UpdateHpBar;
        health.OnDeath += Death;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateHpBar;
        health.OnDeath -= Death;
    }

    private void Awake()
    {
        health = GetComponent<Health>();
        hpSlider = GetComponentInChildren<Slider>();
    }

    private void Init()
    {
        hpSlider.maxValue = health.maxHealth;
        hpSlider.value = health.HP;
    }

    private void Death()
    {

    }

    private void UpdateHpBar(float currentHp)
    {
        hpSlider.value = currentHp;
    }
}
