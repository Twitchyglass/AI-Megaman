using UnityEngine;
using System.Collections;

public class StateGuard : BaseState
{
	public override void Enter(eStates m_priviousState)
	{
	}

	public override void Exit()
	{
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.setVelocity(0.0f, 0.0f);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
		if (!inputs[(int)eInputs.BLOCK])
		{
			m_currentState = eStates.STANDING;
		}
		else if (inputs[(int)eInputs.LEFT])
		{
			m_me.setLeft(true);
		}
		else if (inputs[(int)eInputs.RIGHT])
		{
			m_me.setLeft(false);
		}
	}
}
