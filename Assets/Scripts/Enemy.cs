using UnityEngine;

public class Enemy : MonoBehaviour
{
	Spawner _spawner;
	int _healthCur;
	public bool IsKnockedback { private set; get; }
	public bool IsAttacking { private set; get; }
	[SerializeField] bool _shield;
	float _knockedTime;

	float _hitTime;

	Stats _stat;

	void Start()
	{
	 	_stat = _shield ? Ref.I.Settings.EnemyShield : Ref.I.Settings.EnemyBase;
		

		Debug.Log("Debug life");
		_healthCur = 10000;
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
	public void TakeDamage(int damage, float time, bool knockBack)
	{
		Debug.Log("Got attacked");
		if (_hitTime == time)
			return ;
		_hitTime = time;
		_healthCur -= damage;
		if (_healthCur <= 0 )
			_spawner.OnEnemyDeath();
		else if (!IsKnockedback && knockBack)
		{
			_knockedTime = time;
			IsKnockedback = true;

		}
	}
	void Update()
	{
		Vector3 dir = Player.Instance.transform.position - transform.position;
		dir = new Vector3(dir.x, 0 , 0);
		IsAttacking = Vector3.Magnitude(dir) < Ref.I.Settings.EnemyRange;
		dir = dir.normalized;
		
		if (Time.timeSinceLevelLoad > _knockedTime + Ref.I.Settings.EnemyKnockedTime)
			IsKnockedback = false;
		if (!IsKnockedback )
		{
			if (!IsAttacking)
				transform.position += Time.deltaTime * _stat.Speed * dir;
		}
		else
		{
			Vector2 speed = Ref.I.Settings.EnemyKnockSpeed;
			transform.position -= Time.deltaTime * speed.x * _stat.Speed * dir;
			float curTime = Time.timeSinceLevelLoad - _knockedTime;
			float percent = curTime / Ref.I.Settings.EnemyKnockedTime;
			if (percent < 0.5f)
			{
				transform.position += Time.deltaTime  * (0.5f - percent) * speed.y * Vector3.up;
				Debug.Log("Going up");
			}
			else
			{
				transform.position -= Time.deltaTime * (1f - percent) *  speed.y * Vector3.up;
				Debug.Log("Going down");
			}
		}
	}
}
