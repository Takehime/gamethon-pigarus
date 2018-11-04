using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    public static ItemSpawner GetItemSpawner() {
        var gc = GameObject.FindGameObjectWithTag("ItemSpawner");
        if (gc == null || gc.GetComponentInChildren<ItemSpawner>() == null) {
            Debug.Break();
            print("ItemSpawner not found!");
            return null;
        } else {
            return gc.GetComponentInChildren<ItemSpawner>();
        }
    }

	public GameObject wingPrefab;
	public Transform wingStartPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnWing(Vector3 position) {
        var obj = Instantiate(wingPrefab);
        obj.transform.position = position;
	}
}
