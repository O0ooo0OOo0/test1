using UnityEngine;

public class playeranim : MonoBehaviour
{
    private enum Anim { idle, run, jump, fall };//枚举
    private Anim state;
    private Animator anim;

    private PlayerMove playerMove;
    private playerjump playerjump;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerjump = GetComponent<playerjump>();

    }


    void Update()
    {
        if (playerMove.xInput != 0)
        {
            state = Anim.run;
        }
        else
        {
            state = Anim.idle;
        }

        if (Input.GetButton("Jump"))
        {
            state = Anim.jump;
        }
        anim.SetInteger("states", (int)state);//强制转换成int

    }
}
