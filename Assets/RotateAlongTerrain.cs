using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAlongTerrain : MonoBehaviour
{
    [SerializeField] private Transform _modelTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AlignTransform(_modelTransform);
    }

    public static void AlignTransform(Transform transform)
    {
        Vector3 sample = SampleNormal(transform.position);

        Vector3 proj = transform.forward - (Vector3.Dot(transform.forward, sample)) * sample;
        transform.rotation = Quaternion.LookRotation(proj, sample);
    }
    public static Vector3 SampleNormal(Vector3 position)
    {
        Terrain terrain = Terrain.activeTerrain;
        var terrainLocalPos = position - terrain.transform.position;
        var normalizedPos = new Vector2(
            Mathf.InverseLerp(0f, terrain.terrainData.size.x, terrainLocalPos.x),
            Mathf.InverseLerp(0f, terrain.terrainData.size.z, terrainLocalPos.z)
        );
        var terrainNormal = terrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);
        Debug.Log($"terrainNormal: {terrainNormal}");
        return terrainNormal;
    }
}
