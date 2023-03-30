using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private void Update()
    {
        navMeshAgent.SetDestination(new Vector3(0, 0, 0));
    }
}
