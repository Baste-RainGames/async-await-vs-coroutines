using System.Threading;

using UnityEngine;

public class SpawnerAsync : Spawner
{
    private CancellationTokenSource cancellationTokenSource;
    protected override async void Start()
    {
        base.Start();
        cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        var linked = CancellationTokenSource.CreateLinkedTokenSource(token, destroyCancellationToken);

        await Build(linked.Token);
    }

    public override void StopAnySpawning()
    {
        cancellationTokenSource.Cancel();
    }

    private async Awaitable Build(CancellationToken token)
    {
        await BuildHouses(token);
        await BuildProps(token);
    }

    private async Awaitable BuildHouses(CancellationToken token)
    {
        for (int i = 0; i < numHouses; i++)
        {
            SpawnPrefab(housePrefabs, false);
            await Awaitable.WaitForSecondsAsync(WaitBetweenHouseSpawns(), token);
        }
    }

    private async Awaitable BuildProps(CancellationToken token)
    {
        for (int i = 0; i < numProps; i++)
        {
            SpawnPrefab(propPrefabs, true);
            await Awaitable.WaitForSecondsAsync(WaitBetweenPropSpawns(), token);
        }
    }
}