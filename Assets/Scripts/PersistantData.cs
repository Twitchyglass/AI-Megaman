using UnityEngine;
using System.Collections;

public class PersistantData : MonoBehaviour
{
	public PersistantData m_instance = null;
	private int m_winner = -1;
	private int m_numPlayers = 0;
	
	// Use this for initialization
	void Start()
	{
		if (m_instance == null)
			m_instance = this;
		else
			Destroy(this);

		DontDestroyOnLoad(this);
	}

	void SetWinner(int winner)
	{
		m_winner = winner;
	}

	int GetWinner()
	{
		return m_winner;
	}
}
