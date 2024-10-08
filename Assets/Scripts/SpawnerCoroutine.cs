using System.Collections;

using UnityEngine;

public class SpawnerCoroutine : Spawner
{
    private Coroutine buildRoutine;

    protected override void Start()
    {
        base.Start();
        buildRoutine = StartCoroutine(Build());
    }

    public override void StopAnySpawning()
    {
        StopCoroutine(buildRoutine);
    }

    private IEnumerator Build()
    {
        yield return BuildHouses();
        yield return BuildProps();
    }

    private IEnumerator BuildHouses()
    {
        for (int i = 0; i < numHouses; i++)
        {
            SpawnPrefab(housePrefabs, false);
            yield return new WaitForSeconds(WaitBetweenHouseSpawns());
        }
    }

    private IEnumerator BuildProps()
    {
        for (int i = 0; i < numProps; i++)
        {
            SpawnPrefab(propPrefabs, true);
            yield return new WaitForSeconds(WaitBetweenPropSpawns());
        }
    }
}