using UnityEngine;

public class PlayerDesignationController : MonoBehaviour, IEntityCompoInit
{

    [SerializeField] private DesignationUI designationUI;

    private DesignationDataSO _currentDesignation;
    private Player _player;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
    }

    public void SetDesignation(DesignationDataSO designationData)
    {
        //���� Īȣ�� ���� �Ѵٸ�?
        if(_currentDesignation != null)
        {
            _currentDesignation.UnEffectDesignation(_player);
            _currentDesignation = null;
        }

        if(designationData == null)
        {
            //Null���� ������
            designationUI.SetDesignation(null);
            _currentDesignation = null;
        }

        _currentDesignation = designationData;
        designationUI.SetDesignation(designationData);
        _currentDesignation.EffectDesignation(_player);
    }
}
