using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    public float speed = 10;

    public Rigidbody2D rigidBody;
    
    public Sprite startingImage;

    public Sprite altImage;

    private SpriteRenderer spriteRenderer;

    public float secBeforeSpriteChange = 0.5f;

    public GameObject alienBullet;

    public float minFireRateTime = 0.0f;

    public float maxFireRateTime = 0.0f;

    public float baseFireWaitTime = 0.0f;

    public Sprite explodedShipImage;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = new Vector2(1, 0) * speed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(changeAlienSprite());

        baseFireWaitTime = baseFireWaitTime +
            Random.Range(minFireRateTime, maxFireRateTime);

    }

    void Turn(int direction){
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;
        rigidBody.velocity = newVelocity;
    }

    void MoveDown(){
        Vector2 position = transform.position;
        position.y -= 2;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }
        if (col.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }

        if (col.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
            
            Destroy(gameObject);
           
        }


    }

    public IEnumerator changeAlienSprite(){
        while (true) {
            if (spriteRenderer.sprite == startingImage)
            {
                spriteRenderer.sprite = altImage;
                //SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);
            }
            else
            {
                spriteRenderer.sprite = startingImage;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);
            }

            yield return new WaitForSeconds(secBeforeSpriteChange);
        }
    }

    void FixedUpdate() {
        
        if (Time.time > baseFireWaitTime)
        {

            baseFireWaitTime = baseFireWaitTime +
                Random.Range(minFireRateTime, maxFireRateTime);

            
            Instantiate(alienBullet, transform.position, Quaternion.identity);
           
        }
       

    }

    void OnTriggerEnter2D(Collider2D col){

        if (col.gameObject.tag == "Player")
        {
            // Play exploding ship sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);

            // Change to exploded ship image
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            // Destroy AlienBullet
            Destroy(gameObject);

            // Wait .5 seconds and then destroy Player

            DestroyObject(col.gameObject, 0.5f);
        }

    }

   
}
