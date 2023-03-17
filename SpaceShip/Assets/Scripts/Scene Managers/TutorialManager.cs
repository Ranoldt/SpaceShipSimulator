using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;


    private void Awake()
    {
        DisableControls(); //make sure the player can't control their ship when the tutorial starts
    }


    //activate + deactivate control methods called when approporate as unity event responses.
    public void EnableControls()
    {
        input.ActivateInput();
    }

    public void DisableControls()
    {
        input.DeactivateInput();
    }

    public void FinishTutorial()
    {
        SceneManager.LoadScene(0);
    }
}
