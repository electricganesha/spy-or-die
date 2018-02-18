using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

	private float timer = 0.0f;
	public float timeToStart = 2.0f;
	public float timeToJumpToNextScene = 3.0f;
	public GameObject title;
	public GameObject titleChinese;
	public GameObject titleRussian;
	public GameObject startButton;

	private bool titleOnScreen = false;
	private bool pressedStart = false;
	private bool isStartThere = false;
	private Animator titleAnimator;
	private Animator titleAnimatorChinese;
	private Animator titleAnimatorRussian;
	private Animator startButtonAnimator;

	// Use this for initialization
	void Start () {
		titleAnimator = title.GetComponent<Animator> ();
		titleAnimatorChinese = titleChinese.GetComponent<Animator> ();
		titleAnimatorRussian = titleRussian.GetComponent<Animator> ();
		startButtonAnimator = startButton.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer > timeToStart && !titleOnScreen)
		{
			titleAnimator.SetTrigger ("menuAppear");
			titleAnimatorChinese.SetTrigger ("makeAppear");
			titleAnimatorRussian.SetTrigger ("makeAppear");
			startButtonAnimator.SetTrigger ("makeAppear");
			titleOnScreen = true;
		}
			

		/*if (!startButtonAnimator.GetComponent<Animation> ().isPlaying && isStartThere) {
			startButtonAnimator.SetTrigger ("makeFlicker");
		}*/

		if ((Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.KeypadEnter)) && titleOnScreen) {
			titleAnimator.SetTrigger ("menuDisappear");
			titleAnimatorChinese.SetTrigger ("makeDisappear");
			titleAnimatorRussian.SetTrigger ("makeDisappear");
			pressedStart = true;
			timer = 0.0f;
		}

		if (pressedStart && timer > timeToJumpToNextScene) {
			SceneManager.LoadScene (1);
		}

	}
}
