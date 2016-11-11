using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInputHandler
{

	// Use this for initialization
	public override void Initialize()
	{

	}

	// Update is called once per frame
	public override void Cycle()
	{

	}

	public override void ReceveStatus()
	{

	}

	public override void Inputs(ref bool[] inputs)
	{
		if (Input.GetAxisRaw("Horizontal") < -0.5f)
			inputs[(int)eInputs.LEFT] = true;

		if (Input.GetAxisRaw("Horizontal") > 0.5f)
			inputs[(int)eInputs.RIGHT] = true;

		if (Input.GetAxisRaw("Vertical") < -0.5f)
			inputs[(int)eInputs.DOWN] = true;

		if (Input.GetAxisRaw("Vertical") > 0.5f)
			inputs[(int)eInputs.UP] = true;

		if (Input.GetButton("Jump"))
			inputs[(int)eInputs.JUMP] = true;

		if (Input.GetButton("Block"))
			inputs[(int)eInputs.BLOCK] = true;

		if (Input.GetButton("Attack1"))
			inputs[(int)eInputs.ATTACK_1] = true;
	}
}
