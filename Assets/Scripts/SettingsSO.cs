using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct Stats
{
	public float Speed;
	public int Damage;
	public int Health;
}
[CreateAssetMenu(fileName = "Settings", menuName = "SO", order = 1)]
public class SettingsSO : ScriptableObject
{
	public Stats Player;
	public Stats EnemyShield;
	public Stats EnemyBase;
	public float EnemyRange;
	public float EnemyKnockedTime;
	public Vector2 EnemyKnockSpeed;


}
