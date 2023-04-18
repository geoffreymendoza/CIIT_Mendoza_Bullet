using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealthHandler {
    public static event Action OnKilled;

    public MeshRenderer rend;
    public Material upgradeMat;
    public float moveSpeed = 4;
    Vector3 targetPos;

    public int initHp;
    public int hp { private set; get; }

    private void Start() {
        KillCounter.OnLevelUp += KillCounter_OnLevelUp;
    }

    private void KillCounter_OnLevelUp() {
        rend.material = upgradeMat;
        initHp++;
    }

    private void OnDestroy() {
        KillCounter.OnLevelUp -= KillCounter_OnLevelUp;
    }


    public void Init() {
        hp = initHp;
        targetPos = DataManager.GetPlayer().transform.position;
        StartCoroutine(Mobilize());
    }

    IEnumerator Mobilize() {
        Vector3 lastPosition = this.transform.position;
        float t = 0;
        while (t < 1) {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(lastPosition, targetPos, t);
            yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void DoDamage(int damage) {
        hp -= damage;
        Debug.Log("HIT" + hp + " left");                                                      
        if(hp <= 0) {
            Debug.Log("Died");
            OnKilled?.Invoke();
            Return();
        }
    }

    void Return() {
        ObjectPoolManager.Instance.ReturnGameObject(this.gameObject);
    }
}

public interface IHealthHandler {
    int hp { get; }
    void DoDamage(int damage);
}
