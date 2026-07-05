using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	[SerializeField] Slider _slider;
   	[SerializeField] TextMeshProUGUI _scoreTxt;

	[SerializeField] int _scoreValue;
	[SerializeField]float _currentHp;
	[SerializeField]float _totalHp;

	void Start()
	{
		Instance = this;
	}

	public void AddScore()
	{
		_scoreValue++;
	}
	// Update is called once per frame
	void Update()
    {
		_scoreTxt.text = "Score: " + _scoreValue.ToString();
		_slider.value = _currentHp / _totalHp;
    }

	
}
