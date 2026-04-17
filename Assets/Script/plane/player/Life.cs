using UnityEngine;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public  Vector2 pos;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("die");
        
        rb.bodyType = RigidbodyType2D.Static;
        Restart();
    }

   
    private void Restart()
    {
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.position = pos ;
        rb.bodyType = RigidbodyType2D.Dynamic ;//变回运动状态
    }
}
