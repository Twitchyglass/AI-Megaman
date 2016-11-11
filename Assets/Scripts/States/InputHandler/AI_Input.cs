using UnityEngine;
using System.Collections;

public class AI_Input : BaseInputHandler
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
		inputs[(int)eInputs.RIGHT] = true;
	}
}
