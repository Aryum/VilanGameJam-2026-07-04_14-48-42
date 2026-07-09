using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	Spawner _spawner;
	int _healthCur;
	public bool IsKnockedback {private set; get; }
	public bool IsAttacking {private set; get; }
	bool _shield;

	Stats _stat;

	void Start()
	{
	 	_stat = _shield ? Ref.I.Settings.EnemyShield : Ref.I.Settings.EnemyBase;
		
	}
	void FinishedKnockBack()
	{
		IsKnockedback = false;
	}
	
	public void SetSpawner(Spawner spawn)
	{
		_spawner = spawn;
	}

	public void Spawn()
	{
		_healthCur = _stat.Health;
	}

	public void DealDamage()
	{
		
	}
	public void TakeDamage(int damage)
	{
		_healthCur -=damage;
		if (_healthCur <= 0)
			_spawner.OnEnemyDeath();
		else
			IsKnockedback = true;
	}
	void Update()
	{
		Vector3 dir = Player.Instance.transform.position - transform.position;
		IsAttacking = Vector3.Magnitude(dir) < Ref.I.Settings.EnemyRange;
		dir = dir.normalized;
		if (!IsKnockedback && !IsAttacking)
			transform.position += Time.deltaTime * _stat.Speed * dir;
	
			
			
	}
}
