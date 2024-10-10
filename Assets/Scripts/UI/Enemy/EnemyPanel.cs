using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text grade;
    [SerializeField] private Text speed;
    [SerializeField] private Text maxHP;

    public void SetInfo(GameObject obj)
    {
        nameText.text = obj.GetComponent<Enemy>().status.Name;
        grade.text = obj.GetComponent<Enemy>().status.Grade;
        speed.text = "스피드 : " + obj.GetComponent<Enemy>().status.Speed.ToString();
        maxHP.text = "최대체력 : " + obj.GetComponent<Enemy>().status.Health.ToString();
    }
}
