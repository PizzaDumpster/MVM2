using UnityEngine;

[System.Serializable]
public enum PowerUp
{
    DoubleJump,
    Dash,
    WallClimb,
    WallSlide,
    WallJump
}

[CreateAssetMenu(fileName = "PowerUpSO", menuName = "PowerUp")]
public class PowerUpSO : ScriptableObject
{
    public string PowerUpName;
    public PowerUp powerUp;
}