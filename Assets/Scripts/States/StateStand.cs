using UnityEngine;
using System.Collections;

public class StateStand : BaseState
{
	public override void Enter(eStates m_priviousState)
	{
		m_me.resetHeight();
	}

	public override void Exit()
	{
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.setVelocity(0.0f, 0.0f);

		//TODO clean this!
		//m_me.addToVelocity(0.0f, -25.0f * deltaTime);
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
		else if (inputs[(int)eInputs.LEFT] || inputs[(int)eInputs.RIGHT])
		{
			m_currentState = eStates.WALKING;
		}
		else if (inputs[(int)eInputs.JUMP])
		{
			//m_currentState = eStates.JUMPING;
			m_currentState = eStates.PRE_POST_JUMP;
		}
	}
}
