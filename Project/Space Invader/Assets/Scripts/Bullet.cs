using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{

    public float speed = 30;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If Bullet hits a wall destroy bullet
        if (col.tag == "Wall"){
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Alien") {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
            

            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            Destroy(gameObject);
            
            DestroyObject(col.gameObject);
            IncreaseTextUIScore();
        }

        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            DestroyObject(col.gameObject);
        }

    }


    void OnBecameInvisible() {
        Destroy(gameObject);
    }
    void IncreaseTextUIScore()
    {
        
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUIComp.text);
        score = score + 10;
        textUIComp.text = score.ToString();



        if (score == 190)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

    }

}
