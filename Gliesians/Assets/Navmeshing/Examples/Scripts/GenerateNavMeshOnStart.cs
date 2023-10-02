using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateNavMeshOnStart : MonoBehaviour
{
    public bool activated;

    bool generated;

    private void Start()
    {
        gameObject.AddComponent<NavMeshSurface>();
        GetComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;
    }

    private void Update()
    {
        if (activated)
        {
            if (!generated)
            {
                GetComponent<NavMeshSurface>().BuildNavMesh();
                generated = true;
            }
        }
    }
}
