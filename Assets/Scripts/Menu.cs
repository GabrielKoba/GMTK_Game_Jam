using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    #region Sigleton
    private static Menu instance;
    public static Menu Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<Menu>();
            return instance;
        }
    }
    #endregion

    [SerializeField] TroubleManager troubleManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] GameObject MenuMusic;
    [SerializeField] GameObject MainMusic;
    [SerializeField] GameObject EndMusic;
    [SerializeField] GameObject GameHUD;
    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject EndUI;
    [SerializeField] float shakeDurationWhenUIClick;

    public void StartGame() {
        troubleManager.gameStarted = true;
        playerMovement.enabled = true;
        playerCombat.enabled = true;

        MenuMusic.SetActive(false);
        MainMusic.SetActive(true);

        GameHUD.transform.position = new Vector3 (0, 0, 0);
        MenuUI.transform.position = new Vector3 (0, 1000, 0);

        CameraShake.Instance.shakeDuration = shakeDurationWhenUIClick;
    }

    public void EndGame() {
        troubleManager.gameStarted = false;
        playerMovement.enabled = false;
        playerCombat.enabled = false;

        GameHUD.transform.position = new Vector3 (0, 1000, 0);
        EndUI.transform.position = new Vector3 (0, 0, 0);

        MainMusic.SetActive(false);
        EndMusic.SetActive(true);

        CameraShake.Instance.shakeDuration = shakeDurationWhenUIClick;
    }

    public void RestartGame() {
        SceneManager.LoadScene("Game");
    }

    public void Easy() {

    }
    public void Normal() {

    }
    public void Hard() {

    }
}
