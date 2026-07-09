using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] float _speed;
	bool _attacking;

	public void FinishAttack()
	{
		_attacking = false;
	}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Keyboard.current.aKey.isPressed)
		{
			transform.localEulerAngles = new Vector3(0, 180, 0);
			transform.position += _speed * Time.deltaTime * transform.right;
		}
		if (Keyboard.current.dKey.isPressed)
		{
			transform.localEulerAngles = new Vector3(0, 0, 0);
			transform.position += _speed * Time.deltaTime * transform.right;
		}

		if (Keyboard.current.eKey.isPressed && !_attacking)
		{
			_attacking = true;
		}
		if (Keyboard.current.qKey.isPressed && !_attacking)
		{
			_attacking = true;
			
		}
    }
}
