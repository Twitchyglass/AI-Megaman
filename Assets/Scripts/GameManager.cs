using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject[] m_walls;
	public GameObject[] m_zeros;

	private Transform m_transform;

	public EnviromentCollision[] m_colliders /*= new EnviromentCollision[1]*/;
	public Charicter[] m_charicters /*= new Charicter[1]*/;
	public State_Machine[] m_charicterStates;

	private float m_timer = 0.0f;
	private float m_KO_Time = 3.0f;
	private float m_KO_Slowdown = 0.25f;
	private bool m_KOd = false;

	// Use this for initialization
	void Start()
	{
		//for (int z = 0; z < m_walls.Length; z++)
		//{
		//	m_colliders[z] = m_walls[z].GetComponent<EnviromentCollision>();
		//	//m_colliders[z] = m_walls[z].GetComponent(typeof(EnviromentCollision)) as EnviromentCollision;
		//}

		//for (int z = 0; z < m_zeros.Length; z++)
		//{
		//	m_charicters[z] = m_zeros[z].GetComponent<Charicter>();
		//	//m_charicters[z] = m_zeros[z].GetComponent(typeof(Charicter)) as Charicter;
		//}

		//m_charicterStates[0] = m_zeros[0].GetComponent<State_Machine>();

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Initialize();
		}

		m_transform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		//float deltaTime = Time.deltaTime;
		float deltaTime = 0.0166666666666667f;

		KO_Check(ref deltaTime);

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Cycle(deltaTime);
		}

		Collide(deltaTime);

		HitCheck(deltaTime);

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Physics(deltaTime);
		}

		CameraUpdate(deltaTime);
	}

	void KO_Check(ref float deltaTime)
	{
		bool previouslyKOd = m_KOd;

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			if (m_charicters[z].IsKOd())
			{
				m_KOd = true;
			}
		}

		if (m_KOd)
		{
			if (!previouslyKOd)
			{
				m_timer = m_KO_Time;
			}

			if (m_timer > 0.0f)
			{
				m_timer -= deltaTime;
				deltaTime *= m_KO_Slowdown;
			}
		}
	}

	void Collide(float deltaTime)
	{
		for (int z = 0; z < m_zeros.Length; z++)
		{
			for (int x = 1; x < m_zeros.Length; x++)
			{
				if (m_charicters[x].isCollidedHorizontal(m_charicters[z].predictLeft(deltaTime)) || m_charicters[x].isCollidedHorizontal(m_charicters[z].predictRight(deltaTime)))
				{
					if (m_charicters[x].isCollidedVertical(m_charicters[z].getTop()) || m_charicters[x].isCollidedVertical(m_charicters[z].getBottom()))
					{
						Debug.Log("Collision H Char");
						float maxVel = m_charicters[z].GetXVelocity();

						if (Mathf.Abs(m_charicters[x].GetXVelocity()) > Mathf.Abs(maxVel))
							maxVel = m_charicters[x].GetXVelocity();

						m_charicterStates[z].colideHorizontal(maxVel);
						m_charicterStates[x].colideHorizontal(maxVel);

						//float tempVel = m_charicters[z].GetXVelocity();
						//m_charicterStates[z].colideHorizontal(m_charicters[x].GetXVelocity());
						//m_charicterStates[x].colideHorizontal(tempVel);
					}
				}
			}
		}

		for (int z = 0; z < m_zeros.Length; z++)
		{
			bool colidedHorisontal = false;
			bool colidedVertical = false;

			for (int x = 0; x < m_walls.Length; x++)
			{
				if (m_colliders[x].CollideHorizontal(m_charicters[z].predictLeft(deltaTime)) || m_colliders[x].CollideHorizontal(m_charicters[z].predictRight(deltaTime)))
				{
					if (m_colliders[x].CollideVertical(m_charicters[z].getTop()) || m_colliders[x].CollideVertical(m_charicters[z].getBottom()))
					{
						//Debug.Log("Collision H");
						colidedHorisontal = true;
						m_charicterStates[z].colideHorizontal();
					}
				}

				if (m_colliders[x].CollideVertical(m_charicters[z].predictTop(deltaTime)) || m_colliders[x].CollideVertical(m_charicters[z].predictBottom(deltaTime)))
				{
					if (m_colliders[x].CollideHorizontal(m_charicters[z].getLeft()) || m_colliders[x].CollideHorizontal(m_charicters[z].getRight()))
					{
						//Debug.Log("Collision V");
						colidedVertical = true;
						m_charicterStates[z].collideVertical(m_colliders[x].GetTop());
					}
				}
			}

			if (!colidedHorisontal)
			{
				m_charicterStates[z].notColideHorizontal();
			}

			if (!colidedVertical)
			{
				m_charicterStates[z].notCollideVertical();
			}
		}
	}

	void HitCheck(float deltaTime)
	{
		for (int z = 0; z < m_zeros.Length; z++)
		{
			for (int x = 0; x < m_zeros.Length; x++)
			{
				if (z != x)
				{
					if (m_charicters[z].getX() > m_charicters[x].getX())
						m_charicters[z].AttackEnemy(m_charicters[x], 1.0f);
					else
						m_charicters[z].AttackEnemy(m_charicters[x], -1.0f);
				}
			}
		}
	}

	void CameraUpdate(float deltaTime)
	{
		float TargateX = 0.0f;
		float TargateY = 0.0f;
		int numZeros = m_zeros.Length;

		for (int z = 0; z < numZeros; z++)
		{
			TargateX += m_charicters[z].getX();
			TargateY += m_charicters[z].getY();
		}

		TargateX /= numZeros;
		TargateY /= numZeros;

		m_transform.position = new Vector3(TargateX, TargateY + 2.0f, -10.0f);
	}
}
