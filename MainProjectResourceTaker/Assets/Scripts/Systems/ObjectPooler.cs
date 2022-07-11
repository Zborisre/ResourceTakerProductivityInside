using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPooler instance;

    private List<GameObject> poolObjectsFuel = new List<GameObject>();
    private List<GameObject> poolObjectsMetall = new List<GameObject>();
    private List<GameObject> poolObjectsGlass = new List<GameObject>();
    public int ColOfPool = 20;

    [SerializeField] private GameObject FuelPre;
    [SerializeField] private GameObject MetallPre;
    [SerializeField] private GameObject GlassPre;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

        for (int i = 0; i < ColOfPool; i++)
        {
            GameObject obj = Instantiate(FuelPre);
            obj.SetActive(false);
            poolObjectsFuel.Add(obj);
        }
        for (int i = 0; i < ColOfPool; i++)
        {
            GameObject obj = Instantiate(MetallPre);
            obj.SetActive(false);
            poolObjectsMetall.Add(obj);
        }
        for (int i = 0; i < ColOfPool; i++)
        {
            GameObject obj = Instantiate(GlassPre);
            obj.SetActive(false);
            poolObjectsGlass.Add(obj);
        }
    }

    public GameObject SpawnFuel()
    {
        for (int i = 0; i < poolObjectsFuel.Count; i++)
        {
            if (!poolObjectsFuel[i].activeInHierarchy)
            {
                return poolObjectsFuel[i];
            }

        }
        return null;
    }
    public GameObject SpawnMetal()
    {
        for (int i = 0; i < poolObjectsMetall.Count; i++)
        {
            if (!poolObjectsMetall[i].activeInHierarchy)
            {
                return poolObjectsMetall[i];
            }

        }
        return null;
    }
    public GameObject SpawnGlass()
    {
        for (int i = 0; i < poolObjectsGlass.Count; i++)
        {
            if (!poolObjectsGlass[i].activeInHierarchy)
            {
                return poolObjectsGlass[i];
            }

        }
        return null;
    }

}