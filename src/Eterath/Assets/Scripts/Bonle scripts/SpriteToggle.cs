using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include to work with UI components like Image and Text

public class SpriteToggle : MonoBehaviour
{
    // Sprite variables
    public Sprite unlitSprite;
    public Sprite litSprite;

    // Color variables
    public Color unlitColor = Color.white; // Default UNLIT color
    public Color litColor = Color.yellow; // Default LIT color

    // UI components
    public Text textField; // Assign this in the Unity Editor

    // Key variable
    public KeyCode toggleKey = KeyCode.Space; // Adjustable via the Inspector

    private Image imageComponent;

    void Start()
    {
        // Get the Image component attached to this GameObject
        imageComponent = GetComponent<Image>();
        // Set the default sprite and text color to unlit
        imageComponent.sprite = unlitSprite;
        if (textField != null) // Check if a text field is assigned
        {
            textField.color = unlitColor;
        }
    }

    void Update()
    {
        // Check if the toggle key is pressed down
        if (Input.GetKeyDown(toggleKey))
        {
            // Change the sprite to lit and text color to LIT color
            imageComponent.sprite = litSprite;
            if (textField != null) // Ensure the text field is assigned
            {
                textField.color = litColor;
            }
        }
        // Check if the toggle key is released
        else if (Input.GetKeyUp(toggleKey))
        {
            // Change the sprite back to unlit and text color to UNLIT color
            imageComponent.sprite = unlitSprite;
            if (textField != null) // Ensure the text field is assigned
            {
                textField.color = unlitColor;
            }
        }
    }
}



