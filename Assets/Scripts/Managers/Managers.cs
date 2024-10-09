using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance = null;
    static Managers Instance { get { Init(); return instance; } }

    ImportCSVManager importCsv = new ImportCSVManager();
    GameManager game;

    public static ImportCSVManager ImportCsv { get { return Instance?.importCsv; } }
    public static GameManager Game { get { return Instance?.game; } }

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

            instance.game = go.AddComponent<GameManager>();

            instance.importCsv.Init();
        }
    }
}
