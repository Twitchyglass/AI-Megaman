using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject[] m_walls;
	public GameObject[] m_zeros;

	public EnviromentCollision[] m_colliders /*= new EnviromentCollision[1]*/;
	public Charicter[] m_charicters /*= new Charicter[1]*/;
	public State_Machine[] m_charicterStates;

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
	}

	// Update is called once per frame
	void Update()
	{
		//float deltaTime = Time.deltaTime;
		float deltaTime = 0.0166666666666667f;

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Cycle(deltaTime);
		}

		Collide(deltaTime);

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Physics(deltaTime);
		}
	}

	void Collide(float deltaTime)
	{
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
						Debug.Log("Collision H");
						colidedHorisontal = true;
						m_charicterStates[z].colideHorizontal();
					}
				}

				if (m_colliders[x].CollideVertical(m_charicters[z].predictTop(deltaTime)) || m_colliders[x].CollideVertical(m_charicters[z].predictBottom(deltaTime)))
				{
					if (m_colliders[x].CollideHorizontal(m_charicters[z].getLeft()) || m_colliders[x].CollideHorizontal(m_charicters[z].getRight()))
					{
						Debug.Log("Collision V");
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
}
