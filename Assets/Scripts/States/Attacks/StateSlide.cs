using UnityEngine;
using System.Collections;

public class StateSlide : BaseState
{
	private float m_slideTime = 1.0f;
	private float m_timer;
	private float m_slideSpeed = 8.0f;
	private int m_hitboxID = 0;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_slideTime;
		m_me.setHeight(1.2f);
		m_hitboxID = m_me.AddHitbox(m_me.getX(), m_me.getY(), 2.75f, 1.75f, 3, 6.0f, 12.0f, 0.0f, 0.2f, eAttackType.STRIKE);
	}

	public override void Exit()
	{
		m_me.RemoveHitbox(m_hitboxID);
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		if (m_me.getFacingLeft())
			m_me.setVelocity(-m_slideSpeed, 0.0f);
		else
			m_me.setVelocity(m_slideSpeed, 0.0f);

		m_me.SetHitboxPos(m_hitboxID, m_me.getX(), m_me.getY());

		m_timer -= deltaTime;

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if (inputs[(int)eInputs.LEFT])
			m_me.setLeft(true);

		if (inputs[(int)eInputs.RIGHT])
			m_me.setLeft(false);
	}
}
