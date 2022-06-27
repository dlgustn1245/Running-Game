using UnityEngine;
using UnityEngine.AI;

public delegate void FunctionPointer();

public class Spawn
{
    FunctionPointer spawnDirection;

    public Spawn(FunctionPointer functionPointer)
    {
        this.spawnDirection = functionPointer;
    }

    public void SpawnMap()
    {
        spawnDirection();
    }
}

public class MapGenerator : MonoBehaviour
{
    public GameObject ground;
    public GameObject currMap;
    public Transform straightPos;
    public Transform rightPos;
    public Transform leftPos;
    public float[] percentage;

    float GetRandomIdx()
    {
        float total = 0;
        foreach (float elem in percentage) total += elem;

        float randomPoint = Random.value * total;
        for (int i = 0; i < percentage.Length; i++)
        {
            if (randomPoint < percentage[i]) return i;
            else randomPoint -= percentage[i];
        }
        return percentage.Length - 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int idx = (int)GetRandomIdx();
            GenerateMap(idx);
        }
    }

    void SpawnStraight()
    {
        OffMeshLink offMeshLink = currMap.GetComponentInChildren<OffMeshLink>();
        currMap = Instantiate(ground, straightPos.position, straightPos.rotation) as GameObject;
        offMeshLink.endTransform = currMap.transform.Find("OffSetLink").Find("Start").transform;
    }

    void SpawnRight()
    {
        OffMeshLink offMeshLink = currMap.GetComponentInChildren<OffMeshLink>();
        currMap = Instantiate(ground, rightPos.position, rightPos.rotation) as GameObject;
        offMeshLink.endTransform = currMap.transform.Find("OffSetLink").Find("Right").transform;
    }

    void SpawnLeft()
    {
        OffMeshLink offMeshLink = currMap.GetComponentInChildren<OffMeshLink>();
        currMap = Instantiate(ground, leftPos.position, leftPos.rotation) as GameObject;
        offMeshLink.endTransform = currMap.transform.Find("OffSetLink").Find("Left").transform;
    }

    void GenerateMap(int idx)
    {
        FunctionPointer[] functions = new FunctionPointer[] { SpawnStraight, SpawnRight, SpawnLeft };

        Spawn spawn = new Spawn(functions[idx]);
        spawn.SpawnMap();
    }
}
