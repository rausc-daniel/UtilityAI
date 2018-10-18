using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPatrolPath", menuName = "Patrol Path")]
public class PatrolPath : ScriptableObject
{
    public List<Transform> Nodes;

    public Transform this[int x] => Nodes[x];

//    private void OnEnable()
//    {
//        for (var i = 0; i < Nodes.Count; i++)
//        {
//            GameObject go = new GameObject {name = $"PathNode{i}"};
//            go.transform.position = Nodes[i].position;
//        }
//    }
}