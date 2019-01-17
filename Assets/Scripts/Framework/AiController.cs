using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AiController : Singleton<AiController>
{
    public List<AiClient> clients;

    private void Awake()
    {
        foreach (var client in clients)
            client.InitializeClient();
        StartCoroutine(Tick(0.3f, UpdateClients));
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
                onTick();

            yield return null;
        }
    }
}