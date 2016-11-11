using UnityEngine;
using System.Collections;

public class HitBoxManager : MonoBehaviour
{
	private Hitbox[] m_hitboxes = new Hitbox[5];

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

public class Hitbox
{
	private float m_xPos = 0.0f;
	private float m_yPos = 0.0f;
	private float m_width = 0.0f;
	private float m_height = 0.0f;

	public Hitbox(float xPos, float yPos, float width, float height)
	{
		m_xPos = xPos;
		m_yPos = yPos;
		m_width = width;
		m_height = height;
	}

	public void UpdatePos(float xDelta, float yDelta)
	{
		m_xPos += xDelta;
		m_yPos += yDelta;
	}

	public bool IsPointInside(float x, float y)
	{
		return false;
	}

	public bool IsRectInside(float x, float y, float width, float height)
	{
		return false;
	}
}
