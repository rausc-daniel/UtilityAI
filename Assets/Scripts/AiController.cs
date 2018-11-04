using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class AiController : Singleton<AiController>
{
    public static List<AiClient> clients;
    private Stopwatch sw;

    private float totalTime = 0;
    private float totalCycles = 0;
    [SerializeField] private float timePerCycle;

    protected override void Awake()
    {
        clients = new List<AiClient>();
        sw = new Stopwatch();
        CoroutineHelper.Instance.RunCoroutine(Tick(0.3f, UpdateClients), "AiTick");
    }

    public static void Register(AiClient client)
    {
        if (clients.Contains(client))
        {
            Debug.Log($"{client} already a Client");
            return;
        }

        clients.Add(client);
        Debug.Log(clients.Count);
    }

    private void UpdateClients()
    {
        foreach (AiClient aiClient in clients)
            aiClient.UpdateClient();
    }

    private IEnumerator Tick(float interval, OnTick onTick)
    {
        float progress = 0;

        while (true)
        {
            progress += Time.deltaTime;

            if (progress > interval)
            {
                onTick();
                progress = 0;
            }

            yield return null;
        }
    }
}