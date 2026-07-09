using UnityEngine;

public class AttackBox : MonoBehaviour
{

	bool _isHeavy;
	public void Attack(bool isHeavy)
	{
		_isHeavy = isHeavy;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		
	}
}
