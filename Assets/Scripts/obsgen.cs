using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsgen : MonoBehaviour {
	// System.Random rand;

	[SerializeField] GameObject baseBubble, baseFloater;
	[SerializeField] Vector2 spawnBubbleVelocity = new Vector2(-5.0f, 0.0f);
	[SerializeField] Vector2 spawnFloaterVelocity = new Vector2(-5.0f, 0.0f);
    [SerializeField] float maxTimeNone = 3f;
    float timeNone = 0.0f;

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
        float newWait = 0.0f;
		for (;;) {
			int rnd = Random.Range(1, 100);
			// print(rnd);
			foreach (Obstacles obs in System.Enum.GetValues(typeof(Obstacles))) {
				if (rnd < (int) obs) {
					switch (obs) {
						case Obstacles.Bubble:
                            BubbleBehaviour.Spawn(baseBubble, spawnBubbleVelocity, this.transform.position);
                            newWait = 0.0f;
							break;
						case Obstacles.Floater:
                            FloaterBehaviour.Spawn(baseFloater, spawnFloaterVelocity, this.transform.position);
                            newWait = 0.0f;
							break;
						default:
                            timeNone += newWait;
                            if (timeNone > maxTimeNone) {
                                BubbleBehaviour.Spawn(baseBubble, spawnBubbleVelocity, this.transform.position);
                            }
							break;
					}
					break;
				}
			}
            newWait = Random.Range(0.8f, 2.0f);
            yield return new WaitForSeconds(newWait);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
