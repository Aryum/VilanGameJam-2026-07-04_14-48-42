using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
	public static Player Instance;

	bool _isAttacking;
	bool _isWalking;

	Animator _anim;

	public void FinishAttack()
	{
		_isAttacking = false;
		_anim.SetBool("IsHeavy", false);
		_anim.SetBool("IsLight", false);
	}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
		_anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
		_isWalking = false;
		if (!_isAttacking)
		{
			if (Keyboard.current.eKey.isPressed)
			{
				_isAttacking = true;
				_anim.SetBool("IsHeavy", true);
				return ;
			}
			if (Keyboard.current.qKey.isPressed)
			{
				_isAttacking = true;
				_anim.SetBool("IsLight", true);
				return ;
			}
			
			if (Keyboard.current.aKey.isPressed)
			{
				transform.localEulerAngles = new Vector3(0, 0, 0);
				transform.position -= Ref.I.Settings.Player.Speed * Time.deltaTime * transform.right;
				_isWalking = true;
			}
			if (Keyboard.current.dKey.isPressed)
			{
				transform.localEulerAngles = new Vector3(0, 180, 0);
				transform.position -= Ref.I.Settings.Player.Speed * Time.deltaTime * transform.right;
				_isWalking = true;
			}
			_anim.SetBool("IsWalking", _isWalking);
		}
		
    }
}
