using UnityEditor;
using UnityEngine;

public class CheckSaveFileExample : MonoBehaviour
{
    public int id;
    public GameObject target;

    async void Start()
    {
        Debug.Log("Before wait");
        while (!SaveFile.loaded)
            await Awaitable.NextFrameAsync();

        Debug.Log("After wait");
        if (SaveFile.HasBeenPickedUp(id))
        {
            Debug.Log("Destroys");
            target.SetActive(false);
        }
    }
}

public static class SaveFile
{
    [RuntimeInitializeOnLoadMethod]
    public static void Unload() => loaded = false;

    [MenuItem("Fake/Load")]
    public static void FakeLoad() => loaded = true;

    public static bool loaded;
    public static bool HasBeenPickedUp(int id) => true;
}