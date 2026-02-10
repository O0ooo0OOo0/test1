using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartProduce : MonoBehaviour
{
    public List<InputMaterial> inputMaterials;

    [System.Serializable]
    public struct Products
    {
        public GameObject product;
        public string type;
        public string name;
        public int amount;
        public TMP_Text creat_content;
    }

    public List<Products> products;
    public Button startproduce;
    public GameObject creation;
    public CancelInput cancelinput;

    public bool isCanForging;
    public TMP_Text tips_text;
    public string tips;
    public int forgging;
    public float[] probability;
    public BagContentTextAmount bag;
    public int typeAmount;
    public int[] type;

    public GameObject white;
    public float fadeTime = 1.5f;

    Animator duanzao;

    void Start()
    {
        forgging = 5;
        duanzao = GetComponent<Animator>();
        creation.SetActive(false);
        white.SetActive(false);

        probability = new float[products.Count];
        type = new int[typeAmount];
        TypeStatistics();

        if (startproduce != null)
        {
            startproduce.onClick.AddListener(JudgeForgging);
        }
    }

    public void TypeStatistics()
    {
        for (int i = 0; i < typeAmount; i++)
        {
            type[i] = 0;
        }

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].type.IndexOf("prop", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                type[0]++;
            }
            else if (products[i].type.IndexOf("target", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                type[1]++;
            }
            else if (products[i].type.IndexOf("number", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                type[2]++;
            }
            else if (products[i].type.IndexOf("element", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                type[3]++;
            }
        }
    }

    public void JudgeForgging()
    {
        Text();
        if (isCanForging == true)
        {
            tips_text.text = null;
            ShowCreation();
        }
        else if (isCanForging == false)
        {
            tips_text.text = tips;
        }
    }

    public void Text()
    {
        if (SceneManager.GetActiveScene().name != "map")
        {
            isCanForging = false;
            tips = "抱歉，当前环境被污染，锻造炉无法进行锻造";
        }
        else
        {
            if (cancelinput.inputContent.text == "")
            {
                isCanForging = false;
                tips = "请投入锻造材料";
            }
            else if (cancelinput.inputContent.text != "")
            {
                if (inputMaterials[0].inputAmount == 0)
                {
                    isCanForging = false;
                    tips = "至少需要投入1枚铜币才能进行锻造";
                }
                else if (inputMaterials[0].inputAmount > forgging)
                {
                    isCanForging = false;
                    tips = "锻造炉能力有限，一次只能投入不超过" + forgging + "枚铜币";
                }
                else if (inputMaterials[0].inputAmount > 0 && inputMaterials[0].inputAmount <= forgging)
                {
                    int amount = 0;
                    for (int i = 1; i < inputMaterials.Count; i++)
                    {
                        amount = amount + inputMaterials[i].inputAmount;
                    }
                    if (amount > 10)
                    {
                        isCanForging = false;
                        tips = "投入的锻造定向材料过多，导致锻造混乱，将无法按照预期概率进行产出，请重新投入锻造材料，注意投入的锻造定向材料不要超过10个";
                        cancelinput.CancelAllInputs();
                    }
                    else
                    {
                        isCanForging = true;
                    }
                }
            }
        }
    }

    public void ShowCreation()
    {
        cancelinput.inputContent.text = null;
        InputMaterial.allMaterials.Clear();
        duanzao.SetBool("isForgging", true);
        StartCoroutine(Delay());
        StartCoroutine(Fade(fadeTime));
    }

    public void ClearInput()
    {
        for (int i = 0; i < inputMaterials.Count; i++)
        {
            inputMaterials[i].inputAmount = 0;
        }
    }

    IEnumerator Delay()
    {
        startproduce.interactable = false;
        yield return new WaitForSeconds(2.4f);
        duanzao.SetBool("isForgging", false);
        startproduce.interactable = true;
    }

    IEnumerator Fade(float time)
    {
        yield return new WaitForSeconds(1.8f);

        white.SetActive(true);
        float elapsedTime = 0f;
        float alpha = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(alpha, 1f, elapsedTime / time);
            white.GetComponent<Image>().color = new Color(1, 1, 1, newAlpha);
            yield return null;
        }
        alpha = 1f;
        elapsedTime = 0f;
        white.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        CreatContent();

        float timetime = time + 0.2f;
        while (elapsedTime < timetime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(alpha, 0f, elapsedTime / timetime);
            white.GetComponent<Image>().color = new Color(1, 1, 1, newAlpha);
            yield return null;
        }
        alpha = 0f;
        white.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        white.SetActive(false);
    }

    public void CreatContent()
    {
        creation.SetActive(true);
        ProbabilityCalculations();
        for (int i = 0; i < inputMaterials[0].inputAmount; i++)
        {
            Forgging();
        }
        InputResult();
        Reset();
    }

    public void ProbabilityCalculations()
    {
        float together = 0;

        for (int i = 1; i < inputMaterials.Count && i - 1 < products.Count; i++)
        {
            if (inputMaterials[i].materialType == i)
            {
                if (inputMaterials[i].inputAmount > 0)
                {
                    probability[i - 1] = 0.1f * inputMaterials[i].inputAmount;
                    together = together + probability[i - 1];
                }
                else if (inputMaterials[i].inputAmount == 0)
                {
                    probability[i - 1] = 0;
                }
            }
        }

        float average = Mathf.Max(0, 1 - together) / products.Count;

        for (int i = 0; i < products.Count; i++)
        {
            probability[i] = probability[i] + average;
        }
    }

    public void Forgging()
    {
        float randomIndex = Random.Range(0f, 1f);

        float[] region = new float[products.Count];
        float regionAll = 0;
        for (int i = 0; i < products.Count; i++)
        {
            regionAll = regionAll + probability[i];
            region[i] = regionAll;
        }

        for (int i = 0; i < products.Count; i++)
        {
            if (i == 0)
            {
                if (randomIndex >= 0 && randomIndex < probability[0])
                {
                    Products product = products[0];
                    product.amount = product.amount + 1;
                    products[0] = product;
                    PersistentObject.instance.prop[0]++;
                }
            }
            else if (i != 0)
            {
                if (randomIndex >= region[i-1] && randomIndex < region[i])
                {
                    Products product = products[i];
                    product.amount = product.amount + 1;
                    products[i] = product;

                    if (products[i].type == "prop" + i)
                    {
                        PersistentObject.instance.prop[i]++;
                    }
                    else if (products[i].type == "target" + (i - type[0]))
                    {
                        int a = i - type[0];
                        PersistentObject.instance.targetCard[a]++;
                    }
                    else if (products[i].type == "number" + (i - type[0] - type[1]))
                    {
                        int a = i - type[0] - type[1];
                        PersistentObject.instance.numberCard[a]++;
                    }
                    else if (products[i].type == "element" + (i - type[0] - type[1] - type[2]))
                    {
                        int a = i - type[0] - type[1] - type[2];
                        PersistentObject.instance.elementCard[a]++;
                    }
                }
            }
        }
    }

    public void InputResult()
    {
        for (int i = 0; i < products.Count;i++)
        {
            if (products[i].amount != 0)
            {
                products[i].product.SetActive(true);
                products[i].creat_content.text = products[i].name + "*" + products[i].amount;
            }
        }
        bag.AmountText();
    }

    public void Reset()
    {
        for (int i = 0; i < products.Count; i++)
        {
            probability[i] = 0;
        }
        ClearInput();
    }
}
