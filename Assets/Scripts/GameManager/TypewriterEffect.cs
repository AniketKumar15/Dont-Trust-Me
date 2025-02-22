using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text dialogueText; // Assign this in the Inspector
    public float typingSpeed = 0.05f; // Speed of typing
    public float linePause = 1.5f; // Pause after each line

    private string[] dialogueLines = {
        "Hello, Player. It's me, Koko.",
        "Listen carefully—I have a secret to tell you.",
        "The developer gives hints throughout the game...",
        "But there's a catch—every hint is a lie.",
        "They are meant to trick you.",
        "If you want to pass the levels, do the opposite of what the hints say.",
        "Oh, and one more thing...",
        "Don't tell anyone I told you this.",
        "It's our little secret."
    };

    void Start()
    {
        // Check if the cutscene has been watched before
        if (PlayerPrefs.GetInt("CutsceneWatched", 0) == 1)
        {
            SkipCutscene();
            return;
        }

        dialogueText.text = ""; // Clear text at start
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue()
    {
        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(linePause); // Pause before next line
            dialogueText.text = ""; // Clear text before new line

        }
        // Mark cutscene as watched and load the next scene
        PlayerPrefs.SetInt("CutsceneWatched", 1);
        PlayerPrefs.Save();

        LoadNextScene();
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = ""; // Clear text before typing
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter; // Add letter one by one
            AudioManager.instance.Play("typeSound");
            yield return new WaitForSeconds(typingSpeed); // Wait before next letter
        }
    }
    void SkipCutscene()
    {
        LoadNextScene(); // Directly load the next level
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
