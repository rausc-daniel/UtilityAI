using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper Instance;
    private Dictionary<string, CoroutineNode> activeCoroutines;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
            
        activeCoroutines = new Dictionary<string, CoroutineNode>();
    }

    public void RunCoroutineOnce(IEnumerator coroutine)
    {
        CoroutineNode tmp = CreateInstance(coroutine);
        StartCR(tmp);
    }

    public void RunCoroutine(IEnumerator coroutine, string name)
    {
        CoroutineNode tmp = CreateInstance(coroutine, name);
        StartCR(tmp);
    }

    public void EndCoroutine(string name)
    {
        if (CheckIfCoroutineExists(name))
        {
            CoroutineNode tmp = GetEntry(name);
            DeleteInstance(tmp, name);
        }
        else return;
    }

    private CoroutineNode CreateInstance(IEnumerator coroutine, string name)
    {
        CoroutineNode node = new CoroutineNode(coroutine);
        activeCoroutines.Add(name, node);
        return node;
    }

    private CoroutineNode CreateInstance(IEnumerator coroutine)
    {
        CoroutineNode node = new CoroutineNode(coroutine);
        return node;
    }
    
    private void DeleteInstance(CoroutineNode node, string name)
    {
        activeCoroutines.Remove(name);
        StopCR(node);
    }

    private CoroutineNode GetEntry(string name)
    {
        CoroutineNode tmp;
        activeCoroutines.TryGetValue(name, out tmp);
        return tmp;
    }

    private bool CheckIfCoroutineExists(string name)
    {
        CoroutineNode tmp;
        return activeCoroutines.TryGetValue(name, out tmp);
    }

    private void StartCR(CoroutineNode node)
    {
        StartCoroutine(node.myCoroutine);
    }

    private void StopCR(CoroutineNode node)
    {
        StopCoroutine(node.myCoroutine);
    }

    public void DebugList()
    {
        foreach (KeyValuePair<string, CoroutineNode> item in activeCoroutines)
            Debug.LogFormat("{0}", item.ToString());
    }
}
