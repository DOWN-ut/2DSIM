using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManipulator : MonoBehaviour {

	[Tooltip("Enable auto-time-speed-change to reach desired FPS ?")]
	public bool enable;

	public float timeScale = 1;

	public float targetFPS;

	public float minTimeScale;
	public float maxTimeScale;

	public TextMesh FPS;

    public Slider timeSlider;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if(timeSlider != null)
        {
            timeScale = timeSlider.value;
        }

		Time.timeScale = timeScale;

		if (enable) {

			if ((1 / Time.deltaTime) < targetFPS) {
				timeScale = timeScale - 0.1f;
			}
			if ((1 / Time.deltaTime) > targetFPS) {
				timeScale = timeScale + 0.1f;
			}
			if (timeScale < minTimeScale) {
				timeScale = minTimeScale;
			}
			if (timeScale > maxTimeScale) {
				timeScale = maxTimeScale;
			}

		}

		if (FPS != null) {
			FPS.text = Mathf.Round (1 / Time.deltaTime).ToString () + " FPS";
		}

	}
}
