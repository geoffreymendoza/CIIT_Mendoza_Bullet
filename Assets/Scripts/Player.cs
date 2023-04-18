using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    void Awake() {
        DataManager.RegisterPlayer(this);
    }
}
