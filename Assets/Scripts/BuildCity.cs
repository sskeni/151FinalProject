using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCity : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject pickupPrefab;
    public int mapWidth = 20;
    public int mapHeight = 20;
    public int buildingFootprint = 30;
    public float inputSeed = 0;

    private void Start()
    {
        float seed;
        if (inputSeed != 0)
            seed = inputSeed;
        else
            seed = UnityEngine.Random.Range(0, 100);
        for (int h = 0; h < mapHeight; h++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                int result = (int)(Mathf.PerlinNoise(w / 10f + seed, h / 10f + seed) * 10);
                Vector3 pos = new Vector3(w * buildingFootprint, 0, h * buildingFootprint);

                GameObject tile;
                if (result < 2)
                {
                    tile = Instantiate(pickupPrefab, pos, Quaternion.identity);
                    tile.transform.localScale = new Vector3(pickupPrefab.transform.localScale.x, pickupPrefab.transform.localScale.y, pickupPrefab.transform.localScale.z);
                    tile.transform.position = new Vector3(tile.transform.position.x, UnityEngine.Random.Range(10, 500), tile.transform.position.z);
                }
                else if (result < 4)
                {
                    tile = Instantiate(buildings[0], pos, Quaternion.identity);
                    tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y + 20 * 2, tile.transform.localScale.z);
                    tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 20, tile.transform.position.z);
                    tile.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), 1, UnityEngine.Random.Range(0f, 1f));
                }
                else if (result < 6)
                {
                    tile = Instantiate(buildings[0], pos, Quaternion.identity);
                    tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y + 40 * 2, tile.transform.localScale.z);
                    tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 40, tile.transform.position.z);
                    tile.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1);
                }
                else if (result < 8)
                {
                    tile = Instantiate(buildings[0], pos, Quaternion.identity);
                    tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y + 80 * 2, tile.transform.localScale.z);
                    tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 80, tile.transform.position.z);
                    tile.GetComponent<Renderer>().material.color = new Color(1, 1, UnityEngine.Random.Range(0f, 1f));
                }
                else if (result < 9)
                {
                    tile = Instantiate(buildings[0], pos, Quaternion.identity);
                    tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y + 160 * 2, tile.transform.localScale.z);
                    tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 160, tile.transform.position.z);
                    tile.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), 1, 1);
                }
                else if (result < 10)
                {
                    tile = Instantiate(buildings[0], pos, Quaternion.identity);
                    tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y + 320 * 2, tile.transform.localScale.z);
                    tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 320, tile.transform.position.z);
                    tile.GetComponent<Renderer>().material.color = new Color(1, UnityEngine.Random.Range(0f, 1f), 1);
                }

            }
        }
    }
}
