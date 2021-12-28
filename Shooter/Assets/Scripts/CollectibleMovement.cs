using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    private void Awake()
    {

    }

    public void Update()
    {
        transform.Translate(new Vector2(0f, -1f * Time.deltaTime));
    }
}