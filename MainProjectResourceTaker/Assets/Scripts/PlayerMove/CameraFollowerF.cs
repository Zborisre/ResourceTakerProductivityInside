using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerF : MonoBehaviour
{
    public Transform target;
    public float smooth = 5.0f;
    public Vector3 offset = new Vector3(0, 2, -5);


    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10.0f;
    public GameObject Plane;

    public bool CameraDvij = true;

    private void Start()
    {
       
    }
    void Update()
    {
        if (CameraDvij)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Euler(55, 0, 0);
        }
        else
        {
            transform.position = new Vector3(15, 27, 14.5f);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    public void ButtonBuild()
    {
        CameraDvij = false;
        Plane.SetActive(true);
    }
    public void ButtonNotBuild()
    {
        CameraDvij = true;
        Plane.SetActive(false);
    }
}
