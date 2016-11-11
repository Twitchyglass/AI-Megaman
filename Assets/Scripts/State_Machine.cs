using UnityEngine;
using System.Collections;

public enum eStates
{
	STANDING,
	WALKING,
	JUMPING,
	PRE_POST_JUMP,
	FALLING,

	SLIDING,
	BLOCK,

	SPAWNING,
	KO_D,

	SIZE_OF_E_STATES
}

public enum eInputs
{
	UP,
	DOWN,
	LEFT,
	RIGHT,

	JUMP,
	ATTACK_1,
	ATTACK_2,
	BLOCK,

	SIZE_OF_E_INPUTS
}

public class State_Machine : MonoBehaviour
{
	public Sprite[] m_sprites;
	private BaseState[] m_stateClass = new BaseState[(int)(eStates.SIZE_OF_E_STATES)];
	
	public AnimClass m_animations;
	private SpriteRenderer m_sprite;
	Charicter m_me;

	public eStates m_newState;
	public eStates m_currentState;

	private bool[] m_inputs = new bool[(int)eInputs.SIZE_OF_E_INPUTS];
	public BaseInputHandler m_inputHandler;

	// Use this for initialization
	public void Initialize()
	{
		m_me = GetComponent<Charicter>();
		m_sprite = GetComponent<SpriteRenderer>();
		m_inputHandler = GetComponent<BaseInputHandler>();
		m_inputHandler.Initialize(); 

		 m_stateClass[(int)eStates.STANDING] = GetComponent<StateStand>();
		m_stateClass[(int)eStates.WALKING] = GetComponent<StateWalk>();
		m_stateClass[(int)eStates.JUMPING] = GetComponent<StateJump>();
		m_stateClass[(int)eStates.PRE_POST_JUMP] = GetComponent<StatePrePostJump>();
		m_stateClass[(int)eStates.FALLING] = GetComponent<StateFall>();
		m_stateClass[(int)eStates.SLIDING] = GetComponent<StateSlide>();
		m_stateClass[(int)eStates.BLOCK] = GetComponent<StateGuard>();
		m_stateClass[(int)eStates.SPAWNING] = GetComponent<StateSpawn>();
		m_stateClass[(int)eStates.KO_D] = GetComponent<StateKO>();

		m_newState = eStates.SPAWNING;
		m_currentState = eStates.STANDING;

		m_stateClass[(int)m_newState].Enter(m_currentState);

		for (int z = 0; z < (int) eStates.SIZE_OF_E_STATES; z++)
		{
			m_stateClass[z].Initialize(m_me);
		}

		m_animations = GetComponent<AnimClass>();
		m_animations.F_initialize(m_sprite);

		m_animations.addAnim(); // STAND
		m_animations.addKeyFrame((int)eStates.STANDING, m_sprites[1], 0.01f);
		m_animations.animRepeat((int)eStates.STANDING);

		m_animations.addAnim(); // WALKING
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[4], 0.15f);
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[5], 0.15f);
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[6], 0.15f);
		m_animations.addKeyFrame((int)eStates.WALKING, m_sprites[5], 0.15f);
		m_animations.animRepeat((int)eStates.WALKING);

		m_animations.addAnim(); // JUMPING
		m_animations.addKeyFrame((int)eStates.JUMPING, m_sprites[7], 0.01f);
		m_animations.animRepeat((int)eStates.JUMPING);

		m_animations.addAnim(); // PRE_POST_JUMP
		m_animations.addKeyFrame((int)eStates.PRE_POST_JUMP, m_sprites[1], 0.01f);
		m_animations.animRepeat((int)eStates.PRE_POST_JUMP);

		m_animations.addAnim(); // FALLING
		m_animations.addKeyFrame((int)eStates.FALLING, m_sprites[7], 0.01f);
		m_animations.animRepeat((int)eStates.FALLING);

		m_animations.addAnim(); // SLIDING
		m_animations.addKeyFrame((int)eStates.SLIDING, m_sprites[26], 0.01f);
		m_animations.animRepeat((int)eStates.SLIDING);

		m_animations.addAnim(); // BLOCK
		m_animations.addKeyFrame((int)eStates.BLOCK, m_sprites[51], 0.01f);
		m_animations.animRepeat((int)eStates.BLOCK);

		m_animations.addAnim(); // SPAWNING
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[0], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[0], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[10], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[11], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[1], 0.2f);
		m_animations.addKeyFrame((int)eStates.SPAWNING, m_sprites[2], 0.25f);

		m_animations.F_play();

		ResetInputs();
		m_me.Initialize(m_sprite);
	}

	// Update is called once per frame
	public void Cycle(float deltaTime)
	{
		ResetInputs();
		m_inputHandler.Inputs(ref m_inputs);

		m_stateClass[(int)m_newState].Input(m_inputs, ref m_newState);
		StateCheck();

		m_stateClass[(int)m_newState].Cycle(deltaTime, ref m_newState);
		StateCheck();

		m_animations.setAnim((int)m_currentState);
		m_animations.F_update(deltaTime);

		m_me.F_Update(deltaTime);

		/*for (int z = 0; z < m_projectiles.Count; z++)
		{
			m_projectiles[z].F_update(deltaTime);
		}*/
	}

	public void Physics(float deltaTime)
	{
		m_me.Physics(deltaTime);
	}

	void StateCheck()
	{
		if (m_newState != m_currentState)
		{
			m_stateClass[(int)m_currentState].Exit();
			m_stateClass[(int)m_newState].Enter(m_currentState);

			m_currentState = m_newState;
		}
	}

	void ResetInputs()
	{
		for (int z = 0; z < (int)eInputs.SIZE_OF_E_INPUTS; z++)
		{
			m_inputs[z] = false;
		}
	}

	public void colideHorizontal()
	{
		m_stateClass[(int)m_newState].CollideHorizontal(ref m_newState);
		m_me.colideHorizontal();
	}

	public void collideVertical(float top)
	{
		m_stateClass[(int)m_newState].CollideVertical(ref m_newState);
		m_me.collideVertical(top);
	}

	public void notColideHorizontal()
	{
		m_stateClass[(int)m_newState].NotCollideHorizontal(ref m_newState);
	}

	public void notCollideVertical()
	{
		m_stateClass[(int)m_newState].NotCollideVertical(ref m_newState);
	}
}
