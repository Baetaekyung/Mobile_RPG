using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesignationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI designation;
    [SerializeField] private Image designationBackground;

    public void SetDesignation(DesignationDataSO designationData)
    {
        if(designationData == null)
        {
            designation.text = string.Empty;
            designationBackground.color = Color.clear;

            return;
        }

        designation.text = designationData.designationName;
        designationBackground.sprite = designationData.designationSprite;
        //지금은 안보여서 색깔은 검정으로
        designationBackground.color = Color.black;
    }
}
