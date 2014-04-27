using UnityEngine;
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

        //Player bonus
        private int currentBonus = 0;
        private int nextBonus = 5;
        private float maxBonusDuration = 4f;
        private float bonusDuration = 4f;
        private bool hasBonus = false;

	void Awake(){
            treasureChests = new List<GameObject>();
            food = new List<GameObject>();
            currentHealth = maxHealth;
	}

	void Update(){
            checkForStun(); 
            checkForBonus();
	}
	
	void FixedUpdate (){
           // Cache the horizontal input.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

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

            if(!hasBonus)                    
                maxSpeed = 1.25f * Mathf.Pow(.85f , getTreasureCount());   

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

        public void checkForBonus(){
            if(currentBonus >= nextBonus){
                //Reset the current bonus, set the bonus state, increase next bonus
                currentBonus = 0;
                hasBonus = true;
                nextBonus += Random.Range(1, 6);
                maxBonusDuration += 1f;
                bonusDuration = maxBonusDuration;
                maxSpeed = maxSpeed * 3;
            }
 
            if(hasBonus){
                if(bonusDuration < 0){
                    hasBonus = false;
                    maxSpeed = maxSpeed  / 3;
                } else {
                    bonusDuration -= Time.deltaTime;

                }
            }
        }

        public void stunPlayer(float duration){
            stunned = true;
            stunDuration = duration;
        }

        public void pickupCollectible(GameObject collectible){
            if (collectible.tag == "Treasure"){
                treasureChests.Add(collectible);
                currentBonus += 1; 
            } 
            if (collectible.tag == "Food"){
                food.Add(collectible);
            }

        }
        public int getTreasureCount(){
            return treasureChests.Count;
        }


	public void dropOffCollectibles() {
            if (getTreasureCount() > 0) {
                this.score += getTreasureCount();
                foreach(GameObject treasure in treasureChests){
                    Destroy(treasure);
                }
                treasureChests.RemoveAll(delegate(GameObject treasure){
                    return treasure.tag == "Treasure";
                
                });
            }
	}

        public int getCurrentBonusCount(){
            return currentBonus;
        }

        public int getNextBonusRequirement(){
            return nextBonus;
        }

        public void takeDamage(int damage){
            currentHealth -= damage;
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
}
