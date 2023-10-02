using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterStartWaiter : MonoBehaviour {

	public int waitDuration;
	int delay ;

	public bool done;

	public TextMesh show ;

	// Use this for initialization
	void Start () {
		StartCoroutine (Clock ());
	}

	//Clock
	IEnumerator Clock() {
		if (delay <= waitDuration) {
			delay++;
		}
		yield return new WaitForSecondsRealtime (0.1f);
		StartCoroutine (Clock ());
	}


	// Update is called once per frame
	void Update () {

		if (!done) {

			show.text = (waitDuration - delay).ToString ();

			if (delay < waitDuration) {
				Time.timeScale = 0;
				show.gameObject.SetActive (true);
			} else {
				Time.timeScale = 1;
				show.gameObject.SetActive (false);
				done = true;
			}

		}

	}
}
