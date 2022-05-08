using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    
    private List<Transform> spawnedPrefabs = new List<Transform>();
    private Mesh containedMeshArea;
    private TerrainData containedTerrainArea;
    public GameObject spawnablePrefab;
    public int objectsToSpawn;
    public float minimumDistance = 2.5f;
    public float minimumHeight = -1f;
    public float maximumHeight = 0f;
    public float boundsOffset = 2f;

 
    // Plane Properties
    float x_dim;
    float y_dim;
 
    void Start () {
        if(transform.GetComponent<MeshFilter>()) 
        {
            containedMeshArea = transform.GetComponent<MeshFilter>().mesh;

            // Get the length and width of the plane
            x_dim = containedMeshArea.bounds.size.x - boundsOffset;
            y_dim = containedMeshArea.bounds.size.z - boundsOffset;
        } 
        else if(transform.GetComponent<Terrain>())
        {
            containedTerrainArea = transform.GetComponent<Terrain>().terrainData;

            // Get the length and width of the plane
            x_dim = containedTerrainArea.bounds.size.x;// - boundsOffset;
            y_dim = containedTerrainArea.bounds.size.z;// - boundsOffset;   
        }

        for(int i = 0; i <= objectsToSpawn; i++)
            Spawn();
    }
 
    private void Spawn() {
        Vector3 randpos = Vector3.zero;

        for(int i = 0; i < 50; i++)
        {
            randpos = Vector3.zero;
            randpos.x = Random.Range(-x_dim/2f, x_dim/2f);      
            randpos.y = Random.Range(minimumHeight, maximumHeight);    
            randpos.z = Random.Range(-y_dim/2f, y_dim/2f);

            if(!IsTooClose(randpos, minimumDistance, spawnedPrefabs)) 
            {
                Transform instance = Instantiate(spawnablePrefab, this.transform).transform;
                instance.localPosition = randpos;

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
