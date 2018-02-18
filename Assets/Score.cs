using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public float gameTime;
	private string minutes;
	private string seconds;
	public int Red;
	public int Blue;
	
	// Use this for initialization
	void Start ()
	{
		gameTime = 299.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		minutes = Mathf.Floor(gameTime / 60).ToString("00");
		seconds = (gameTime % 60).ToString("00");
		
		if (gameTime > 0)
		{
			GameObject.FindGameObjectWithTag("RedScore").GetComponent<Text>().text = Red.ToString();
			GameObject.FindGameObjectWithTag("BlueScore").GetComponent<Text>().text = Blue.ToString();
			GameObject.FindGameObjectWithTag("GameTime").GetComponent<Text>().text = minutes + ":" + seconds;
			gameTime -= Time.deltaTime;
		}
		else
		{
			// load 'end game'
		}
	}

	public void Connected(string team)
	{
		if (team == "Red")
			Red++;
		else
			Blue++;
	}
}
