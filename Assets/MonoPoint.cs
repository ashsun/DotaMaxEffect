using UnityEngine;
using System.Collections;

public class MonoPoint : MonoBehaviour
{
	public Vector3 Speed { set; get; }	

	// Use this for initialization
	public void Init(Vector3 p, Vector3 s, Color c) 
	{
		transform.position = p;
		Speed = s;
		transform.GetComponent<SpriteRenderer>().color = c;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += Speed * Time.deltaTime;
		if (transform.position.x < -Screen.width / 2)
		{
			transform.position = new Vector3(Screen.width / 2, transform.position.y, transform.position.z);
		}
		else if (transform.position.x > Screen.width / 2)
		{
			transform.position = new Vector3(-Screen.width / 2, transform.position.y, transform.position.z);
		}
		else if (transform.position.y < -Screen.height / 2)
		{
			transform.position = new Vector3(transform.position.x, Screen.height / 2, transform.position.z);
		}
		else if (transform.position.y > Screen.height / 2)
		{
			transform.position = new Vector3(transform.position.x, -Screen.height / 2, transform.position.z);
		}
	}
}
