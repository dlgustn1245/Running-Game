using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItemController : MonoBehaviour
{
    public enum CollectibleTypes { Item01, Item02, Obstacle };
    public CollectibleTypes types;
    public bool rotate;

    float rotateSpeed = 150.0f;

    void Start()
    {

    }


    void Update()
    {
        if (rotate)
        {
            this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        switch (types)
        {
            case CollectibleTypes.Item01:
                {
                    GameManager.Instance.PlayerScored(5);
                    break;
                }
            case CollectibleTypes.Item02:
                {
                    print("get improve");
                    break;
                }
            case CollectibleTypes.Obstacle:
                {
                    GameManager.Instance.PlayerDead();
                    break;
                }
        }
        Destroy(this.gameObject);
    }
}