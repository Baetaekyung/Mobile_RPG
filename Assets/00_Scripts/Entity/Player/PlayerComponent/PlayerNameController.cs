using UnityEngine;

public class PlayerNameController : MonoBehaviour, IEntityCompo
{
    [SerializeField] private PlayerNameUI playerNameUI;

    public void SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            playerNameUI.SetName(null);

            return;
        }

        playerNameUI.SetName(name);
    }
}
