using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoManager : MonoBehaviour 
{
	Dictionary<Transform, Dictionary<Transform, Transform>> linePointMapping;
	List<Transform> points;
	int NUM = 100;

	Dictionary<Transform, Transform> lineMouseMapping;
	Transform mouse;

	int SPEED_MIN = 50;
	int SPEED_MAX = 100;

	int POINT_MIN_DISTANCE = 0;
	int POINT_MAX_DISTANCE = 30;

	int MOUSE_MIN_DISTANCE = 0;
	int MOUSE_MAX_DISTANCE = 100;

	// Use this for initialization
	void Start () 
	{
		linePointMapping = new Dictionary<Transform, Dictionary<Transform, Transform>>();
		points = new List<Transform>();
		for (int i = 0; i < NUM; i++)
		{
			GameObject point = GameObject.Instantiate(Resources.Load("Point")) as GameObject;
			point.GetComponent<MonoPoint>().Init(
				new Vector3(Random.Range(-Screen.width / 2, Screen.width / 2), Random.Range(-Screen.height / 2, Screen.height / 2), 0),
				new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0) * Random.Range(SPEED_MIN, SPEED_MAX),
				new Color(1, 1, 1, Random.Range(0f, 0.5f))
			);

			points.Add(point.transform);
		}

		for (int i = 0; i < NUM; i++)
		{
			linePointMapping[points[i]] = new Dictionary<Transform, Transform>();
			for (int j = 0; j < NUM; j++)
			{
				linePointMapping[points[i]][points[j]] = null;
			}
		}

		lineMouseMapping = new Dictionary<Transform, Transform>();
		mouse = (GameObject.Instantiate(Resources.Load("Point")) as GameObject).transform;
		for (int i = 0; i < NUM; i++)
		{
			lineMouseMapping[points[i]] = null;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < NUM; i++)
		{
			for (int j = 0; j < NUM; j++)
			{
				float d = Vector3.Distance(points[i].position, points[j].position);
				if (d > POINT_MIN_DISTANCE && d < POINT_MAX_DISTANCE)
				{
					DrawPointLine(points[i], points[j], Ratio(d, POINT_MIN_DISTANCE, POINT_MAX_DISTANCE));
				}
				else
				{
					RemovePointLine(points[i], points[j]);
				}
			}
		}

		Vector3 mouseP = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
		mouse.position = new Vector3(mouseP.x, mouseP.y, 0);
		for (int i = 0; i < NUM; i++)
		{
			float d = Vector3.Distance(points[i].position, mouse.position);
			if (d > MOUSE_MIN_DISTANCE && d < MOUSE_MAX_DISTANCE)
			{
				DrawMouseLine(points[i], mouse, Ratio(d, MOUSE_MIN_DISTANCE, MOUSE_MAX_DISTANCE));
			}
			else
			{
				RemoveMouseLine(points[i]);
			}
		}
	}

	void DrawPointLine(Transform from, Transform to, float ratio)
	{
		if (linePointMapping[from][to] == null)
		{
			linePointMapping[from][to] = (GameObject.Instantiate(Resources.Load("Line")) as GameObject).transform;
		}

		LineRenderer line = linePointMapping[from][to].GetComponent<LineRenderer>();
		line.SetPosition(0, from.position);
		line.SetPosition(1, to.position);
		line.material.color = new Color(1, 1, 1, ratio);
	}
	void RemovePointLine(Transform from, Transform to)
	{
		if (linePointMapping[from][to] != null)
		{
			GameObject.Destroy(linePointMapping[from][to].gameObject);
			linePointMapping[from][to] = null;
		}
	}

	void DrawMouseLine(Transform from, Transform to, float ratio)
	{
		if (lineMouseMapping[from] == null)
		{
			lineMouseMapping[from] = (GameObject.Instantiate(Resources.Load("Line")) as GameObject).transform;
		}

		LineRenderer line = lineMouseMapping[from].GetComponent<LineRenderer>();
		line.SetPosition(0, from.position);
		line.SetPosition(1, to.position);
		line.material.color = new Color(1, 1, 1, ratio);
	}
	void RemoveMouseLine(Transform from)
	{
		if (lineMouseMapping[from] != null)
		{
			GameObject.Destroy(lineMouseMapping[from].gameObject);
			lineMouseMapping[from] = null;
		}
	}

	float Ratio(float d, float min, float max)
	{
		return (max - d) / (max - min);
	}
}
