using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        Vector3 playerPos = GameManager.Instance.playerInstance.transform.position;
        
        navMeshAgent.SetDestination(playerPos);
    }
}
