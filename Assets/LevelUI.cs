using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    public void SetLevel(int level)
    {
        levelText.text = "N° Livello " + level;
    }
}
