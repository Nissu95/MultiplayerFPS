using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class PointPercentageBar : NetworkBehaviour {

    [SyncVar(hook = "OnChangeSlider")]
    public float percent = 50;

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangePercentBlue(float addPercentNumber)
    {
        percent += addPercentNumber * Time.deltaTime;
    }

    public void ChangePercentRed(float addPercentNumber)
    {
        percent -= addPercentNumber * Time.deltaTime;
    }

    void OnChangeSlider(float _percent)
    {
        slider.value = _percent;
    }

}
