using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Node> neighbors;

    public List<Node> _neighbors => neighbors;
}
