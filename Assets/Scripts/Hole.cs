using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ball b = other.GetComponent<Ball>();
        if (b != null)
        {
            if(b.Point == 0)
            {
                GameManager.instance.ShowString($"White Ball drop!!\nYou Lose");
                Time.timeScale = 0f;
            }
            else
            {
                GameManager.instance.ShowScoreText(b.Point);
            }
            Destroy(b.gameObject);
        }
    }
}
