using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesSystem : MonoBehaviour
{

    public static int Fuel;
    public static int Metal;
    public static int Glass;

    public static int OgrFuel;
    public static int OgrMetal;
    public static int OgrGlass;

    public Text FuelTex;
    public Text MetalTex;
    public Text GlassTex;


    // Start is called before the first frame update
    void Start()
    {
        Fuel = 0;
        Metal = 0;
        Glass = 0;
        OgrFuel = 30;
        OgrMetal = 30;
        OgrGlass = 30;
    }

    // Update is called once per frame
    void Update()
    {
        FuelTex.text = Fuel.ToString() + "/" + OgrFuel.ToString();
        MetalTex.text = Metal.ToString() + "/" + OgrMetal.ToString();
        GlassTex.text = Glass.ToString() + "/" + OgrGlass.ToString();
    }
}
