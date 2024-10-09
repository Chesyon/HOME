using UnityEngine;
using TMPro;

public class RPS : MonoBehaviour
{
    int CompChoice;
    int playerChoice;
    public TMP_InputField playerInput;
    public TextMeshProUGUI outputField;
    public void Go()
    {
        // interpret player input
        switch (playerInput.text.ToLower())
        {
            case "rock":
                playerChoice = 0;
                break;
            case "scissors":
                playerChoice = 1;
                break;
            case "paper":
                playerChoice = 2;
                break;
            default:
                playerChoice = -1;
                break;
        }
        CompChoice = Random.Range(0, 3); // determine comp choice
        // determine winner
        if (playerChoice == -1) Output("Invalid input");
        else
        {
            if (playerChoice == CompChoice) Output("Tie");
            else if ((playerChoice + 1) % 3 == CompChoice) Output("Win");
            else Output("Lose");
        }

    }
    public void Output(string newText)
    {
        outputField.text = newText;
    }
}
