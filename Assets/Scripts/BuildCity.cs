using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCity : MonoBehaviour
{
    public GameObject[] buildings;
    public int mapWidth = 20;
    public int mapHeight = 20;
    public int buildingFootprint = 30;

    private void Start()
    {
        float seed = UnityEngine.Random.Range(0, 100);
        for (int h = 0; h < mapHeight; h++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                int result = (int)(Mathf.PerlinNoise(w/10f + seed, h/10f + seed) * 10);
                Vector3 pos = new Vector3(w * buildingFootprint, 0, h * buildingFootprint);

                GameObject building = Instantiate(buildings[0], pos, Quaternion.identity);
                
                if (result < 2)
                {
                    building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y + 10 * 2, building.transform.localScale.z);
                    building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + 10, building.transform.position.z);
                    building.GetComponent<Renderer>().material.color = new Color(1, UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                }
                else if (result < 4)
                {
                    building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y + 20 * 2, building.transform.localScale.z);
                    building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + 20, building.transform.position.z);
                    building.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), 1, UnityEngine.Random.Range(0f, 1f));
                }
                else if (result < 6)
                {
                    building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y + 40 * 2, building.transform.localScale.z);
                    building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + 40, building.transform.position.z);
                    building.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1);
                }
                else if (result < 8)
                {
                    building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y + 80 * 2, building.transform.localScale.z);
                    building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + 80, building.transform.position.z);
                    building.GetComponent<Renderer>().material.color = new Color(1, 1, UnityEngine.Random.Range(0f, 1f));
                }
                else if (result < 9)
                {
                    building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y + 160 * 2, building.transform.localScale.z);
                    building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + 160, building.transform.position.z);
                    building.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), 1, 1);
                }
                else if (result < 10)
                {
                    building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y + 320 * 2, building.transform.localScale.z);
                    building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + 320, building.transform.position.z);
                    building.GetComponent<Renderer>().material.color = new Color(1, UnityEngine.Random.Range(0f, 1f), 1);
                }

            }
        }
    }
}
