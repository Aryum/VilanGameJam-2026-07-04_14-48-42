using UnityEngine;

public class AttackBox : MonoBehaviour
{
	[SerializeField] bool _isHeavy;
	bool _active;
	GameObject _child;
	void Start()
	{
		_child = transform.GetChild(0).gameObject;
		_child.SetActive(false);
	}
	public void Attack()
	{
		_time = Time.timeSinceLevelLoad;
		_active = true;
		_child.SetActive(true);
	}

	public void Finish()
	{
		_active = false;
		_child.SetActive(false);
	}

	float _time;

	void OnTriggerStay2D(Collider2D collision)
	{
		if (_active && collision.gameObject.TryGetComponent(out Enemy enemy))
			enemy.TakeDamage(Ref.I.Settings.Player.Damage, _time, _isHeavy);
	}
	
	
}
