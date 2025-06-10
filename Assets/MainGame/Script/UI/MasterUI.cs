using System.Collections.Generic;
using UnityEngine;

public class MasterUI : MonoBehaviour
{
    public static MasterUI Instance {get; private set;}
    [SerializeField] protected List<ScreenUI> listScreen;

    private void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
}
