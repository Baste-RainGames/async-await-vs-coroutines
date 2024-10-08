using UnityEngine;

public class TestScript : MonoBehaviour
{
    public bool something;

    private async void Start()
    {
        // Four ways to wait a single frame. Three of them are broken.
        while (!something)
            await Awaitable.NextFrameAsync();
            // await Awaitable.NextFrameAsync(destroyCancellationToken); // exception logged on exit play mode!
            // await Task.Yield(destroyCancellationToken); // exception logged on exit play mode!
            // await Task.Yield(); // will keep running after exiting play mode
        Debug.Log(transform.position);
    }
}