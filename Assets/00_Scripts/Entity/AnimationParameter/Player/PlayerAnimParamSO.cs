using UnityEngine;

[CreateAssetMenu(menuName = "SO/Animation/Player param")]
public class PlayerAnimParamSO : AnimParamSO
{
    [SerializeField] private EPlayerStateEnum stateEnum;

    public EPlayerStateEnum GetStateEnum => stateEnum;
}
