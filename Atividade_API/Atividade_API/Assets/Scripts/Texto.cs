using NUnit.Framework.Constraints;
using UnityEngine;

public class Texto : MonoBehaviour
{
    public float timer = 1.3f;
    private void OnEnable()
    {
        timer = 1.3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
