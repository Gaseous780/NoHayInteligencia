using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

public class EnemyControllerLs : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Rigidbody playerRb;
    [SerializeField] LineOfSight los;
    [SerializeField] List<Transform> patrolPoints;
    [SerializeField] float speed = 3f;
    [SerializeField] float rotationSpeed=5f;
    [SerializeField] float arriveRadius = 5f;
    [SerializeField] float preductionTime=0.5f;
    [SerializeField] float evadeDistance=3f;
    [SerializeField] float patrolPointReachDistance = 0.5f;
    [SerializeField] int currentPatrolIndex = 0;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        los = GetComponent<LineOfSight>();
        rb = player.GetComponent<Rigidbody>();
    }
    public void Start()
    {
        
    }

    private void Update()
    {
        bool canSeePlayer = los.IsRange(transform,player)&& los.IsAngle(transform,player)&&los.IsObstacle(transform,player);
        bool isClose = Vector3.Distance(transform.position, player.position) <= evadeDistance;
        List<ActionOption> actionOptions= BuilAction(canSeePlayer, isClose);
        ActionOption bestAction= SelectBestAction(actionOptions);
        bestAction.action?.Invoke();

    }

    private ActionOption SelectBestAction(List<ActionOption> actions)
    {
        ActionOption best = null;
        float bestScore = float.MinValue;
        foreach (var action in actions)
        {
            if (action.score > bestScore)
            {
                bestScore = action.score;
                best = action;
            }
        }
        return best;
    }


    private List<ActionOption> BuilAction(bool canSeePlayer, bool isClose)
    {
        List<ActionOption> actions = new List<ActionOption>();
        actions.Add(new ActionOption("Patrol", canSeePlayer ? 5f : 30f, Patrol));
        actions.Add(new ActionOption("Pursue", canSeePlayer ? 80f : 0f, Pursue));
        actions.Add(new ActionOption("Evade", canSeePlayer ? 100f : 0f, Evade));
        return actions;
    }
    private void Pursue()
    {
        Vector3 dir = SteeringBehaviours.Pursue(transform, player, playerRb, preductionTime);
        Move(dir);
    }
    private void Evade()
    {
        Vector3 dir = SteeringBehaviours.Evade(transform, player, playerRb, preductionTime);
        Move(dir);
    }
    private void Patrol()
    {
       if(patrolPoints==null|| patrolPoints.Count==0)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        Transform target = patrolPoints[currentPatrolIndex];
        Vector3 dir = target.position- target.position;
        dir.y = 0f;
        if(dir.magnitude<patrolPointReachDistance)
        {
            rb.linearVelocity = Vector3.zero;
            currentPatrolIndex++;
            if(currentPatrolIndex>=patrolPoints.Count)
            {
                Shuffle (patrolPoints);
                currentPatrolIndex = 0;
            }
            return;
        }
        Move(dir.normalized);
    }

    private void Move(Vector3 dir)
    {
        Vector3 velocity = dir * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity= velocity;
        if(dir!=Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotationSpeed);
        }
    }

    private void Shuffle(List<Transform>list)
    {
        for (int i = list.Count - 1; i > 0;i--)
        {
            int j= UnityEngine.Random.Range(0, i+1);
            Transform temp= list[i];
            list[i]=list[j];
            list[j]=temp;
        }
    }
}
