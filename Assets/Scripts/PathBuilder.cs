using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    [SerializeField] private Terrain _area;

    public List<Vector3> BuildPath(int numberOfWaypoints)
    {
        var path = new List<Vector3>();

        for (int i = 0; i < numberOfWaypoints; i++)
        {
            path.Add(GetRandomPointOnTerrain());
        }

        return path;
    }

    private Vector3 GetRandomPointOnTerrain()
    {
        float terrainWidth = _area.terrainData.size.x;
        float terrainLength = _area.terrainData.size.z;

        float x = Random.Range(0f, terrainWidth);
        float z = Random.Range(0f, terrainLength);
        float y = _area.SampleHeight(new Vector3(x, 0, z));

        return _area.transform.position + new Vector3(x, y, z);
    }
}
