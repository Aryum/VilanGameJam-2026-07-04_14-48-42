using UnityEngine;

public class AttackBox : MonoBehaviour
{
	[SerializeField] bool _isHeavy;
	public void Attack(bool isHeavy)
	{
		_isHeavy = isHeavy;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Enemy enemy))
			enemy.TakeDamage(Ref.I.Settings.Player.Damage);
	}
}
