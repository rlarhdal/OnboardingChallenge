using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers Instance { get { Init(); return instance; } }

    ImportCSVManager importCsv = new ImportCSVManager();

    public static ImportCSVManager ImportCsv { get { return Instance?.importCsv; } }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            // csv������ import
            instance.importCsv.Init();
        }
    }


}
