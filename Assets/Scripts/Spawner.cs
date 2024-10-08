using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using Random = UnityEngine.Random;

public abstract class Spawner : MonoBehaviour
{
    public SpawnerSettings settings;

    protected float        distanceX     => settings.distanceX;
    protected float        distanceZ     => settings.distanceZ;
    protected int          numHouses     => settings.numHouses;
    protected int          numProps      => settings.numProps;
    protected GameObject[] housePrefabs  => settings.housePrefabs;
    protected GameObject[] propPrefabs   => settings.propPrefabs;
    protected float minWaitBetweenHouses => settings.minWaitBetweenHouses;
    protected float maxWaitBetweenHouses => settings.maxWaitBetweenHouses;
    protected float minWaitBetweenProps  => settings.minWaitBetweenProps;
    protected float maxWaitBetweenProps  => settings.maxWaitBetweenProps;

    private List<Vector3> freeSpots;

    protected virtual void Start()
    {
        var requiredSpots = numHouses + numProps;
        var numSpotsOnAxis = Mathf.CeilToInt(Mathf.Sqrt(requiredSpots) * 1.4f);

        freeSpots = new ();
        for (int x = 0; x < numSpotsOnAxis; x++)
        for (int z = 0; z < numSpotsOnAxis; z++)
            freeSpots.Add(new (x * distanceX, 0f, z * distanceZ));

        Shuffle(freeSpots);
    }

    protected void SpawnPrefab(GameObject[] prefabList, bool rotate)
    {
        var rotation = rotate ? Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) : Quaternion.identity;
        Instantiate(prefabList[Random.Range(0, prefabList.Length)], GetFreeSpot(), rotation);
    }

    protected float WaitBetweenHouseSpawns()
    {
        return Random.Range(minWaitBetweenHouses, maxWaitBetweenHouses);
    }

    protected float WaitBetweenPropSpawns()
    {
        return Random.Range(minWaitBetweenProps, maxWaitBetweenProps);
    }

    private Vector3 GetFreeSpot()
    {
        if (freeSpots.Count == 0)
        {
            Debug.LogWarning("No free spots left");
            return default;
        }

        var spot = freeSpots[^1];
        freeSpots.RemoveAt(freeSpots.Count - 1);
        return spot;
    }

    public static void Shuffle<T>(IList<T> list) {
        if (list is not { Count: > 1 })
            return;

        //Fisher-Yates
        for (int i = list.Count - 1; i >= 1; i--) {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StopAnySpawning();
        }
    }

    public abstract void StopAnySpawning();
}