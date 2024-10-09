using UnityEngine;

public class theaterTest : MonoBehaviour
{
    public int age;
    public bool hasParent;
    public bool hasInfant;
    // Start is called before the first frame update
    void Awake()
    {
        if (age >= 18 && !hasInfant || (hasParent && age >= 5)) Debug.Log("G, PG, PG-13, R");
        else if (age >= 13 && !hasInfant || (hasParent && age >= 5)) Debug.Log("G, PG, PG-13");
        else if (age >= 5) Debug.Log("G, PG");
        else Debug.Log("G");
    }
}