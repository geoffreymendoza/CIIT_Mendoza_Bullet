using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour{
    [Header("Boundary")]
    public Bounds rangeBounds;
    private Vector3 boundsCenter;

    private static List<Bullet> bullets;

    private void Awake() {
        bullets = new List<Bullet>();
    }

    public static void RegisterObject(Bullet bul) {
        bullets.Add(bul);
    }

    private static void UnregisterObject(Bullet bul) {
        bullets.Remove(bul);
    }
    
    private void Update() {
        GetCurrentBoundsPenalty();
    }

    void GetCurrentBoundsPenalty() {
        for (int i = bullets.Count - 1 ; i >= 0 ; i--) {
            var bul = bullets[i];
            Vector3 currentPos = bul.transform.position;
            if (rangeBounds.Contains(currentPos))
                continue;
            bul.Return();
            UnregisterObject(bul);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(rangeBounds.center, rangeBounds.size);
    }
}