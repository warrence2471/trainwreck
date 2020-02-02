using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mainMenuBehaviour : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public GameObject instructionsPanel;
    public GameObject textInput;
    public GameObject trafficLightsObject;
    [SerializeField]
    private string selectedCharacter = "male";
    private Animator maleAnimator;
    private Animator femaleAnimator;
    private Animator instructionsAnimator;
    private trafficLightController trafficLights;

    void Awake()
    {
        maleAnimator = maleCharacter.GetComponent<Animator>();
        femaleAnimator = femaleCharacter.GetComponent<Animator>();
        instructionsAnimator = instructionsPanel.GetComponent<Animator>();
        trafficLights = trafficLightsObject.GetComponent<trafficLightController>();

        maleAnimator.SetBool("isVisible", true);
        femaleAnimator.SetBool("isVisible", false);
        instructionsAnimator.SetBool("instructionsVisible", false);

        PlayerVars.CharacterModel = selectedCharacter;

        trafficLights.toggleLights();
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

    public void setName()
    {
        var name = textInput.GetComponent<TMP_InputField>().text;
        PlayerVars.Name = name;
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
