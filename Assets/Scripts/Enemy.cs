using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	Spawner _spawner;
	int _healthMax;
	int _healthCur;
	int _damage;

	public void SetSpawner(Spawner spawn)
	{
		_spawner = spawn;
	}

	public void Spawn()
	{
		_healthCur = _healthMax;
	}

	public void DealDamage()
	{
		
	}
	public void TakeDamage(int damage)
	{
		_healthCur -=damage;
		if (_healthCur <= 0)
		{
			_spawner.OnEnemyDeath();
		}
	}
}
