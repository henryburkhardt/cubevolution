using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour
{

    public float timer;
    public int newtarget;
    public float speed; 
    public NavMeshAgent nav; 
    public Vector3 Target;
    public float food;
    public float size;
    public GameObject FoodCreator;
    //public float colideRadius;

    // Start is called before the first frame update
    void Start()
    {
        food = 0;
        size = 2;
        gameObject.transform.localScale = new Vector3(size,size,size);
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 20;
        nav.radius = 0.75f;
        newtarget = 2;
        transform.position = new Vector3(Random.Range(-20,20),0,Random.Range(-20,20));
        FoodCreator = GameObject.Find("FoodCreator");
        
    }

    // Update is called once per frame
    void Update()
    {
        

        findFood(4);
        checkColideChar(size);
         timer += Time.deltaTime;

            if (timer >= newtarget){
                newTarget();
                size = size - 0.1f;
                gameObject.transform.localScale = new Vector3(size,size,size);
                timer = 0;
                if (size < 0.1f){
                    Destroy(gameObject);
                }
        }
        if(FoodCreator.transform.childCount > 1){
                //print("Food Remaining:" + FoodCreator.transform.childCount);
            } else{
                //print("Food Remaining: 0, cannibalism inititated");
            } 

        
        
    }

    void newTarget(){
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float xPos = Random.Range(-40,40);
        float zPos = Random.Range(-40,40);

        Target = new Vector3(xPos,gameObject.transform.position.y,zPos);

        nav.SetDestination(Target);

    }

    void findFood(float radius){
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position,radius);
             foreach (var hitCollider in hitColliders){
                if(hitCollider.gameObject.tag == "Food"){
                    nav.SetDestination(hitCollider.gameObject.transform.position);
                    Debug.DrawRay(transform.position, (hitCollider.gameObject.transform.position - transform.position), Color.green);
            }
        }   
    }

    
    void checkColideChar(float radius){
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position,radius);
             foreach (var hitCollider in hitColliders){
                if((hitCollider.gameObject.tag == "Charecter")&&(hitCollider.gameObject.transform.position != transform.position)){
                    Debug.DrawRay(transform.position, (hitCollider.gameObject.transform.position - transform.position), Color.red);
                    if (FoodCreator.transform.childCount < 1){
                        float otherSize = hitCollider.gameObject.GetComponent<MoveRandomly>().size;
                        //nav.SetDestination(hitCollider.gameObject.transform.position);
                        if(size > otherSize){
                            Destroy(hitCollider.gameObject);
                            size = size + (0.2f*otherSize);
                            gameObject.transform.localScale = new Vector3(size,size,size);  

                        }
                         if(size < otherSize){
                            Destroy(gameObject);

                        }
                    }
            }
        }   
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Food"){
            food = food + 1;
            nav.speed = nav.speed + food;
            size = size + 0.05f;
            gameObject.transform.localScale = new Vector3(size,size,size); 
            
            
        }
    }

     private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (transform.position, (size));
    }
}
