using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointArea : MonoBehaviour
{

	public GameObject score;
	public string team;
	public float timer;
	public float targetTime;
	public bool transmited;
	
	// Use this for initialization
	void Start ()
	{
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transmited)
		{	
			if (timer >= targetTime)
			{
				score.gameObject.GetComponent<Score>().Connected(team);
				// mudar o trnasform do target
				timer = 0.0f;
				transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
				Debug.Log(transform.position);
			}
			else
			{
				timer += Time.deltaTime;
			}

			transmited = false;
		}
	}

}
