using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Transform[] characters;  // Array to hold references to the characters
    public CameraFollow cameraFollow;  // Reference to the CameraFollow script
    public int currentCharacterIndex = 0;
    public bool canSwitch;
    void Start()
    {
        // Initialize with the first character
        if (canSwitch)
        {
            SwitchToCharacter(0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Switch to the next character
            SwitchToCharacter((currentCharacterIndex + 1) % characters.Length);
        }
    }

    void SwitchToCharacter(int index)
    {
        if (index >= 0 && index < characters.Length)
        {
            // Store the current position
            Vector3 lastPosition = characters[currentCharacterIndex].position;

            // Deactivate the current character
            characters[currentCharacterIndex].gameObject.SetActive(false);

            // Update the current character index
            currentCharacterIndex = index;

            // Set the new character's position to the last position
            characters[currentCharacterIndex].position = lastPosition;

            // Activate the new character
            characters[currentCharacterIndex].gameObject.SetActive(true);

            // Update the camera to follow the new active character
            cameraFollow.target = characters[currentCharacterIndex];
        }
    }
}
