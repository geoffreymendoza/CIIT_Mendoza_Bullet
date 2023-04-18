using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject splatVfx;

    private void OnEnable() {
        //Invoke("Return", 2.5f);
    }

    void Return() {
        ObjectPoolManager.Instance.ReturnGameObject(this.gameObject);
    }

    IEnumerator DelayReturn(float duration) {
        yield return new WaitForSeconds(duration);
        ObjectPoolManager.Instance.ReturnGameObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wall"))
            Return();

        if (!other.CompareTag("Enemy")) return;
        var handler = other.GetComponent<IHealthHandler>();
        var splat = ObjectPoolManager.Instance.GetObject(splatVfx);
        splat.transform.position = other.transform.position;
        handler.DoDamage(1);
        Return();
    }
}
