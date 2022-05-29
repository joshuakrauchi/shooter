using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    public void BuyItem()
    {
        ++gameData.NumberOfShots;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}