using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController != null) {
            if(rubyController.Health < rubyController.maxHealth) {
                rubyController.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
