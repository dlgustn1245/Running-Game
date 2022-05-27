using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public float[] percentage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        return percentage.Length-1;
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            int idx = (int)GetRandomIdx();
            print(percentage[idx]);
        }
    }
}
