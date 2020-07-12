using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBarBehaviour : MonoBehaviour
{
	[SerializeField] TroubleManager troubleManager;
	public float elapsedTime;
	private Slider _slider;
	void Start ()
	{
		_slider = GetComponent<Slider>();
		Reset();
	}

	public void Reset()
	{
		_slider.minValue = 0;
		_slider.maxValue = troubleManager.timeBeforeNightEnd;
	}
	void Update ()
	{
		if (troubleManager.gameStarted){
			elapsedTime += Time.deltaTime;
			_slider.value = elapsedTime;
		}
	}
}