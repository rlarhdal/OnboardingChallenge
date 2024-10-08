using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Transform target;  //���� ���� �̾����� ���
    [SerializeField]
    private float scrollAmount;  //�̾����� �� ��� ������ �Ÿ�
    [SerializeField]
    private float moveSpeed;  //�̵� �ӵ�
    [SerializeField]
    private Vector3 moveDirection;  //�̵� ����

    void Update()
    {
        //����� moveDirection �������� moveSpeed�� �ӵ��� �̵�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //����� ������ ������ ����� ��ġ �缳��
        if (transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * (float)(scrollAmount * 4);
        }
    }
}
