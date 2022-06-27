using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject[] items; //coin, star. trap, slowTrap
    public float[] percentage;
    public GameObject areaObject;

    BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = areaObject.GetComponent<BoxCollider>();
    }
    
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            int idx = (int)GetRandomIdx();
            GameObject item = Instantiate(items[idx], GetRandomPoint(), Quaternion.identity) as GameObject;
            Destroy(item, 10.0f);
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 originPos = areaObject.transform.parent.position;

        float range_x = boxCollider.bounds.size.x;
        float range_z = boxCollider.bounds.size.z;

        range_x = Random.Range((range_x / 2) * (-1), range_x / 2);
        range_z = Random.Range((range_z / 2) * (-1), range_z / 2);

        Vector3 randomPos = new Vector3((int)range_x, 0.3f, (int)range_z);
        Vector3 spawnPos = originPos + randomPos;

        return spawnPos;
    }

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
}