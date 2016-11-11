using UnityEngine;
using System.Collections;

public class StateSlide : BaseState
{
	private float m_slideTime = 1.0f;
	private float m_timer;
	private float m_slideSpeed = 8.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_slideTime;
		m_me.setHeight(1.2f);
	}

	public override void Exit()
	{
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		if (m_me.getFacingLeft())
			m_me.setVelocity(-m_slideSpeed, 0.0f);
		else
			m_me.setVelocity(m_slideSpeed, 0.0f);

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
