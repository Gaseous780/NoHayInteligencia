using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StatePathFinding : MonoBehaviour
{
    [SerializeField] private Node goal;
    [SerializeField] private Node end;
    [SerializeField] private float radius;
    public LayerMask nodeLayer; 

    public void SetPath()
    {
        List<Node> path = BFS.Run(goal, IsSatisfied, GetConections);

        // A partir de aca implementación del profe

        List <Vector3> points = new List<Vector3>();

        for (int i = 0; i < points.Count; i++)
        {
            points.Add(path[i].transform.position);
        }

        //Aca iría un metodo del profe que hace que se ponga Crash en la posición inicial
    }
    public void ThetaStar()
    {
        Node inicio = GetClosestNode(_entity.transform.position);
        List<Node>path=ThetaStar.Run(inicio,IsSatisfied, GetConections,GetCosts,Heuristic,);
        List<Vector3> points = new List<Vector3>();
        for(int i=0; i < path.Count; i++)
        {
            points.Add(path[i].transform.position);
        }
        SetWaypoints(points);
    }
    public bool HasLineOfSight(Node node1,Node node2)
    {
        Vector3 startPos = node1.transform.position + Vector3.up*0.5F;
        Vector3 dirction = endpos - startPos;
        float distance = direction.magnitude;
        return!Physics.Raycast(startPos, dirction.normalized,distance);
    }

    public void SetPatDijkstrah()
    {
        List<Node> path = Dijkstra.Run(goal, IsSatisfied, GetConections, GetCosts );
        List<Vector3> points = new List<Vector3>();

        // A partir de aca implementación del profe

        for (int i = 0; i < points.Count; i++)
        {
            points.Add(path[i].transform.position);
        }

        //Aca iría un metodo del profe que hace que se ponga Crash en la posición inicial
    }

    Node GetClosestNode(Vector3 position)
    {
        Node closest = null;

        Collider[] nodos = Physics.OverlapSphere(position, radius, nodeLayer);

        float nearDistnce = Mathf.Infinity;


        for(int i = 0;i < nodos.Length;i++)
        {
            Node newNode = nodos[i].gameObject.GetComponent<Node>();
            if (newNode != null) { continue; }
            float distance = Vector3.Distance(position, nodos[i].transform.position);
            if (distance < nearDistnce)
            {
                Vector3 direction = nodos[i].transform.position-position;
                if (Physics.Raycast(position, direction.normalized,distance,LayerMask.GetMask("Wall")))continue;
                nearDistnce = Vector3.Distance(position, nodos[i].transform.position);
                closest = newNode;
            }
            
        }
        return closest;
    }

    public void SetPatASar()
    {
        //Node inicio=GetClosestNode(_entity)
        List<Node> path = AStar.Run(start, IsSatisfied, GetConections, GetCosts,Heuristic);
        List<Vector3> points = new List<Vector3>();

        // A partir de aca implementación del profe

        for (int i = 0; i < points.Count; i++)
        {
            points.Add(path[i].transform.position);
        }
        _move.SetPosition(StartCoroutine().transform.position);
        SetWayPoints(points);

        //Aca iría un metodo del profe que hace que se ponga Crash en la posición inicial
    }

    public bool IsSatisfied (Node node)
    {
        if (node == end)
        {
            return true;
        }

        return false;
    }

    public List <Node> GetConections(Node node) 
    {
        return node.neightbourds;
    }
    public float Heuristic(Node node)
    {
        float h = 0;
        h += Vector3.Distance(node.transform.position, goal.transform.position);
        return h;
    }

    public float GetCosts (Node node1, Node node2)
    {
        float costs = 0.0f;
        costs += Vector3.Distance(node1.transform.position, node2.transform.position);
        if (node2.hasTrap)
        {
            costs += 100;
        }

        return costs;
    }
}
