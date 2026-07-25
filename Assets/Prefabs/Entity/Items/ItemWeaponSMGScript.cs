using UnityEngine;

public class ItemWeaponSMGScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject Player = collision.gameObject;
        if (Input.GetAxisRaw("WeaponPickup") > 0 && PlayerScript.Game_player.GetCurrectWeapon() == "none")
        {

            if (Player.CompareTag("Player"))
            {
                Player.GetComponent<PlayerScript>().SetWeapon("Wep_SMG");
                Destroy(gameObject);
            }
        }

    }
}
