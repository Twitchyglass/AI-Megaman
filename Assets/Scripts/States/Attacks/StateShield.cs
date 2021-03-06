﻿using UnityEngine;
using System.Collections;

public class StateShield : BaseState
{
	Projectile m_shield;

	private float m_stateTime = 0.45f;
	private float m_throwTime = 0.32f;
	private float m_timer = 0.0f;
	private bool m_thrown = false;

	public override void Initialize(Charicter me)
	{
		m_me = me;

		m_me.AddProjectile(m_shield);
	}

	public void giveShield(Projectile shield)
	{
		m_shield = shield;
		m_shield.Initialize();
	}

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_stateTime;
	}

	public override void Exit()
	{
		m_thrown = false;
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_timer -= deltaTime;

		m_me.setVelocity(0.0f, 0.0f);

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
		else if (m_timer < m_throwTime && !m_thrown)
		{
			m_shield.Fire(m_me, 9.0f * m_me.getIntFacingLeft(), 0.0f, m_me.GetHitboxManager(), 2.0f);
			m_thrown = true;
		}
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
	}
}
