using TMPro;
using UnityEngine;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;

    public void SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            playerNameText.text = string.Empty;
            return;
        }

        playerNameText.text = name;
    }
}
