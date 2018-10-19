using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AiController : Singleton<AiController>
{
    public static List<AiClient> clients;

    protected override void Awake()
    {
        clients = new List<AiClient>();
        CoroutineHelper.Instance.RunCoroutine(Tick(0.3f, UpdateClients), "AiTick");
    }

    public static void Register(AiClient client)
    {
        if(clients.Contains(client)) return;

        clients.Add(client);
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
            Debug.Log(clients.Count);
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