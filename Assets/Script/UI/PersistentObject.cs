using UnityEngine;
using TMPro;

public class PersistentObject : MonoBehaviour
{
    public static PersistentObject instance;

    // 数值
    //public int prop1, prop2, prop3;  //道具类 
    //public int talisman1, talisman2, talisman3;   //护符类
    //public int targetCard_you, targetCard_i, targetCard_they, targetCard_all, targetCard_who;   //目标牌
    //public int numberCard_one, numberCard_two, numberCard_three, numberCard_four, numberCard_five, numberCard_six, numberCard_seven, numberCard_eight, numberCard_nine;    //数字牌
    //public int elementCard_heart, elementCard_shield, elementCard_cold, elementCard_hot, elementCard_spirit;    //元素牌
    //public int coins;   //铜钱
    //public int material1, material2, material3, material4;   //锻造材料

    public int[] prop;
    public int[] talisman;
    public int[] targetCard;
    public int[] numberCard;
    public int[] elementCard;
    public int[] material;
    public int coins;


    //文本载体
    //public TMP_Text propText1, propText2, propText3;
    //public TMP_Text talismanText1, talismanText2, talismanText3;
    //public TMP_Text targetCardText_you, targetCardText_i, targetCardText_they, targetCardText_all, targetCardText_who;
    //public TMP_Text numberCardText_one, numberCardText_two, numberCardText_three, numberCardText_four, numberCardText_five, numberCardText_six, numberCardText_seven, numberCardText_eight, numberCardText_nine;
    //public TMP_Text elementCardText_heart, elementCardText_shield, elementCardText_cold, elementCardText_hot, elementCardText_spirit;
    //public TMP_Text coinsText;
    //public TMP_Text materialText1, materialText2, materialText3, materialText4;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DefineInitializeAmount();
    }

    public void DefineInitializeAmount()
    {
        //道具类初始值
        InitializeProp();

        //护符类初始值
        InitializeTalisman();

        //目标牌初始值
        targetCard[0] = 1;  //无穷
        targetCard[1] = 2;
        targetCard[2] = 3;
        targetCard[3] = 2;
        targetCard[4] = 3;

        //数字牌初始值
        numberCard[0] = 1;  //无穷
        numberCard[1] = 2;
        numberCard[2] = 1;
        numberCard[3] = 1;  //无穷
        numberCard[4] = 3;
        numberCard[5] = 2;
        numberCard[6] = 2;
        numberCard[7] = 1;
        numberCard[8] = 1;

        //元素牌初始值
        elementCard[0] = 3;
        elementCard[1] = 3;
        elementCard[2] = 3;
        elementCard[3] = 3;
        elementCard[4] = 3;

        //铜钱初始值
        coins = 15;

        //锻造材料初始值
        InitializeMaterial();
    }

    public static void InitializeProp()
    {
        int randomIndex = Random.Range(0, instance.prop.Length);
        for (int i = 0; i < instance.prop.Length; i++)
        {
            if (i == randomIndex)
            {
                instance.prop[i] = 1;
            }
            else
            {
                instance.prop[i] = 0;
            }
        }
    }

    public static void InitializeTalisman()
    {
        int randomIndex = Random.Range(0, instance.talisman.Length);
        for (int i = 0; i < instance.talisman.Length; i++)
        {
            if (i == randomIndex)
            {
                instance.talisman[i] = 1;
            }
            else
            {
                instance.talisman[i] = 0;
            }
        }
    }

    public static void InitializeMaterial()
    {
        int[] randomIndex = new int[instance.material.Length]; 

        for (int i = 0; i < randomIndex.Length; i++)
        {
            randomIndex[i] = Random.Range(2, 6); 
        }

        for (int i = 0; i < instance.material.Length; i++)
        {
            instance.material[i] = randomIndex[i];
        }
    }
}