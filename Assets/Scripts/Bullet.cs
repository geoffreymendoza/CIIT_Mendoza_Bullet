using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject splatVfx;

    private void OnEnable() {
        Boundary.RegisterObject(this);
    }

    public void Return() {
        ObjectPoolManager.Instance.ReturnGameObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Enemy")) return;
        var handler = other.GetComponent<IHealthHandler>();
        var splat = ObjectPoolManager.Instance.GetObject(splatVfx);
        splat.transform.position = other.transform.position;
        handler.DoDamage(1);
        Return();
    }
}
