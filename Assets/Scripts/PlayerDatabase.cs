using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public Color color;
    public int id;
}

public class PlayerDatabase : MonoBehaviour {
    public static PlayerDatabase GetPlayerDatabase() {
        var gc = GameObject.FindGameObjectWithTag("PlayerDatabase");
        if (gc == null || gc.GetComponentInChildren<PlayerDatabase>() == null) {
            Debug.Break();
            print("PlayerDatabase not found!");
            return null;
        } else {
            return gc.GetComponentInChildren<PlayerDatabase>();
        }
    }

    public List<PlayerData> playerData = new List<PlayerData>();
	public PlayerData winner = null;
	public PlayerData loser = null;

	void Start() {
		DontDestroyOnLoad(this.gameObject);
	}
}
