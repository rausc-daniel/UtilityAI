using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineNode
{
    internal IEnumerator myCoroutine;

    public CoroutineNode(IEnumerator myCoroutine)
    {
        this.myCoroutine = myCoroutine;
    }
}
