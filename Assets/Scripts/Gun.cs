using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public float timeBetweenShot = 0.5f;
    float shotTime;

    public Transform BulletSpawnpoint;
    public GameObject BulletPrefab;
    public float BulletSpeed;
    public GameObject SpawnFX;
    // Start is called before the first frame update
    void Start() {
        shotTime = Time.time + timeBetweenShot;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time < shotTime) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //Instantiate(SpawnFX, BulletSpawnpoint.position, BulletSpawnpoint.rotation);
            var vfx = ObjectPoolManager.Instance.GetObject(SpawnFX);
            vfx.transform.SetPositionAndRotation(BulletSpawnpoint.position, BulletSpawnpoint.rotation);

            var bullet = ObjectPoolManager.Instance.GetObject(BulletPrefab);
            bullet.transform.SetPositionAndRotation(BulletSpawnpoint.position, BulletSpawnpoint.rotation);
            //var bullet = Instantiate(BulletPrefab, BulletSpawnpoint.position, BulletSpawnpoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnpoint.forward * BulletSpeed;

            shotTime = Time.time + timeBetweenShot;
        }
    }
}
