using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] float _spawnDelay;
	float _spawnTime;

	int _maxEnemyCount;
	int _curEnemyCount;
	int _spawnedEnemyCount;

	[SerializeField] Enemy _enemyAsset;
	[SerializeField] List<Enemy> _enemyPool;

	bool _isRightSide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		_isRightSide = gameObject.name != "Left";
		if (_isRightSide)
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, -transform.localEulerAngles.z);
		for(int i = 0; i < _maxEnemyCount; i++)
		{
			Enemy obj = Instantiate(_enemyAsset.gameObject).GetComponent<Enemy>();
			_enemyPool.Add(obj);
			obj.gameObject.SetActive(false);
			obj.SetSpawner(this);
		}
    }

	void SpawnEnemy()
	{
		if (_curEnemyCount == _maxEnemyCount)
			return ;
		_spawnedEnemyCount++;
		int cur = _spawnedEnemyCount % _maxEnemyCount;
		_enemyPool[cur].gameObject.SetActive(true);
		_enemyPool[cur].Spawn();
	}
	public void OnEnemyDeath()
	{
		_curEnemyCount--;
		UIManager.Instance.AddScore();
	}

    // Update is called once per frame
    void Update()
    {
		if (Time.timeSinceLevelLoad > _spawnTime + _spawnDelay)
		{
			SpawnEnemy();
			_spawnTime = Time.timeSinceLevelLoad;
		}
    }
}
