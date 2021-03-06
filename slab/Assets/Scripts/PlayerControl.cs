﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public int treasureCount = 0;
	[HideInInspector]
	public int score = 0;

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public int maxHealth = 100;

	private bool stunned = false;
	private float stunDuration;
	private float oldMass = 0;
	private List<GameObject> treasureChests;
	private List<GameObject> food;
	private int currentHealth;
	private int progress;
	private float timer = 1.0f;


	void Start() {
	}

	void Awake(){
            treasureChests = new List<GameObject>();
            food = new List<GameObject>();
            currentHealth = maxHealth;
	}

	void Update() {
            checkForDeath();
            checkForStun(); 
            //loseHealth();
	}
	
	void FixedUpdate (){
           // Cache the horizontal input.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
			bool facingLeft = !facingRight;

            //Handle being stunned
            if(stunned){
                h = 0f;
                v = 0f;
                if(oldMass == 0){
                    oldMass = rigidbody2D.mass;
                } else {
                    rigidbody2D.mass = 100;
                }
            }

                                
            maxSpeed = 1.25f + getTreasureCount() / 5f;
			if (maxSpeed > 2.5f)
				maxSpeed = 2.5f;

            // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            if(h * rigidbody2D.velocity.x < maxSpeed) {
                // ... add a force to the player
                rigidbody2D.AddForce(Vector2.right * h * moveForce);
            }

            if(v * rigidbody2D.velocity.y < maxSpeed) {
                // ... add a force to the player
                rigidbody2D.AddForce(Vector2.up * v * moveForce);
            }   

            // If the player's horizontal velocity is greater than the maxSpeed...
            if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
                // ... set the player's velocity to the maxSpeed in the x axis.
                rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
            }

            if(Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed) {
                // ... set the player's velocity to the maxSpeed in the y axis.
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed);
            }
            // If the input is moving the player right and the player is facing left...
            if(h > 0 && !facingRight) {
                FlipPlayer();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if(h < 0 && facingRight) {
                FlipPlayer();
            }
	}

	void FlipPlayer () {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
	}

	void checkForDeath(){
            if(currentHealth <= 0) {
                currentHealth = 100;
                Messenger.Broadcast("respawn player");
            }
	}

	public void checkForStun(){
	    if(stunned) {
	        if(stunDuration < 0){
	            stunned = false;
	            rigidbody2D.mass = oldMass;
	            oldMass = 0;
	        } else {
	            stunDuration -= Time.deltaTime;
	        }
	    }
	}

	public void loseHealth() {
		timer -= Time.deltaTime;
		if (timer < 0) {
			currentHealth--;
			timer = 1.0f;
			Debug.Log(currentHealth);
		}
	}

	public void stunPlayer(float duration){
	    stunned = true;
	    stunDuration = duration;
	}
	public void pickupCollectible(GameObject collectible){
	    if (collectible.tag == "Treasure"){
	        Messenger.Broadcast("pickup treasure");
			if(! collectible.GetComponent<CollectableCollision>().isFishy()){
	        	treasureChests.Add(collectible);
			}
	    } 
	    if (collectible.tag == "Food"){
	        food.Add(collectible);
	    }
	}
	public int getTreasureCount(){
	    return treasureChests.Count;
	}

	public void dropOffCollectibles() {
		int treasureCount = getTreasureCount ();
		if (treasureCount > 0) {
			this.score += treasureCount*100 + (int)Mathf.Pow(treasureCount-1, 2)*10;
                foreach(GameObject treasure in treasureChests){
					progress += 1;
                    Destroy(treasure);
                }
                treasureChests.RemoveAll(delegate(GameObject treasure){
                    return treasure.tag == "Treasure";
                
                });
			 
    	}
	}

	public void takeDamage(int damage){
	    	Messenger.Broadcast("take damage");
			int counter = 0;
			foreach(GameObject duck in treasureChests){
				if(counter == damage)
					break;
	
				CollectableCollision duckScript = duck.GetComponent<CollectableCollision>();
				duckScript.Die();
				counter++;
			}
			treasureChests.RemoveAll(delegate(GameObject duck){
				return duck.GetComponent<CollectableCollision>().isDead();
			});
	    //currentHealth -= damage;
	}
	
	public void resetHealth(){
	    currentHealth = maxHealth;
	}
	
	public int getCurrentHealth(){
	    return currentHealth;
	}
	
	public int getScore(){
	    return score;
	}

	public int getProgress(){
		return progress;
	}

	public void resetProgress(){
		progress = 0;
	}
}
