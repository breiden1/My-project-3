using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class CardDisplay : MonoBehaviour
{
    [System.Serializable]
    public class ElementSettings
    {
        public Vector2 position;
        public Vector2 size = Vector2.one;
        [Min(1)] public float fontSize = 2f;
        public Color color = Color.black;
        public TextAlignmentOptions alignment = TextAlignmentOptions.Center;
    }

    [Header("Card Data")]
    public CardData cardData;

    [Header("Artwork Settings")]
    public Vector2 artworkPosition = new Vector2(0f, 0.35f);
    public Vector2 artworkSize = new Vector2(0.65f, 0.65f);
    public bool preserveArtworkAspect = true;
    public int artworkSortingOrder = 1;

    [Header("Name Settings")]
    public ElementSettings nameSettings = new ElementSettings
    {
        position = new Vector2(0f, 0.9f),
        size = new Vector2(1.5f, 0.25f),
        fontSize = 0.2f
    };

    [Header("Description Settings")]
    public ElementSettings descriptionSettings = new ElementSettings
    {
        position = new Vector2(0f, -0.35f),
        size = new Vector2(1.5f, 0.6f),
        fontSize = 0.12f,
        alignment = TextAlignmentOptions.TopLeft
    };

    [Header("HP Settings - Top Left Green Box")]
    public ElementSettings hpSettings = new ElementSettings
    {
        position = new Vector2(-0.86f, 1.14f),
        size = new Vector2(0.25f, 0.68f),
        fontSize = 2f,
        color = Color.black,
        alignment = TextAlignmentOptions.Center
    };

    [Header("Cost Settings - Top Right Red Box")]
    public ElementSettings costSettings = new ElementSettings
    {
        position = new Vector2(0.86f, 1.14f),
        size = new Vector2(0.25f, 0.25f),
        fontSize = 2f,
        color = Color.black,
        alignment = TextAlignmentOptions.Center
    };

    [Header("Attack Settings - Bottom Left Red Box")]
    public ElementSettings attackSettings = new ElementSettings
    {
        position = new Vector2(-0.86f, -1.14f),
        size = new Vector2(0.25f, 0.25f),
        fontSize = 2f,
        color = Color.black,
        alignment = TextAlignmentOptions.Center
    };

    [Header("Defense Settings - Bottom Right Blue Box")]
    public ElementSettings defenseSettings = new ElementSettings
    {
        position = new Vector2(0.86f, -1.14f),
        size = new Vector2(1.04f, 0.25f),
        fontSize = 2f,
        color = Color.black,
        alignment = TextAlignmentOptions.Center
    };

    private const string GeneratedName = "Generated Card Display";
    private Transform generatedRoot;
    private SpriteRenderer artworkRenderer;
    private TMP_Text nameText;
    private TMP_Text descriptionText;
    private TMP_Text hpText;
    private TMP_Text costText;
    private TMP_Text attackText;
    private TMP_Text defenseText;

    private void OnEnable()
    {
        RefreshDisplay();
    }

    private void OnValidate()
    {
        if (isActiveAndEnabled)
            RefreshDisplay();
    }

    [ContextMenu("Refresh Card Display")]
    public void RefreshDisplay()
    {
        if (GetComponent<SpriteRenderer>() == null)
            return;

        FindOrCreateElements();
        ApplySettings();
        UpdateCardContent();
    }

    private void FindOrCreateElements()
    {
        if (generatedRoot == null)
        {
            Transform existing = transform.Find(GeneratedName);

            if (existing != null)
                generatedRoot = existing;
            else
            {
                GameObject root = new GameObject(GeneratedName);
                root.transform.SetParent(transform, false);
                generatedRoot = root.transform;
            }
        }

        artworkRenderer = GetOrCreateArtwork();
        nameText = GetOrCreateText("Name", nameText);
        descriptionText = GetOrCreateText("Description", descriptionText);
        hpText = GetOrCreateText("HP", hpText);
        costText = GetOrCreateText("Cost", costText);
        attackText = GetOrCreateText("Attack", attackText);
        defenseText = GetOrCreateText("Defense", defenseText);
    }

    private SpriteRenderer GetOrCreateArtwork()
    {
        Transform existing = generatedRoot.Find("Artwork");
        GameObject artworkObject;

        if (existing != null)
            artworkObject = existing.gameObject;
        else
        {
            artworkObject = new GameObject("Artwork");
            artworkObject.transform.SetParent(generatedRoot, false);
        }

        SpriteRenderer renderer = artworkObject.GetComponent<SpriteRenderer>();

        if (renderer == null)
            renderer = artworkObject.AddComponent<SpriteRenderer>();

        return renderer;
    }

    private TMP_Text GetOrCreateText(string objectName, TMP_Text currentText)
    {
        Transform existing = generatedRoot.Find(objectName);
        GameObject textObject;

        if (existing != null)
            textObject = existing.gameObject;
        else
        {
            textObject = new GameObject(objectName);
            textObject.transform.SetParent(generatedRoot, false);
        }

        TMP_Text text = textObject.GetComponent<TextMeshPro>();

        if (text == null)
            text = textObject.AddComponent<TextMeshPro>();

        text.textWrappingMode = TextWrappingModes.Normal;
        text.overflowMode = TextOverflowModes.Overflow;

        return text;
    }

    private void ApplySettings()
    {
        SetArtworkTransform();
        ApplyTextSettings(nameText, nameSettings);
        ApplyTextSettings(descriptionText, descriptionSettings);
        ApplyTextSettings(hpText, hpSettings);
        ApplyTextSettings(costText, costSettings);
        ApplyTextSettings(attackText, attackSettings);
        ApplyTextSettings(defenseText, defenseSettings);
    }

    private void SetArtworkTransform()
    {
        if (artworkRenderer == null)
            return;

        Transform t = artworkRenderer.transform;
        t.localPosition = new Vector3(artworkPosition.x, artworkPosition.y, -0.1f);
        t.localScale = Vector3.one;

        SpriteRenderer frameRenderer = GetComponent<SpriteRenderer>();
        artworkRenderer.sortingLayerID = frameRenderer.sortingLayerID;
        artworkRenderer.sortingOrder = frameRenderer.sortingOrder + artworkSortingOrder;

        if (cardData == null || cardData.artwork == null)
        {
            artworkRenderer.sprite = null;
            return;
        }

        artworkRenderer.sprite = cardData.artwork;
        Vector2 spriteSize = cardData.artwork.bounds.size;

        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
            return;

        float scaleX = artworkSize.x / spriteSize.x;
        float scaleY = artworkSize.y / spriteSize.y;

        if (preserveArtworkAspect)
        {
            float scale = Mathf.Min(scaleX, scaleY);
            t.localScale = new Vector3(scale, scale, 1f);
        }
        else
            t.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    private void ApplyTextSettings(TMP_Text text, ElementSettings settings)
    {
        if (text == null || settings == null)
            return;

        text.transform.localPosition = new Vector3(settings.position.x, settings.position.y, -0.2f);
        text.rectTransform.sizeDelta = settings.size;
        text.fontSize = settings.fontSize;
        text.color = settings.color;
        text.alignment = settings.alignment;

        SpriteRenderer frameRenderer = GetComponent<SpriteRenderer>();
        Renderer textRenderer = text.GetComponent<Renderer>();

        textRenderer.sortingLayerID = frameRenderer.sortingLayerID;
        textRenderer.sortingOrder = frameRenderer.sortingOrder + 2;
    }

    private void UpdateCardContent()
    {
        if (cardData == null)
            return;

        if (nameText != null)
            nameText.text = cardData.cardName;

        if (descriptionText != null)
            descriptionText.text = cardData.description;

        if (hpText != null)
            hpText.text = cardData.cardType == CardType.Creature ? cardData.hp.ToString() : "";

        if (costText != null)
            costText.text = cardData.sacrificeCost.ToString();

        if (attackText != null)
            attackText.text = cardData.cardType == CardType.Creature ? cardData.attack.ToString() : "";

        if (defenseText != null)
            defenseText.text = cardData.cardType == CardType.Creature ? cardData.defense.ToString() : "";
    }
}