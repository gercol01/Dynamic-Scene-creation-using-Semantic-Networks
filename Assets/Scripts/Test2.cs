using System.Collections;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        StartCoroutine(DoStuffCoroutine());
    }

    private IEnumerator InstantiateGameObjectCoroutine()
    {
        GameObject obj = Instantiate(prefab);
        yield return new WaitForEndOfFrame();
        // The object is now fully instantiated
        Debug.Log("Object instantiated: " + obj.name);
    }

    private IEnumerator DoStuffCoroutine()
    {
        yield return StartCoroutine(InstantiateGameObjectCoroutine());
        // The InstantiateGameObjectCoroutine coroutine has finished
        Debug.Log("DoStuffCoroutine continuing...");

        Debug.Log("Check if destroyed");
    }
}