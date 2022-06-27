using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GameManager.Instance.chaseStart) navMeshAgent.SetDestination(player.position);

        if(Vector3.Distance(this.transform.position, player.position) < 1.5f && !GameManager.Instance.playerDead)
        {
            GameManager.Instance.PlayerDead();
        }
    }
}