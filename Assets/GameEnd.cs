using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {
    public string dead_reason = "Some Buggy Reason";
    public static bool gameEnd = false;
    private static int final_score;
    private static Text display_reason;
    private static Text display_score;
    private static Canvas end_display;
    private static GameObject character;

    public AudioClip source;
    AudioSource end;

    void Start()
    {
        end_display = GameObject.FindWithTag("end_text").GetComponent<Canvas>();
        end_display.enabled = false;
        character = GameObject.FindWithTag("avatar");
        display_reason = GameObject.FindWithTag("end_reason").GetComponent<Text>();
        display_score = GameObject.FindWithTag("end_score").GetComponent<Text>();
        final_score = PlayerController.scoreCount;
       
    }

	// Update is called once per frame
	void OnCollisionEnter(Collision c) {
        if(c.gameObject.CompareTag("avatar"))
        {
            print("hit");
            end = GetComponent<AudioSource>();
            if (end != null)
                end.Play();
            display_reason.text = dead_reason;
            display_score.text = final_score.ToString();
            end_display.enabled = true;
            gameEnd = true;
            //if(Input.GetButtonDown("Restart"))
            //{
            StartCoroutine(restart());
            //}
        } 
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameEnd.gameEnd = false;
    }

    public void onClick()
    {
        while (Input.GetKeyDown(KeyCode.R))
        {
            print("restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            break;
        }
    }
}
