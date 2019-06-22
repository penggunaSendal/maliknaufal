using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBullet : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float speed = 30;

    public Sprite explodedShipImage;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        
        rigidBody.velocity = Vector2.down * speed;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

       
        if (col.gameObject.tag == "Player") {
            
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);

          
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);

           
            DestroyObject(col.gameObject, 0.5f);

            



        }

        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            DestroyObject(col.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
