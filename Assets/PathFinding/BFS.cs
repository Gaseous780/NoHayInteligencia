using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BFS
{ 
    public static List <Node> Run(Node initialNode, Func <Node, bool> isSatified, Func <Node, List <Node>> getConections)
    {
        Queue<Node> pending = new Queue<Node>();
        HashSet<Node> visited = new HashSet<Node>();
        Dictionary <Node, Node> parents = new Dictionary<Node, Node>();
    
        pending.Enqueue(initialNode);
        visited.Add(initialNode);

        while (pending.Count > 0) 
        {
            Node node = pending.Dequeue();

            if (isSatified(node) == true) //Se encuentra el nodo que cumple el reqisito pedido
            {
                List<Node> path = new List<Node>();
                Node current = node;
                while (parents.ContainsKey(current) == true) //Mientras que encuentre el valor del pedido
                {
                    path.Add(current);
                    current = parents[current];
                }
               
                path.Reverse(); //Invierte todo el orden de la lista al revés. Si el nodo A era el primero ahora es el final, mientras que si F era el último, ahora es el primero
                return path;
            }
            else
            {
                List <Node> children = getConections(node);

                for (int i = 0; i < children.Count; i++)
                {
                    if (visited.Contains(children[i]) == true) continue; // "continue" sirve para saltar la iteración del for, si se tiene el visitado i se va a sumar y no va a pasar nada de lo que sigue en la función
                    pending.Enqueue(children[i]);
                    visited.Add(children[i]);
                    parents[children[i]] = node;
                }
            }
        
        }

        return new List<Node>(); //Si no encontro nada devuelve una lista vacía
    
    }


}
