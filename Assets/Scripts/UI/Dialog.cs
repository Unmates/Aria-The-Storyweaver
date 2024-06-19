using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public struct DialogLine
    {
        public string text;
        public Sprite characterName;
        public Sprite characterImage;
    }

    [SerializeField] GameObject blackBg;
    public TextMeshProUGUI textComponent;
    public Image nameComponent; // Add this in the inspector
    public Image characterImageComponent; // Add this in the inspector


    public DialogLine[] lines;
    public float textSpeed;

    private int index;

    [SerializeField] GameObject playerObj;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
        {
            if (textComponent.text == lines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].text;
            }
        }
    }

    public void StartDialog()
    {
        health = playerObj.GetComponent<Health>();
        health.DisableControll();
        gameObject.SetActive(true);
        blackBg.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        DialogLine currentLine = lines[index];
        textComponent.text = string.Empty;
        nameComponent.sprite = currentLine.characterName;
        characterImageComponent.sprite = currentLine.characterImage;

        AdjustImageSize(currentLine.characterImage);

        foreach (char c in currentLine.text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        health = playerObj.GetComponent<Health>();
        health.EnableControll();
        gameObject.SetActive(false);
        blackBg.SetActive(false);
    }

    void AdjustImageSize(Sprite sprite)
    {
        if (sprite != null)
        {
            // Get the original size of the sprite
            Vector2 spriteSize = sprite.bounds.size;

            // Get the RectTransform of the character image UI element
            RectTransform rectTransform = characterImageComponent.GetComponent<RectTransform>();

            // Adjust the size of the RectTransform to match the sprite size
            rectTransform.sizeDelta = new Vector2(spriteSize.x * 100f, spriteSize.y * 100f);
        }
    }
}