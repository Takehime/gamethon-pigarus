using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InstantiatedSceneLoader : MonoBehaviour {

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}
}
