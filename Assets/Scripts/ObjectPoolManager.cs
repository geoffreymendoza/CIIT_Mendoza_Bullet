using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    private List<GameObject> parentObjs = new List<GameObject>();

    public static ObjectPoolManager Instance { private set; get; }

    private void Awake() {
        Instance = this;
    }

    private void OnDestroy() {
        Instance = null;
    }

    public GameObject GetObject(GameObject gameObj) {
        if (!objectPool.TryGetValue(gameObj.name, out Queue<GameObject> objectList))
            return CreateNewObject(gameObj);
        if (objectList.Count == 0)
            return CreateNewObject(gameObj);

        GameObject _object = objectList.Dequeue();
        _object.SetActive(true);
        return _object;
    }

    private GameObject CreateNewObject(GameObject gameObj) {
        GameObject newGO = Instantiate(gameObj);
        newGO.name = gameObj.name;
        GameObject parent = null;
        foreach (var go in parentObjs) {
            if (go.name != newGO.name)
                continue;
            parent = go;
            break;
        }
        if (parent == null) {
            parent = new GameObject(newGO.name);
            parentObjs.Add(parent);
        }
        newGO.transform.SetParent(parent.transform);
        return newGO;
    }

    public void ReturnGameObject(GameObject gameObj) {
        if (objectPool.TryGetValue(gameObj.name, out Queue<GameObject> objectList)) {
            objectList.Enqueue(gameObj);
        } else {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObj);
            objectPool.Add(gameObj.name, newObjectQueue);
        }
        gameObj.SetActive(false);
    }
}
