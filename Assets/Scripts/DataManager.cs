using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager {
    static Player _currentPlayer;
    public static Player GetPlayer() => _currentPlayer;

    public static void RegisterPlayer(Player p) {
        _currentPlayer = p;
    }
}
