using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
    }
}
