using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;

    [SerializeField]
    public int NeedFuel;
    [SerializeField]
    public int NeedMetal;
    [SerializeField]
    public int NeedGlass;

    public static int NeedFuelRes;
    public static int NeedMetalRes;
    public static int NeedGlassRes;
    public bool Hran;
    public int TypeOfHran;
    public bool ResourceDealer;
    public int TypeOfRes;
    public float CoolDownTime;
    private Collider Collider;
    public bool TimeSkiper;
    public static int WhatTimeSkip;

    public string Description;

    public static string DiscText;

    public GameObject Step1;
    public GameObject Step2;
    public GameObject Step3;

    private float cooldown;
    public float StroiCooldownInt;
    public GameObject Stroi;

    private int Mat;
    public static bool Itog;

    public Vector2Int Size = Vector2Int.one;


    private void Start()
    {
        Collider = GetComponent<BoxCollider>();
        Collider.enabled = false;
        Mat = 0;
        Itog = false;
        if(ResourceDealer)
        {
            Stroi.SetActive(false);
            cooldown = 10f;
        }
    }

    private void Update()
    {
        NeedFuelRes = NeedFuel;
        NeedMetalRes = NeedMetal;
        NeedGlassRes = NeedGlass;
        DiscText = Description;
        if (cooldown == 0 && ResourceDealer)
        {
            Stroi.SetActive(true);
            Stroi.transform.Rotate(0, 0.1f, 0);

        }
        //if (StroiCooldownInt == 0)
        //{
        //    Step3.SetActive(true);
        //    Destroy(Step2.gameObject);
        //}
    }

    public void SetTransparent(bool available)
    {
        if (ResourcesSystem.Fuel >= NeedFuel && ResourcesSystem.Metal >= NeedMetal && ResourcesSystem.Glass >= NeedGlass)
        {
            Itog = true;
        }

        if (available && Itog)
        {
            MainRenderer.material.color = Color.green;
        }
        else
        {
            MainRenderer.material.color= Color.red;
        }

    }

    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
        Mat = 0;

        Step3.SetActive(false);
        Step1.SetActive(true);

        if (Itog)
        {
            Collider.enabled = true;
            ResourcesSystem.Fuel -= Building.NeedFuelRes;
            ResourcesSystem.Metal -= Building.NeedMetalRes;
            ResourcesSystem.Glass -= Building.NeedGlassRes;
            Itog = false;
            StartCoroutine(StroiCooldown());
        }
        else
        {
            Destroy(this.gameObject);
            Itog = false;
        }
    }

    public void StarProcess()
    {
        if (Hran)
        {
            if (TypeOfHran == 1)
            {
                ResourcesSystem.OgrFuel += 10;
            }
            if (TypeOfHran == 2)
            {
                ResourcesSystem.OgrMetal += 10;
            }
            if (TypeOfHran == 3)
            {
                ResourcesSystem.OgrGlass += 10;
            }
        }
        if (ResourceDealer)
        {
            StartCoroutine(ShootCooldown());
        }
        if(TimeSkiper)
        {
            WhatTimeSkip = WhatTimeSkip + 2;
        }
    }

    IEnumerator ShootCooldown()
    {

        yield return new WaitForSeconds(cooldown);

        cooldown = 0;
        StopCoroutine(ShootCooldown());
    }

    IEnumerator StroiCooldown()
    {
        StroiCooldownInt = (StroiCooldownInt - WhatTimeSkip) / 2;
        yield return new WaitForSeconds(StroiCooldownInt);

        Step2.SetActive(true);
        Destroy(Step1.gameObject);

        yield return new WaitForSeconds(StroiCooldownInt);

        StroiCooldownInt = 0;

        Step3.SetActive(true);
        Destroy(Step2.gameObject);

        StarProcess();
        StopCoroutine(StroiCooldown());
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ship" && cooldown == 0 && ResourceDealer)
        {

            if (TypeOfRes == 1 && ResourcesSystem.OgrFuel > ResourcesSystem.Fuel)
            {
                ResourcesSystem.Fuel += 1;
                Stroi.SetActive(false);
                cooldown = CoolDownTime;
                StartCoroutine(ShootCooldown());
            }
            else if (TypeOfRes == 2 && ResourcesSystem.OgrMetal > ResourcesSystem.Metal)
            {
                ResourcesSystem.Metal += 1;
                Stroi.SetActive(false);
                cooldown = CoolDownTime;
                StartCoroutine(ShootCooldown());
            }
            else if (TypeOfRes == 3 && ResourcesSystem.OgrGlass > ResourcesSystem.Glass)
            {
                ResourcesSystem.Glass += 1;
                Stroi.SetActive(false);
                cooldown = CoolDownTime;
                StartCoroutine(ShootCooldown());
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        for(int x = 0; x < Size.x; x++)
        {
            for(int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color (1f, 0f, 0f, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 1f, 1));
            }
        }
    }
}
