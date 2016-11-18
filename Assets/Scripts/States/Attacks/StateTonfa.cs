using UnityEngine;
using System.Collections;

public class StateTonfa : BaseState
{
	private float m_slideTime = 32.0f / 60.0f;
	private float m_timer;
	private int m_hitboxID = 0;
	private float m_hitboxOffet = 1.5f;

	//private bool m_attackActive = false;
	//private float m_attackFrom = 9.0f / 60.0f;
	//private float m_attackTo = 13.0f / 60.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_slideTime;

		//m_attackActive = false;

		m_hitboxID = m_me.AddHitbox(m_me.getX() + (m_me.getIntFacingLeft() * m_hitboxOffet), m_me.getY(), 3.25f, 3.25f, 3, -4.0f, 8.0f, 0.75f, 0.2f, eAttackType.STRIKE);
	}

	public override void Exit()
	{
		m_me.RemoveHitbox(m_hitboxID);
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.SetHitboxPos(m_hitboxID, m_me.getX() + (m_me.getIntFacingLeft() * m_hitboxOffet), m_me.getY());

		m_me.setVelocity(0.0f, 0.0f);

		m_timer -= deltaTime;

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
	}
}
