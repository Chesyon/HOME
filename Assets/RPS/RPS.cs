using UnityEngine;
using TMPro;

public class RPS : MonoBehaviour
{
    int compChoice;
    int playerChoice;
    public string[] choiceNames;
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI outputField;

    bool WinCheck() // i originally used (playerChoice + 1) % 3 == compChoice, but when switching from Rock Scissors Paper to Rock Paper Scissors, it kinda messed that up. so now we have this.
    {
        int beatsNum;
        if (playerChoice == 0) beatsNum = 2;
        else beatsNum = playerChoice - 1;

        if (beatsNum == compChoice) return true;
        else return false;
    }

    public void Go()
    {
        compChoice = Random.Range(0, 3); // determine comp choice
        // determine winner
        if (playerChoice == compChoice) Output("Tie!");
        else if (WinCheck()) Output("Win!");
        else Output("Lose!");
    }

    void Output(string result)
    {
        string newText = "Player choice: " + choiceNames[playerChoice] + "\nComp Choice: " + choiceNames[compChoice] + "\n" + result;
        outputField.text = newText;
    }

    public void UpdatePlayerChoice()
    {
        playerChoice = dropdown.value;
    }
}
