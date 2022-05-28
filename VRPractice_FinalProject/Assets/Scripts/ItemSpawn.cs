using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject star;
    public GameObject trap;
    public float[] percentage;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    float GetRandomIdx()
    {
        float total = 0;
        foreach (float elem in percentage) total += elem; //total = 100

        float randomPoint = Random.value * total; //0~100
        for (int i = 0; i < percentage.Length; i++)
        {
            if (randomPoint < percentage[i]) return i;
            else randomPoint -= percentage[i];
        }
        return percentage.Length - 1;
    }
}