﻿using UnityEngine;
using System.Collections;

public class SaberAttack : BaseAttack
{
	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[3], 3.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[48], 5.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[49], 7.0f / 60.0f);
	}

	public override BaseState InitializeState()
	{
		return gameObject.AddComponent<StateSaber>();
	}
}
