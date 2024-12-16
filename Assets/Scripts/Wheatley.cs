using UnityEngine;

public class Wheatley : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public GameObject enableObject1;
    public GameObject enableObject2;
    public GameObject disableObject;

    void Update()
    {
        if (object1 == null && object2 == null && object3 == null)
        {
            enableObject1.SetActive(true);
            enableObject2.SetActive(true);
            disableObject.SetActive(false);
            
            Destroy(this);
        }
    }
}