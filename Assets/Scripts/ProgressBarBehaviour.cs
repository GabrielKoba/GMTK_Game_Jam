using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBarBehaviour : MonoBehaviour
{
	[Tooltip("How long does it take for this slider to fill, in seconds")]
	public float FillTime = 1.0f;

	private Slider _slider;
	void Start ()
	{
		_slider = GetComponent<Slider>();
		Reset();
	}

	public void Reset()
	{
		_slider.minValue = Time.time;
		_slider.maxValue = Time.time + FillTime;
	}
	void Update ()
	{
		_slider.value = Time.time;
	}
}