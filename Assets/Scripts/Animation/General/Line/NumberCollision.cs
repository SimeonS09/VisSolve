using UnityEngine;
using TMPro;

public class NumberCollision : MonoBehaviour
{
    public NumberPosition number;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NumberCollider"))
        {
            TMP_Text text = other.GetComponent<TMP_Text>();
            if (text != null)
            {
                int value;
                if (int.TryParse(text.text, out value))
                {
                    if (value > number.lineTransformations.length) number.distance *= -1;
                }
            }
        }
    }
}
