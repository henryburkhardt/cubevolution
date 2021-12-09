using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateFood : MonoBehaviour


{

    
    public float timer;
    public int newTarget;

    // Start is called before the first frame update
    void Start(){
        

        


        generateFood(500);
    }

    // Update is called once per frame
    void Update()
    {
        float numFood = gameObject.transform.childCount;
        timer += Time.deltaTime;
        if ((timer >= 10)){
            generateFood(2);
            print("newFood Generated");
            timer = 0;
        }
    }

    void generateFood(int quantity) {
        Material food = Resources.Load("Food", typeof(Material)) as Material;
        GameObject sphere;
        for (int i = 1; i <= quantity; i++){
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(Random.Range(-20,20),1,Random.Range(-20,20));
            sphere.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            sphere.GetComponent<Renderer>().material = food;
            sphere.AddComponent<FoodAction>();
            sphere.tag = "Food";
            sphere.name = "Food" + i;
            sphere.transform.SetParent(gameObject.transform);
            Rigidbody rb = sphere.AddComponent<Rigidbody>();
            rb.mass = 1;
        }

    }
}
