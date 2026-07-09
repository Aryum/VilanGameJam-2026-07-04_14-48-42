using System;
using UnityEngine;

public class Ref : MonoBehaviour
{
	public static Ref I;
	[field:SerializeField] public SettingsSO Settings {private set; get; }
    void Start()
    {
        I = this;
    }

   
}
