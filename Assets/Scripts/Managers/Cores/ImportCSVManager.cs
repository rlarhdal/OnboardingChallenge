using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class ImportCSVManager
{
    public readonly string ADRESS = "https://docs.google.com/spreadsheets/d/1WnmxN1DUqdMZSj7dnF0Gr42EIKksjuS_34gKNypGwz4";
    public readonly string RANGE = "A2:D6";
    public readonly int SHEET_ID = 1901658409;

    public List<EnemyStatus> enemys;

    // key  > 스프레드시트 주제
    // value  > 스프레드시트 데이터
    private Dictionary<Type, string> sheetDatas = new Dictionary<Type, string>();
    private Type currentType;

    public void Init()
    {
        sheetDatas.Add(typeof(EnemyStatus), GetCSVAddress(ADRESS, RANGE, SHEET_ID));
    }

    public IEnumerator LoadData()
    {
        currentType = typeof(EnemyStatus);

        foreach (KeyValuePair<Type, string> kvp in sheetDatas)
        {
            if (kvp.Key == currentType)
            {
                UnityWebRequest www = UnityWebRequest.Get(sheetDatas[currentType]);
                yield return www.SendWebRequest();

                // 딕셔너리의 value 값 변경
                sheetDatas[currentType] = www.downloadHandler.text;

                if (currentType == typeof(EnemyStatus))
                {
                    enemys = GetDatasAsChildren<EnemyStatus>(sheetDatas[currentType]);

                    foreach (EnemyStatus enemy in enemys)
                    {
                        // 저장된 enemy 정보처리
                        // Resource의 오브젝트에 해당 정보 전달
                        //GameObject SpawnEnemy = Resources.Load<GameObject>($"Enemy/{enemy.name}");
                        //SpawnEnemy.name = enemy.name;
                        //SpawnEnemy.GetComponentInChildren<Enemy>().status = enemy;
                        Debug.Log(enemy.Name + " " + enemy.Grade + " " + enemy.Speed + " " + enemy.Health);
                    }
                }
                break;
            }
        }
    }

    public static string GetCSVAddress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }

    private List<T> GetDatasAsChildren<T>(string data)
    {
        List<T> dataList = new List<T>();

        // 셀의 행값을 배열에 저장(몬스터 각 정보를 배열에 저장)
        string[] splitedData = data.Split('\n');

        // 저장한 배열의 값에 접근
        foreach (string element in splitedData)
        {
            // 해당 객체의 정보를 배열에 저장
            string[] datas = element.Split('\t');
            dataList.Add(GetData<T>(datas, datas[0]));
        }
        return dataList;
    }

    private T GetData<T>(string[] datas, string typeName = "")
    {
        object data;

        // childType이 비어있거나 그런 Type이 없을 때
        if (string.IsNullOrEmpty(typeName) || Type.GetType(typeName) == null)
        {
            data = Activator.CreateInstance(typeof(T));
        }
        else
        {
            data = Activator.CreateInstance(Type.GetType(typeName));
        }

        // 클래스에 있는 변수들을 순서대로 저장한 배열 (순서대로 진행이 되므로 스프레드시트와 클래스의 클래스 순서가 같아야 함)
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            // 필드의 내용을 인스턴스 data에 저장
            try
            {
                // string > parse
                // EnemyStatus에 선언한 변수들의 순서대로 타입을 확인
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

                if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);

                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));

                else if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));

            }
            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message}");
            }
        }
        return (T)data;
    }
}
