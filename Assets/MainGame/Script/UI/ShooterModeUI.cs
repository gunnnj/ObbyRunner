using System;
using UnityEngine;
using UnityEngine.UI;

public class ShooterModeUI : MonoBehaviour
{
    [SerializeField] Button btnJump;
    [SerializeField] Button btnShoot;

    public Action onJump;
    public Action onShoot;

    public void OnJump(){
        onJump?.Invoke();
    }
    public void OnShoot(){
        onShoot?.Invoke();
    }
}
