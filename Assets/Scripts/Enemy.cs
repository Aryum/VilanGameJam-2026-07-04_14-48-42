using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	Spawner _spawner;
	int _healthCur;
	public bool IsKnockedback { private set; get; }
	public bool IsAttacking { private set; get; }
	[SerializeField] bool _shield;
	[SerializeField] SpriteRenderer _sprite;

	float _knockedTime;

	float _hitTime;

	Stats _stat;

	float _minY;
	Vector2 _curSpeed;
	void Start()
	{
	 	_stat = _shield ? Ref.I.Settings.EnemyShield : Ref.I.Settings.EnemyBase;
		

		Debug.Log("Debug life");
		_healthCur = 10000;

		_minY = transform.position.y;
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

	IEnumerator SpriteDamage()
	{
		WaitForSeconds w = new WaitForSeconds(Ref.I.Settings.SpriteFlash_Delay);
		bool isAlpha = true;
		Color transparent =  new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _sprite.color.a / 2);
		Color color = _sprite.color;

		while(IsKnockedback) 
		{
			_sprite.color = isAlpha ? transparent : color;
			isAlpha = !isAlpha;
			yield return w;
		}
		_sprite.color = color;
	}

	public void TakeDamage(int damage, float time, bool knockBack)
	{
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
			StartCoroutine(SpriteDamage());
			_curSpeed.y = Ref.I.Settings.EnemyKnockedSpeed;
			_curSpeed.x = Ref.I.Settings.EnemyKnockedSpeed;

		}
	}
	void Update()
	{
		Vector3 dir = Player.Instance.transform.position - transform.position;
		dir = new Vector3(dir.x, 0 , 0);
		IsAttacking = Vector3.Magnitude(dir) < Ref.I.Settings.EnemyRange;
		dir = dir.normalized;
		
		
		if (!IsKnockedback )
		{
			if (!IsAttacking)
				transform.position += Time.deltaTime * _stat.Speed * dir;
		}
		else
		{
			if (transform.position.y >= _minY)
			{
				float acceration = -Ref.I.Settings.EnemyKnockedAcc;
				_curSpeed.y += acceration * Time.deltaTime;
				_curSpeed.x += acceration * Time.deltaTime * 0.5f;

				transform.position += _curSpeed.y * Time.deltaTime * Vector3.up + _curSpeed.x * Time.deltaTime * -dir;
			}
			else
				IsKnockedback = false;
			
		}
	}
}
