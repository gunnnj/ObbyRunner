using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapReviveUI : MonoBehaviour
{
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text textTime;
    [SerializeField] int timeCooldown = 30;
    bool isWin = false;

    void OnEnable()
    {
        GameEvent.eventLoseGame+=LoseGame;
        GameEvent.eventWinGame+=WinGame;
    }

    void Start()
    {
        loseUI.SetActive(false);
        winUI.SetActive(false);
        StartCoroutine(CoolDownTime());
    }
    void OnDisable()
    {
        GameEvent.eventLoseGame-=LoseGame;
        GameEvent.eventWinGame-=WinGame;
    }
    private void WinGame()
    {
        winUI.SetActive(true);
        isWin = true;
    }
    private void LoseGame()
    {
        if(!isWin){
            loseUI.SetActive(true);
        }
    }
    private IEnumerator CoolDownTime(){
        while(timeCooldown>0){
            textTime.text = timeCooldown.ToString();
            yield return new WaitForSeconds(1f);
            timeCooldown--;
        }
        textTime.text = timeCooldown.ToString();
        GameEvent.eventLoseGame?.Invoke();
    }
    public void PlayAgain(){
        int idScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idScene);
    }
    public void Home(){
        SceneManager.LoadScene(0);
    }
}
