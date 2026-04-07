using UnityEngine;

public class BackGround : MonoBehaviour//分层卷动
{

    public Transform target;//玩家位置
    public Transform MidBackGround, FarBackGround, NearBackGround;
    private Vector2 LastPos;//上一个相机位置


    void Start()
    {
        LastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector2 distance = new Vector2 (transform.position.x - LastPos.x,transform.position.y - LastPos.y);//位移差
        
        FarBackGround.position += new Vector3(distance.x * 1.0f, distance.y * 1.0f, 0f);
        MidBackGround.position += new Vector3(distance.x * 0.7f, distance.y * 0.7f, 0f);
        NearBackGround.position += new Vector3(distance.x * 0.3f, distance.y * 0.3f, 0f);

        LastPos = transform.position;
    }
}
