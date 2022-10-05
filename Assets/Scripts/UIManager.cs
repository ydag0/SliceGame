using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Canvas mainCanvas;
    [SerializeField] Button playButton;
    [SerializeField] Button restartButton;
    
    float playButtonAnimDuration = 1.5f;
    float gameEndWallAnimDuration = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    public void Play()
    {
        StartCoroutine(PlayCoroutine());
    }
    public void LevelEnd()
    {
        StartCoroutine(LevelEndCoroutine());
    }
    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }
    public void Died()
    {
        KnifeMovement.Instance.SetCanMove(false);
        StartCoroutine(DiedCoroutine());
        
    }
    IEnumerator PlayCoroutine()
    {
        KnifeMovement.Instance.SetCanMove(true);
        yield return new WaitForSeconds(playButtonAnimDuration);
        mainCanvas.gameObject.SetActive(false);
    }
    IEnumerator LevelEndCoroutine()
    {
        KnifeMovement.Instance.SetCanMove(false);
        yield return new WaitForSeconds(gameEndWallAnimDuration);
        mainCanvas.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }
    IEnumerator DiedCoroutine()
    {
        KnifeMovement.Instance.GetComponentInChildren<dispersePixels>().enabled = true;
        yield return new WaitForSeconds(.5f);   
        mainCanvas.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }
    IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(playButtonAnimDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
