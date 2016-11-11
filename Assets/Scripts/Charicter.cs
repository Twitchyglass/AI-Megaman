using UnityEngine;
using System.Collections;

public class Charicter : MonoBehaviour
{
	private float[] m_position = new float[2];
	private float[] m_velocity = new float[2];
	private float[] m_maxVelocity = new float[2];

	Transform m_tansform;
	SpriteRenderer m_sprite;

	private float m_height;
	private float m_baseHeight;
	private float m_width;

	bool m_left;

	// Use this for initialization
	public void Initialize(SpriteRenderer sprite)
	{
		m_tansform = GetComponent<Transform>();
		m_sprite = sprite;

		m_position[0] = m_tansform.position.x;
		m_position[1] = m_tansform.position.y;

		//m_height = transform.localScale.y /** 0.5f*/;
		//m_width = transform.localScale.x /** 0.5f*/;

		m_baseHeight = 2.4f;
		m_height = m_baseHeight;
		m_width = 1.0f;
	}

	// Update is called once per frame
	public void F_Update(float deltaTime)
	{
		if (m_velocity[0] > 0.1)
			m_left = false;
		else if (m_velocity[0] < -0.1)
			m_left = true;

		m_sprite.flipX = m_left;

		Debug.DrawLine(new Vector3(getLeft(), getTop()), new Vector3(getLeft(), getBottom()), Color.green);
		Debug.DrawLine(new Vector3(getRight(), getTop()), new Vector3(getRight(), getBottom()), Color.green);
		Debug.DrawLine(new Vector3(getLeft(), getTop()), new Vector3(getRight(), getTop()), Color.green);
		Debug.DrawLine(new Vector3(getLeft(), getBottom()), new Vector3(getRight(), getBottom()), Color.green);
	}

	public void Physics(float deltaTime)
	{
		m_position[0] += m_velocity[0] * deltaTime;
		m_position[1] += m_velocity[1] * deltaTime;

		//Debug.Log("X Velocity " + m_velocity[0]);
		
		m_tansform.position = new Vector3(m_position[0], m_position[1], 0);
	}

	public void MovePos(float x, float y)
	{
		m_position[0] += x;
		m_position[1] += y;
	}

	public void addToVelocity(float x, float y)
	{
		m_velocity[0] += x;
		m_velocity[1] += y;

		clampVelocity();
	}

	public void setVelocity(float x, float y)
	{
		m_velocity[0] = x;
		m_velocity[1] = y;

		//clampVelocity();
	}

	private void clampVelocity()
	{
		//if (m_velocity[0] > m_maxVelocity[0])
		//{
		//	m_velocity[0] = m_maxVelocity[0];
		//}
		//else if (m_velocity[0] < -m_maxVelocity[0])
		//{
		//	m_velocity[0] = -m_maxVelocity[0];
		//}

		//if (m_velocity[1] > m_maxVelocity[1])
		//{
		//	m_velocity[1] = m_maxVelocity[1];
		//}
		//else if (m_velocity[1] < -m_maxVelocity[1])
		//{
		//	m_velocity[1] = -m_maxVelocity[1];
		//}
	}

	public void setLeft(bool left)
	{
		m_left = left;
	}

	public bool getFacingLeft()
	{
		return m_left;
	}

	public float predictX(float deltaTime)
	{
		return m_position[0] + m_velocity[0] * deltaTime;
	}

	public float predictY(float deltaTime)
	{
		return m_position[1] + m_velocity[1] * deltaTime;
	}

	public float getX()
	{
		return m_position[0];
	}

	public float getY()
	{
		return m_position[1];
	}

	public void colideHorizontal()
	{
		m_velocity[0] = 0.0f;
	}

	public void collideVertical(float top)
	{
		m_velocity[1] = 0.0f;
		//m_position[1] = top + 0.01f;
	}

	public float predictLeft(float deltaTime)
	{
		return getLeft() + m_velocity[0] * deltaTime;
	}

	public float predictRight(float deltaTime)
	{
		return getRight() + m_velocity[0] * deltaTime;
	}

	public float predictTop(float deltaTime)
	{
		return getBottom() + m_velocity[1] * deltaTime;
	}

	public float predictBottom(float deltaTime)
	{
		return getBottom() + m_velocity[1] * deltaTime;
	}

	public float getLeft()
	{
		return m_position[0] - m_width;
	}

	public float getRight()
	{
		return m_position[0] + m_width;
	}

	public float getTop()
	{
		return m_position[1] + m_height;
	}

	public float getBottom()
	{
		return m_position[1] /*- m_height*/;
	}

	public void setHeight(float newHeight)
	{
		m_height = newHeight;
	}

	public void resetHeight()
	{
		m_height = m_baseHeight;
	}
}
