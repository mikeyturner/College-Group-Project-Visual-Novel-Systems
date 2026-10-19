using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject DialoguePanel;
    public TMP_Text CharacterNameText;
    public TMP_Text DialogueText;
    public Button NextButton;
    public GameObject ChoiceContainer;              // The container holding choice buttons
    public GameObject ChoiceButtonTemplate;         // The template button
    public Image CharacterImage;

    private DialogueLineSO CurrentLine;
    private List<GameObject> ActiveChoiceButtons = new List<GameObject>(); // Tracks spawned buttons for cleanup

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        NextButton.onClick.AddListener(Advance);
        DialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueLineSO FirstLine)
    {
        CurrentLine = FirstLine;
        DialoguePanel.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        if (CurrentLine == null)
        {
            EndDialogue();
            return;
        }

        bool ConditionsPassed = true;
        foreach (var condition in CurrentLine.Conditions)
        {
            if (!condition.Evaluate())
            {
                ConditionsPassed = false;
                break;
            }
        }

        if (!ConditionsPassed)
        {
            // TODO: Replace with branching system later
            CurrentLine = CurrentLine.Next;
            ShowLine();
            return;
        }

        // Display character
        if (CurrentLine.Character != null)
        {
            CharacterNameText.text = CurrentLine.Character.CharacterName;
            CharacterImage.sprite = CurrentLine.Character.DefaultSprite;
            CharacterImage.gameObject.SetActive(true);
        } else
        {
            CharacterImage.gameObject.SetActive(false);
        }

        if (CurrentLine.Triggers.Count > 0)
        {
            TriggerManager.Instance.Execute(CurrentLine.Triggers);
        }

        DialogueText.text = CurrentLine.Text;

        // Check if this line has choices
        if (CurrentLine.Choices.Count > 0)
        {
            ShowChoices();
        } else
        {
            ShowNextButton();
        }
    }

    void ShowChoices()
    {
        // Hide next button, show choices instead
        NextButton.gameObject.SetActive(false);
        ChoiceContainer.SetActive(true);

        // Clean up any old choice buttons from last time
        foreach (var button in ActiveChoiceButtons)
        {
            Destroy(button);
        }
        ActiveChoiceButtons.Clear();

        // Spawn a button for each valid choice
        foreach (var choice in CurrentLine.Choices)
        {
            // Check conditions on this choice
            bool ChoiceConditionsPassed = true;
            foreach (var condition in choice.Conditions)
            {
                if (!condition.Evaluate())
                {
                    ChoiceConditionsPassed = false;
                    break;
                }
            }

            // Skip this choice if conditions failed
            if (!ChoiceConditionsPassed) continue;

            // Spawn a new button from the template
            GameObject NewButton = Instantiate(ChoiceButtonTemplate, ChoiceContainer.transform);
            NewButton.SetActive(true);

            // Set the button text
            NewButton.GetComponentInChildren<TMP_Text>().text = choice.ChoiceText;

            // When clicked, pick this choice
            NewButton.GetComponent<Button>().onClick.AddListener(() => PickChoice(choice));

            ActiveChoiceButtons.Add(NewButton);
        }
    }

    void PickChoice(ChoiceSO Choice)
    {
        CurrentLine = Choice.NextLine;
        ShowLine();

        // Fire triggers after ShowLine so they overwrite the default sprite
        if (Choice.Triggers.Count > 0)
        {
            TriggerManager.Instance.Execute(Choice.Triggers);
        }
    }

    void ShowNextButton()
    {
        NextButton.gameObject.SetActive(true);
        ChoiceContainer.SetActive(false);
    }
    public void SetExpression(Sprite Expression)
    {
        CharacterImage.sprite = Expression;
    }
    void Advance()
    {
        CurrentLine = CurrentLine.Next;
        ShowLine();
    }

    void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        CurrentLine = null;
    }
}