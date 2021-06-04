using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -4)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
      {
        Destroy(this.gameObject);
      }
      else if (other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    
        
    }
}
