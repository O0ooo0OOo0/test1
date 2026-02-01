using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    private PlayerMove PlayerMove;

    void Start()
    {
        PlayerMove = GetComponent<PlayerMove>();//ÒýÓÃ½Å±¾

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerMove.jumpForce);
    }
}
