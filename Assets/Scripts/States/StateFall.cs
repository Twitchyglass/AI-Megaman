﻿using UnityEngine;
using System.Collections;

public class StateFall : BaseState
{
	private float m_gravity = -25.0f;

	public override void Enter(eStates m_priviousState)
	{
	}

	public override void Exit()
	{

	}

	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.addToVelocity(0.0f, m_gravity * deltaTime);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{

	}

	public override void CollideVertical(ref eStates m_currentState)
	{
		m_currentState = eStates.STANDING;
	}
}
