using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour {
    public ParticleSystem ps;
    public bool loop= false;

    // Start is called before the first frame update
    void OnEnable () {
        ps.Clear();
        ps.Play();
        if(loop)
            Invoke("Return", 2.5f);
    }

    void Return() {
        ObjectPoolManager.Instance.ReturnGameObject(this.gameObject);
    }
}
