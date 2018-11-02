using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsgen : MonoBehaviour {
	// System.Random rand;

	[SerializeField] GameObject baseBubble;

	enum Obstacles {
		Bubble = 666,
		None = 1000,
	}

	// Use this for initialization
	void Start () {
		
		StartCoroutine(CreateObstacle());
	}
	
	private IEnumerator CreateObstacle() {
		for (;;) {
			int rnd = new System.Random().Next(1, 1000);
			print(rnd);
			foreach (Obstacles obs in System.Enum.GetValues(typeof(Obstacles))) {
				if (rnd < (int) obs) {
					switch (obs) {
						case Obstacles.Bubble:
						BubbleBehaviour.Spawn(baseBubble);
						break;
					default:
						break;
					}
				}
			}
			yield return new WaitForSeconds(1.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
