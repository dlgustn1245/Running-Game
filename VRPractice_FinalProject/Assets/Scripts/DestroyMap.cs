using UnityEngine;

public class DestroyMap : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 15.0f);
    }
}
