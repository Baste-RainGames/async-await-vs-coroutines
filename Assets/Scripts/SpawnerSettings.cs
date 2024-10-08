using UnityEngine;

public class SpawnerSettings : MonoBehaviour
{
    public float distanceX;
    public float distanceZ;
    public int numHouses;
    public int numProps;
    public float minWaitBetweenHouses;
    public float maxWaitBetweenHouses;
    public float minWaitBetweenProps;
    public float maxWaitBetweenProps;
    public GameObject[] housePrefabs;
    public GameObject[] propPrefabs;
}