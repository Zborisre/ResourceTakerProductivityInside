using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(30, 30);

    public Building[,] grid;
    private Building flyingBuilding;
    private Camera cam;

    public Text Information;

    public GameObject PanelTwo;
    public static bool oppon = false;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        cam = Camera.main;
    }

    public void StartPlacing(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
            PanelTwo.SetActive(false);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    public void Destroyer()
    {
        Destroy(flyingBuilding.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool avaliable = true;

                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x)
                    avaliable = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y)
                    avaliable = false;

                if(avaliable && IsPlaceTaken(x,y)) 
                    avaliable = false;

                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTransparent(avaliable);

                Information.text = (Building.DiscText + "\n Ресурсы нужные для производства: " + "\nТоплива - " + Building.NeedFuelRes.ToString() + "\nМеталла - " + Building.NeedMetalRes.ToString() + "\nСтекла - " + Building.NeedGlassRes.ToString());

                if (Input.GetMouseButtonDown(0) && avaliable)
                {
                    PlaceBuilding(x,y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if(grid[placeX + x, placeY + y] != null)
                    return true;
            }
        }

        return false;
    }

    private void PlaceBuilding(int placeX, int placeY)
    {
        for(int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }

        PanelTwo.SetActive(false);

        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 1f, 1));
            }
        }
    }
}
