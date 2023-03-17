using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueTextBox : MonoBehaviour
{
    [SerializeField]
    private UnityEvent response;

    [SerializeField]
    private TextMeshProUGUI textComponent;

    [SerializeField]
    private string[] lines;

    [SerializeField]
    private float textSpeed;

    private int index;

    private void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;

            StartCoroutine(TypeLine());
        }
        else
        {
            //logic when the text ends
            response.Invoke();
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
