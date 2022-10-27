using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequests {

    private class WebRequestsMonoBehaviour : MonoBehaviour { }

    private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

    private static void Init() {
        if (webRequestsMonoBehaviour == null) {
            GameObject gameObject = new GameObject("WebRequests");
            webRequestsMonoBehaviour = gameObject.AddComponent<WebRequestsMonoBehaviour>();
        }
    }

    public static void Get(string url, Action<string> onError, Action<string> onSuccess) {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess) {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url)) {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ProtocolError || unityWebRequest.result == UnityWebRequest.Result.ConnectionError) {
                onError(unityWebRequest.error);
            } else {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

}
