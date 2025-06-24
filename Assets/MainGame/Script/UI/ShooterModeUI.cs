using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShooterModeUI : MonoBehaviour
{
    [SerializeField] GameObject OptionGun;
    [SerializeField] Button gunLight;
    [SerializeField] Button gunMedium;
    [SerializeField] Button gunWeight;
    [SerializeField] Button btnGo;
    [SerializeField] Slider Armor;
    [SerializeField] Slider Health;
    [SerializeField] TMP_Text scoreLeftTeam;
    [SerializeField] TMP_Text scoreRightTeam;
    private int scoreL;
    private int scoreR;
    private PlayerShootMode playerShootMode;

    public Action onJump;
    public Action onShoot;
    async void Start()
    {
        playerShootMode = FindFirstObjectByType<PlayerShootMode>();
        gunLight.onClick.AddListener(()=>playerShootMode.PickGun(TypeGun.Light));
        gunMedium.onClick.AddListener(()=>playerShootMode.PickGun(TypeGun.Medium));
        gunWeight.onClick.AddListener(()=>playerShootMode.PickGun(TypeGun.Weight));
        btnGo.onClick.AddListener(()=>ActiveOptionGun(false));
        ResetUI();
        ActiveOptionGun(false);

        scoreLeftTeam.text = "0";
        scoreRightTeam.text = "0";
        scoreL = 0;
        scoreR = 0;

        await Task.Delay(1000);
        ActiveOptionGun(true);
    }
    public void ActiveOptionGun(bool value){
        OptionGun.SetActive(value);
    }
    public void OnJump(){
        onJump?.Invoke();
    }
    public void OnShoot(){
        onShoot?.Invoke();
    }
    public void UpdateUI(int armor, int health){
        if(armor>0){
            Armor.value = (float)armor/100;
        }
        else{
            Armor.value = 0;
        }

        if(health>0){
            Health.value = (float)health/100;
        }
        else{
            Health.value = 0;
        }
        
    }
    public void ResetUI(){
        Armor.value = 1;
        Health.value = 1;
    }
    public void UpdateScore(TagTarget tag){
        if(tag.Equals("Player")){
            scoreL ++;
            scoreLeftTeam.text = scoreL.ToString();
        }
        else{
            scoreR++;
            scoreRightTeam.text = scoreR.ToString();
        }
    }
}
