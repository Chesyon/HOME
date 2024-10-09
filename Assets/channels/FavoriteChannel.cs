using UnityEngine;
using System.Linq; // needed for AddToArray
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class FavoriteChannel : MonoBehaviour
{
    int selectedChannel;
    public int[] favorites;
    public TMP_InputField inputText;
    public TextMeshProUGUI outputText;
    public Image channelImage;
    public Sprite[] channelImgs;
    public string[] channelNames;

    public void AddToFavorites()
    {
        if (!ChannelIsValid(selectedChannel)) Output("Invalid channel.");
        else if (!favorites.Contains(selectedChannel)) // We don't want multiple instances of the same channel on our list, so we make sure it's not already there before adding it.
        {
            favorites = AddToArray(favorites, selectedChannel);
            Output(channelNames[selectedChannel] + " has been added to your favorites.");
        }
        else
        {
            Output(channelNames[selectedChannel] + " is already in your favorites.");
        }
    }

    int[] AddToArray(int[] arr, int val) // Arrays do not support appending. This function converts the array to a list, adds the new value to the list, converts back to an array, and returns the new array.
    {
        List<int> list = arr.ToList();
        list.Add(val);
        return list.ToArray();
    }

    public void UpdateSelectedChannel()
    {
        try { selectedChannel = int.Parse(inputText.text); } // Updates selectedChannel to the number in the input field. If the text is invalid (non-int), set the selected channel to 0.
        catch {
            selectedChannel = 0;
            inputText.text = "0";
        }
        // Update the channel image.
        if (ChannelIsValid(selectedChannel)) // Set the image to the one for the matching channel.
        {
            channelImage.sprite = channelImgs[selectedChannel];
            channelImage.color = new Color(1, 1, 1, 1); // set opacity to 1 so it's not hidden
        }
        else // if the channel is invalid, hide the image.
        {
            channelImage.sprite = null; // redundant?
            channelImage.color = new Color(1, 1, 1, 0); // hide the image
        }
    }

    void Output(string newText) { // shorthand for updating the UI text
        outputText.text = newText;
    }

    bool ChannelIsValid(int channelNum) // Channel number must be greater than 0 is less than the length of the array to be valid.
    {
        if (channelNum <= 0 || channelNum > channelNames.Length-1) return false;
        else return true;
    }

    public void DisplayList() // add every channel in the favorites array together to create one bullet pointed string
    {
        string output = "";
        foreach (int channelNum in favorites)
        {
            output += "• " + channelNames[channelNum];
            output += "\n";
        }
        Output(output);
    }
}
