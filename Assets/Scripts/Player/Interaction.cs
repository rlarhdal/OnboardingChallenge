using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Camera cam;
    float maxDistance = 15f;
    Vector3 mousePos;
    [SerializeField] EnemyPanel enemyPanel;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, maxDistance);
            Debug.DrawLine(mousePos, transform.forward * 10, Color.red, .3f);

            if (hit.transform.CompareTag("Enemy"))
            {
                if (enemyPanel.gameObject.activeSelf == false)
                {
                    enemyPanel.gameObject.SetActive(true);
                    enemyPanel.SetInfo(hit.transform.gameObject);
                }
                else
                {
                    enemyPanel.gameObject.SetActive(false);
                }
            }
        }
    }
}

