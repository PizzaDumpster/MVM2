using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapon/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string WeaponName;
    public int WeaponDamage;
    public Sprite WeaponImage;
    public AudioClip WeaponSFX;
}