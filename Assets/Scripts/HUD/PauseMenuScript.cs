using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Button primaryButton;
    [SerializeField] private PauseEvent pauseEvent;
    [SerializeField] private GameObject myEventSystem;

    public void Resume(){
        playerInput.enabled = true;
        pauseEvent.Invoke(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
		AudioManager.Instance.Play("SFX_ButtonClick");
        GameIsPaused = false;
    }

    public void Pause(){
        playerInput.enabled = false;
        pauseEvent.Invoke(true);
        pauseMenuUI.SetActive(true);
        primaryButton.Select();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GoToMenu(){
        GameIsPaused = false;
        Time.timeScale = 1f;
		AudioManager.Instance.Play("SFX_ButtonClick");
        AudioManager.Instance.Stop("BGM_GameplayMusic");
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Menu");
    }

    public void OnMenuStartOpen(InputAction.CallbackContext context){
        if(!GameIsPaused){
            if(context.performed){
                Pause();
            }
        }
    }

}
