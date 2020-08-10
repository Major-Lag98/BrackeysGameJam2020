using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Apply directly onto a Text UI object to apply typewriter effect
/// </summary>
[DisallowMultipleComponent]
public class TextWriter : MonoBehaviour
{
    public bool UseTrigger;
    public TriggerArea Trigger;
    public float StartDelay;

    [SerializeField] private float timePerCharacter;

    private TextMeshProUGUI textMesh; // Our text mesh

    private string text; // Our cached text
    private int currIndex;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();

        text = textMesh.text; // cache our text
        textMesh.text = ""; // Clear the text content of the object

        if (!UseTrigger)
            StartWriting(StartDelay);
        else
            Trigger.OnTriggerEnter += _ => StartWriting(StartDelay);
    }

    /// <summary>
    /// Called to start our text effect object
    /// </summary>
    public void StartWriting(float startDelay)
    {
        StartCoroutine(WriteText(startDelay));
    }

    private int SkipTokens(string text, int start)
    {
        if (text[currIndex] == '<')
            return text.IndexOf('>', start);

        return start;
    }

    private IEnumerator WriteText(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        var size = text.Length;
        while (currIndex < size)
        {
            currIndex = SkipTokens(text, currIndex); // Skip tokens like <color></color>

            var newText = text.Substring(0, currIndex+1); // Get the text we need to display
            textMesh.text = newText; // Set it
            currIndex++; //Increment
            yield return new WaitForSeconds(timePerCharacter); // Wait for our desired time
        }
        yield break;
    }
}
