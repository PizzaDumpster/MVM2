using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public ObjectStringSO playerString;
    public GameObject player;


    public void Awake()
    {
    MessageBuffer<SetPlayer>.Subscribe(CharacterSet);
         
    }


    private void CharacterSet(SetPlayer obj)
    {
        player = obj.player;
    }

void Update()
    {


        if (player != null)
        {
            // Calculate direction vector towards the player
            Vector3 direction = player.transform.position - transform.position;

            // Flip the object if player is on the left
            if (direction.x < 0)
            {
                // Face left (flip along X-axis)
                transform.localScale = new Vector3(-1, 1, 1);
            }
            // Flip the object if player is on the right
            else
            {
                // Face right (set back to original scale)
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
