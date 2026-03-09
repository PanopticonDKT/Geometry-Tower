using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public Sprite seven;
    public Sprite eight;
    public Sprite nine;

    [Header("")]
    public string Coins_Lives;
    
    public int Class; 

    int value = 0;
    
    // 1. ОБЪЯВЛЯЕМ ПЕРЕМЕННУЮ (исправляет ошибку CS0103)
    private Image img; 

    void Awake()
    {
        // 2. ИЩЕМ КОМПОНЕНТ ПРИ СТАРТЕ
        img = GetComponent<Image>();
    }

    int GetDigit(int value, int index)
    {
        if (Coins_Lives == "Coins")
            value = Manager.Coins;
        else if (Coins_Lives == "Lives")
            value = Manager.Lives;

        value = value / Class;
        return value = value % 10;
    }

    void Start(){
        img.sprite = nine;
        }

    void Update()
    {
        if (img == null) return;
        int digit = GetDigit(value, Class);

        switch (digit)
        {
            case 0: img.sprite = zero; break;
            case 1: img.sprite = one; break;
            case 2: img.sprite = two; break;
            case 3: img.sprite = three; break;
            case 4: img.sprite = four; break;
            case 5: img.sprite = five; break;
            case 6: img.sprite = six; break;
            case 7: img.sprite = seven; break;
            case 8: img.sprite = eight; break;
            case 9: img.sprite = nine; break;
        }
    }
}