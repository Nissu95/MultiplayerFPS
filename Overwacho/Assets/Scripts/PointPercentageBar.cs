using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PointPercentageBar : NetworkBehaviour {

    [SyncVar(hook = "OnChangeSlider")]
    public float percent = 50;

    Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
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

        if (_percent >= 100 || _percent <= 0)
        {
            RpcEndGame();
        }
    }

    [ClientRpc]
    void RpcEndGame()
    {
        Player[] playerScripts = FindObjectsOfType<Player>();

        for (int i = 0; i < playerScripts.Length; i++)
        {
            if (playerScripts[i].isLocalPlayer && (percent >= 100 || percent <= 0))
            {
                if (percent >= 100 )
                {
                    if (playerScripts[i].GetTeam() == Team.Blue)
                        playerScripts[i].SetVictory();
                    else
                        playerScripts[i].SetDefeat();
                }
                else
                {
                    if (playerScripts[i].GetTeam() == Team.Red)
                        playerScripts[i].SetVictory();
                    else
                        playerScripts[i].SetDefeat();
                }
            }
        }
    }
}
