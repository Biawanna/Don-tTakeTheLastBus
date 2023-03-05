using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private GameObject boxTrigger;

    [SerializeField] private float minAttackDamage;
    [SerializeField] private float maxAttackDamage;

    [SerializeField] private Transform Player;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    //private void Start()
    //{
    //    Player = GameObject.FindWithTag("Player").transform;

    //}

    private void Update()
    {
        if (Player != null)
        {
            if (_waiting)
            {
                navMeshAgent.velocity = Vector3.zero;
                animator.SetBool("Run", false);
                _waitCounter += Time.deltaTime;
                if (_waitCounter < _waitTime)
                    return;
                _waiting = false;
            }

            Transform wp = Player;
            if (Vector3.Distance(transform.position, wp.position) < 1.6f)
            {
                animator.SetTrigger("Attack");
                GameManager.instance.AttackPlayer(minAttackDamage, maxAttackDamage);
                _waitCounter = 0f;
                _waiting = true;
            }
            else
            {
                navMeshAgent.destination = wp.position;
                animator.SetBool("Run", true);
            }
        }
        else
        {
            navMeshAgent.speed = 0;
        }
    }
}

