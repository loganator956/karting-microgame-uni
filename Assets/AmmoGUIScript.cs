using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoGUIScript : MonoBehaviour
{
    WeaponFire playerWeapon;
    Text text;

    void Awake()
    {
        playerWeapon = FindObjectOfType<WeaponFire>();
        text = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Ammo: {playerWeapon.ammo}";
    }
}
