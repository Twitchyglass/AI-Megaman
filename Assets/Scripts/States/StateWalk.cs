﻿using UnityEngine;
using System.Collections;

public class StateWalk : BaseState
{
	private float m_movementSpeed = 6.0f;

	public override void Enter(eStates m_priviousState)
	{
	}

	public override void Exit()
	{
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.addToVelocity(0.0f, -25.0f * deltaTime);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if (inputs[(int)eInputs.DOWN] && inputs[(int)eInputs.ATTACK_1])
		{
			m_currentState = eStates.SLIDING;
		}
		else if (inputs[(int)eInputs.BLOCK])
		{
			m_currentState = eStates.BLOCK;
		}
		else if (inputs[(int)eInputs.JUMP])
		{
			//m_currentState = eStates.JUMPING;
			m_currentState = eStates.PRE_POST_JUMP;
		}
		else if (inputs[(int)eInputs.LEFT])
		{
			m_me.setVelocity(-m_movementSpeed, 0.0f);
		}
		else if (inputs[(int)eInputs.RIGHT])
		{
			m_me.setVelocity(m_movementSpeed, 0.0f);
		}
		else
		{
			m_currentState = eStates.STANDING;
		}
	}

	public override void NotCollideVertical(ref eStates m_currentState)
	{
		m_currentState = eStates.FALLING;
	}
}
