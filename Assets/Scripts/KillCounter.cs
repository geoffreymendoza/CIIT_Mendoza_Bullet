using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class KillCounter : MonoBehaviour {
    public static event Action OnLevelUp;
    public int killedCount;
    public bool monsterLvlUp = false;
    public TextMeshProUGUI killCountTmp;

    // Start is called before the first frame update
    void Start() {
        Enemy.OnKilled += Enemy_OnKilled;
    }

    private void OnDestroy() {
        Enemy.OnKilled -= Enemy_OnKilled;
    }

    private void Enemy_OnKilled() {
        killedCount++;
        killCountTmp.text = $"Killed Count: {killedCount}";

        if(!monsterLvlUp && killedCount > 30) {
            monsterLvlUp = true;
            OnLevelUp?.Invoke();
        }
    }
}
