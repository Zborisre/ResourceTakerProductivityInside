using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawn : MonoBehaviour
{
    private int Ranges;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(0, 28), 0, Random.Range(0, 28));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.1f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);


        Ranges = Random.Range(1,4);


        if (Ranges == 1)
        {
            GameObject bullet = ObjectPooler.instance.SpawnFuel();
            if (bullet != null)
            {
                bullet.transform.position = new Vector3(Random.Range(0, 28), 0, Random.Range(0, 28));
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
        }
        if (Ranges == 2)
        {
            GameObject bullet = ObjectPooler.instance.SpawnMetal();
            if (bullet != null)
            {
                bullet.transform.position = new Vector3(Random.Range(0, 28), 0, Random.Range(0, 28));
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
        }
        if (Ranges == 3)
        {
            GameObject bullet = ObjectPooler.instance.SpawnGlass();
            if (bullet != null)
            {
                bullet.transform.position = new Vector3(Random.Range(0, 28), 0, Random.Range(0, 28));
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
        }

    }
}
