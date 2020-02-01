﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuBehaviour : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    [SerializeField]
    private string selectedCharacter = "female";
    private Animator maleAnimator;
    private Animator femaleAnimator;

    void Awake()
    {
        maleAnimator = maleCharacter.GetComponent<Animator>();
        femaleAnimator = femaleCharacter.GetComponent<Animator>();

        maleAnimator.SetBool("isVisible", true);
        femaleAnimator.SetBool("isVisible", false);
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
    }


    public void startGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("GameScene");
    }
}
