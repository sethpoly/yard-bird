using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObjectSpawner : MonoBehaviour
{
    private List<Transform> spawnedPrefabs = new List<Transform>();
    private TerrainData containedTerrainArea;
    public GameObject spawnablePrefab;
    public int objectsToSpawn;
    public float minimumDistance = 2.5f;
    public float boundsOffset = 2f;

    private float xMinimum, xMaximum, zMinimum, zMaximum;
 
    void Start () {
        if(transform.GetComponent<Terrain>())
        {
            containedTerrainArea = transform.GetComponent<Terrain>().terrainData;
            xMinimum = transform.position.x;
            zMinimum = transform.position.z;
            xMaximum = xMinimum + containedTerrainArea.size.x;
            zMaximum = zMinimum + containedTerrainArea.size.z;
        }

        for(int i = 0; i <= objectsToSpawn; i++)
            Spawn();
    }
 
    private void Spawn() {
        Vector3 randpos = Vector3.zero;

        for(int i = 0; i < 50; i++)
        {
            randpos = Vector3.zero;
            randpos.x = Random.Range(xMinimum, xMaximum);      
            randpos.y = transform.position.y;    
            randpos.z = Random.Range(zMinimum, zMaximum);

            if(!IsTooClose(randpos, minimumDistance, spawnedPrefabs)) 
            {
                Transform instance = Instantiate(spawnablePrefab, randpos, Quaternion.identity).transform;
                instance.parent = this.transform;

                // Add to prefab list
                spawnedPrefabs.Add(instance);
                break;
            }
        }
    }

    // Get the next available position without crowding spawn area
    private bool IsTooClose(Vector3 pos, float minimumDistance, List<Transform> list)
    {
        if(list.Count == 0) { return false; }
        bool tooClose = false;

        foreach(var t in list)
        {
            float distance = Vector3.Distance(pos, t.localPosition);

            if(distance < minimumDistance) 
            {
                tooClose = true;
                break;
            }
        }
        return tooClose;
    }
}
