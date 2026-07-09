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
	[SerializeField] Transform _leftSpawner;
	[SerializeField] Transform _rightSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
		if(Random.Range(0, 2) == 1)
		{
			_enemyPool[cur].transform.position = _leftSpawner.position;
			_enemyPool[cur].transform.rotation = _leftSpawner.rotation;
		}
		else
		{
			_enemyPool[cur].transform.position = _rightSpawner.position;
			_enemyPool[cur].transform.rotation = _rightSpawner.rotation;
		}
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
