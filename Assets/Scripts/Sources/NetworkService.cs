using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;

// запрос в сеть
public class NetworkService : MonoBehaviour
{
    public IEnumerator GetTimeJson(string url, Action<string> errorCallback, Action<string> successCallback)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            errorCallback(uwr.error);
            yield break;
        }
        successCallback(uwr.downloadHandler.text);
    }
}

