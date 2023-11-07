using UnityEngine;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour
{
    public FoodSO FoodSO { get; private set; }

    private FoodSO[] _foodsSO;

    private Image _image;

    public void Init()
    {
        _image = GetComponent<Image>();
        _foodsSO = Resources.LoadAll<FoodSO>("SO/Foods");
    }

    public void CreateFood(FoodVariant foodVariant)
    {
        FoodSO = _foodsSO[(int)foodVariant];
        _image.sprite = FoodSO.Sprite;
    }
}
