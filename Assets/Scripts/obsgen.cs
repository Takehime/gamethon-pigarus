using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsgen : MonoBehaviour {
	// System.Random rand;

	[SerializeField] GameObject baseBubble, baseFloater;
	[SerializeField] Vector2 spawnBubbleVelocity = new Vector2(-5.0f, 0.0f);
	[SerializeField] Vector2 spawnFloaterVelocity = new Vector2(-5.0f, 0.0f);

	enum Obstacles {
		Floater = 50,
		Bubble = 66,
		None = 100,
	}

	// Use this for initialization
	void Start () {
		
		StartCoroutine(CreateObstacle());
	}
	
	private IEnumerator CreateObstacle() {
		for (;;) {
			int rnd = new System.Random().Next(1, 100);
			// print(rnd);
			foreach (Obstacles obs in System.Enum.GetValues(typeof(Obstacles))) {
				if (rnd < (int) obs) {
					switch (obs) {
						case Obstacles.Bubble:
							BubbleBehaviour.Spawn(baseBubble, spawnBubbleVelocity);
							break;
						case Obstacles.Floater:
							FloaterBehaviour.Spawn(baseFloater, spawnFloaterVelocity);
							break;
						default:
							break;
					}
					break;
				}
			}
			yield return new WaitForSeconds(1.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
