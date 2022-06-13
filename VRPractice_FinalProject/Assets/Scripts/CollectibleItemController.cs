using UnityEngine;

public class CollectibleItemController : MonoBehaviour
{
    public enum CollectibleTypes { Item01, Item02, Obstacle01, Obstacle02 };
    public CollectibleTypes types;
    public bool rotate;

    float rotateSpeed = 150.0f;

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
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject other)
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
                    print("improve speed");
                    break;
                }
            case CollectibleTypes.Obstacle01:
                {
                    GameManager.Instance.PlayerDead();
                    break;
                }
            case CollectibleTypes.Obstacle02:
                {
                    other.GetComponent<PlayerController>().SlowMoveSpeed();
                    break;
                }
        }
        Destroy(this.gameObject);
    }
}