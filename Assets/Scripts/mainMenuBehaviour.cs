using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuBehaviour : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public GameObject instructionsPanel;
    [SerializeField]
    private string selectedCharacter = "male";
    private Animator maleAnimator;
    private Animator femaleAnimator;
    private Animator instructionsAnimator;

    void Awake()
    {
        maleAnimator = maleCharacter.GetComponent<Animator>();
        femaleAnimator = femaleCharacter.GetComponent<Animator>();
        instructionsAnimator = instructionsPanel.GetComponent<Animator>();

        maleAnimator.SetBool("isVisible", true);
        femaleAnimator.SetBool("isVisible", false);
        instructionsAnimator.SetBool("instructionsVisible", false);

        PlayerVars.CharacterModel = selectedCharacter;
    }


    public void toggleCharacter()
    {
        if (selectedCharacter != "male")
        {
            maleAnimator.SetBool("isVisible", true);
            femaleAnimator.SetBool("isVisible", false);
        }
        else
        {
            maleAnimator.SetBool("isVisible", false);
            femaleAnimator.SetBool("isVisible", true);
        }

        selectedCharacter = selectedCharacter == "male" ? "female" : "male";
        PlayerVars.CharacterModel = selectedCharacter;
    }


    public void startGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void toggleInstructions()
    {
        instructionsAnimator.SetBool("instructionsVisible", !instructionsAnimator.GetBool("instructionsVisible"));
    }

    public void exitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("GameScene");
    }
}
