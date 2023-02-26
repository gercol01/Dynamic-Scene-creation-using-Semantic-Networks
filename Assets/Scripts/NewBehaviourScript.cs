using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject prefab;
    bool flag = false;

    void Start()
    {
        StartCoroutine(InstantiateGameObjectCoroutine());
        var a = 0;
    }

    void Update() {
        if (flag) {
            Debug.Log("Hello");
        }
    }

    private IEnumerator InstantiateGameObjectCoroutine()
    {
        GameObject obj = Instantiate(prefab);
        yield return new WaitForEndOfFrame();
        // The object is now fully instantiated
        Debug.Log("Object instantiated: " + obj.name);

        flag = true;
    }
}