using System.Threading.Tasks;
using UnityEngine;

public class BotHeath : BaseHealth
{
    [SerializeField] Transform transParent;
    [SerializeField] BotShooterMode botShooterMode;
    protected override async void Revide()
    {
        base.Revide();
        shooterModeUI.UpdateScore(botShooterMode.tagTarget);
        Debug.Log(transParent.name+" dead");
        transParent.gameObject.SetActive(false);
        await Task.Delay(1000);
        botShooterMode.SetPowerByGun();
        transParent.position = RevidePosition;
        transParent.gameObject.SetActive(true);
        botShooterMode.agent.enabled = true;
        botShooterMode.SetNewPos();

    }
}
