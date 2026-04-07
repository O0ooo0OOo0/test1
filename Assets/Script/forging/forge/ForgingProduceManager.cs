using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using GameFramework.Samples.Localization;
using System.Linq;

public class ForgingProduceManager : MonoBehaviour
{
    public List<InputMaterial> inputMaterials;   // 可参与锻造的材料
    public List<ForgingProducts> products;   // 锻造产物信息

    // 动画
    public Animator duanzao;   // 锻造动画
    public float duanzaoAniTime = 1f;   
    public GameObject white;   // 过渡动画
    public float fadeInSpeed = 1f;

    // UI
    public Button startForging;
    public GameObject productsPanel;    // 锻造产物界面
    public GameObject extraPdtsPanel;    // 额外锻造产物界面
    public TMP_Text exPdtsText;

    // 锻造
    public int forgCoinsMax;   // 每次锻造最大可投入的铜币数量
    public TMP_Text inputContent;   // 投入的锻造材料
    private bool isCombatScene;   // 是否是战斗场景
    public bool isCanForging;   // 是否可以锻造
    public float[] probability;   // 每个产物的锻造概率

    // 额外锻造 
    public float extraFgP;   // 出现额外的锻造产物的概率
    public int extraTimes;   // 额外锻造的次数
    public bool isExtraFg;
    private Dictionary<string, int> pdtsEX;   // 额外产出内容

    // 调用其他单个脚本
    public ForgingDialogManager fdm;
    public CancelInput cancelinput;
    public MatCounterPdt mcp;
    public ItemAmountText itemAmount;
    public CardsBagAmountText cardsBagAmount;

    void Start()
    {
        productsPanel.SetActive(false);
        extraPdtsPanel.SetActive(false);
        white.SetActive(false);

        probability = new float[products.Count];
        pdtsEX = new Dictionary<string, int>();   // 初始化字典
        ResetFgData();

        if (startForging != null)
        {
            startForging.onClick.AddListener(JudgeForgging);
        }
    }

    // 判断是否能进行锻造
    public void JudgeForgging()
    {
        // 战斗时不可锻造物品
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // 获取当前场景序列数

        // 判断当前场景是否为战斗场景
        CombatSceneManager.csm.JudgeIsCombatScene(currentSceneIndex);
        isCombatScene = CombatSceneManager.csm.isCombatScene;

        // 生成提示
        if (isCombatScene == true)   // 战斗场景
        {
            isCanForging = false;
            fdm.OnlyTip(0);
        }
        else if (isCombatScene == false)   // 非战斗场景
        {
            if (inputContent.text == "")   // 没有投入锻造材料
            {
                isCanForging = false;
                fdm.OnlyTip(1);
            }
            else if (inputContent.text != "")   // 投入了锻造材料
            {
                if (inputMaterials[0].inputAmount == 0)   // 没有投入铜币
                {
                    isCanForging = false;
                    fdm.OnlyTip(2);
                }
                else if (inputMaterials[0].inputAmount > forgCoinsMax)   // 投入铜币数超出
                {
                    isCanForging = false;
                    fdm.OnlyTip(3);
                }
                else if (inputMaterials[0].inputAmount > 0 && inputMaterials[0].inputAmount <= forgCoinsMax)
                {
                    // 检查其他锻造材料的投入总数
                    int amount = 0;
                    for (int i = 1; i < inputMaterials.Count; i++)   // 获取投入的其他锻造材料总数
                    {
                        amount = amount + inputMaterials[i].inputAmount;
                    }
                    if (amount > 10)
                    {
                        isCanForging = false;
                        fdm.OnlyTip(4);
                        cancelinput.RefreshInputs();   // 重置锻造材料投入
                    }
                    else
                    {
                        isCanForging = true;
                        fdm.ClearDialogContent();
                        StartForging();
                        JudgeExtraFg();
                    }
                }
            }
        }
    }

    // 开始锻造
    public void StartForging()
    {
        inputContent.text = null;   
        InputMaterial.allMaterials.Clear();   // 清空投入材料字典
        duanzao.SetBool("isForgging", true);   // 开始锻造动画
        StartCoroutine(DelayAniTime());   // 延迟锻造动画的时长
        StartCoroutine(Fade());   // 产物显示过渡效果
    }

    // 锻造动画协程
    IEnumerator DelayAniTime()
    {
        startForging.interactable = false;   // 锻造动画期间不可再次点击开始锻造
        yield return new WaitForSeconds(duanzaoAniTime);
        duanzao.SetBool("isForgging", false);   // 重置动画状态
        startForging.interactable = true;
    }

    // 产出过渡协程
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(duanzaoAniTime - 0.6f);   // 等待锻造动画

        white.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + 1f);
            white.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        alpha = 1f;
        white.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        ProduceContent();  // 锻造内容

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            white.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        alpha = 0f;
        white.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        white.SetActive(false);
    }

    // 产物生成及产出结果显示
    public void ProduceContent()
    {
        productsPanel.SetActive(true);

        // 正常锻造
        ProbabilityCalculations();
        for (int i = 0; i < inputMaterials[0].inputAmount; i++)   // 锻造产物的总数 = 投入的铜币数
        {
            Forgging();
        }

        // 额外锻造
        if (isExtraFg == true)
        {
            for (int i = 0; i < extraTimes; i++)   // 额外锻造产物总数 = 额外锻造次数
            {
                ExtraForging();
            }
            extraPdtsPanel.SetActive(true);
            ExtraFgingResult();
        }

        ForgingResult();
        ResetFgData();
        ClearInput();
    }

    /// <summary>
    /// 正常锻造流程
    /// </summary>

    // 计算每个产物的产出概率（根据投入材料）
    public void ProbabilityCalculations()
    {
        float together = 0;   // 所有产物产出的总概率

        // 根据投入材料计算产物产出的额外概率
        for (int i = 1; i < inputMaterials.Count; i++)
        {
            if (inputMaterials[i].inputAmount > 0)
            {
                ForgingProducts pdt;   // 局部变量
                mcp.FindMatCouPdt(inputMaterials[i]);   // 查找材料对应的产物
                pdt = mcp.matCpdt;

                for (int j = 0; j < products.Count; j++)
                {
                    if (products[j] == pdt)
                    {
                        probability[j] = 0.1f * inputMaterials[i].inputAmount;  // 每投入一个材料，该材料对应产物的产出概率增加10%
                        together = together + probability[j];
                    }
                }
            }
        }

        float average = Mathf.Max(0, 1 - together) / products.Count;   // 剩余概率平均分配给所有产物

        // 产物实际产出概率
        for (int i = 0; i < products.Count; i++)
        {
            probability[i] = probability[i] + average;
        }
    }

    // 锻造
    public void Forgging()
    {
        float randomIndex = Random.Range(0f, 1f);   // 生成0-1的随机数（数值在哪个产物的对应区间内，即生成哪种产物）

        // 形成产物对应的数值区间
        float[] region = new float[products.Count + 1];   // 区间起止点数组（数组长度等于总产物数量+1）
        float regionProduct = 0;
        for (int i = 0; i < products.Count; i++)   // 设置前 products.Count 个区间点（防止probability[i]访问越界）
        {
            region[i] = regionProduct;
            regionProduct = regionProduct + probability[i];
        }
        region[products.Count] = regionProduct;   // 最后一个区间点

        // 根据数值所在的区间生成对应产物
        for (int i = 0; i < products.Count; i++)
        {
            if (randomIndex >= region[i] && randomIndex < region[i + 1])
            {
                ForgingProducts product = products[i];   // 创建局部副本
                product.amount = product.amount + 1;
                products[i] = product;

                UpdatePersistentObj(i);
            }
        }
    }

    /// <summary>
    /// 额外锻造流程
    /// </summary>

    // 判断本次锻造是否产生额外的副产物
    public void JudgeExtraFg()
    {
        float randomExtra = Random.Range(0f, 1f);

        if (randomExtra < extraFgP)
        {
            isExtraFg = true;
            ExtraFgTimes();
        }
        else
        {
            isExtraFg = false;
        }
    }

    // 本次额外锻造的次数
    public void ExtraFgTimes()
    {
        float randomTimes = Random.Range(0f, 1f);

        extraTimes = 0;   // 初始化额外锻造次数
        if (randomTimes < 0.6)     // 60%的概率额外锻造次数为1
        {
            extraTimes = 1;   
        }
        else if (randomTimes >= 0.6 && randomTimes < 0.9)     // 30%的概率额外锻造次数为2
        {
            extraTimes = 2;
        }
        else if (randomTimes >= 0.9)     // 10%的概率额外锻造次数为3
        {
            extraTimes = 3;
        }
    }

    // 额外锻造
    public void ExtraForging()
    {
        int random = Random.Range(0, products.Count);

        for (int i = 0; i < products.Count; i++)
        {
            if (i == random)
            {
                ForgingProducts pdt = products[i];
                pdt.amount++;
                products[i] = pdt;
                UpdatePersistentObj(i);

                // 更新副产物数据
                if (pdtsEX.ContainsKey(products[i].nameP))
                {
                    pdtsEX[products[i].nameP]++;
                }
                else
                {
                    pdtsEX.Add(products[i].nameP, 1);
                }
            }
        }
    }

    /// <summary>
    /// 锻造结果输出及信息处理
    /// </summary>

    // 输出整体锻造结果
    public void ForgingResult()
    {
        for (int i = 0; i < products.Count;i++)
        {
            if (products[i].amount != 0)
            {
                products[i].product.SetActive(true);
                products[i].productContent.text = products[i].nameP + "×" + products[i].amount;
            }
        }

        // 更新背包中的物品数量
        itemAmount.AmountTextItem();
        cardsBagAmount.AmountTextCards();
    }

    // 输出额外锻造结果
    public void ExtraFgingResult()
    {
        foreach (var pdt in pdtsEX)
        {
            exPdtsText.text += pdt.Key + "×" + pdt.Value + "、";
        }
        exPdtsText.text = exPdtsText.text.TrimEnd("、");
    }

    /// <summary>
    /// 重置数据
    /// </summary>

    // 重置本次锻造数据
    public void ResetFgData()
    {
        // 重置产物的锻造概率
        for (int i = 0; i < products.Count; i++)
        {
            probability[i] = 0;
        }

        // 重置额外锻造
        isExtraFg = false;
        extraTimes = 0;
        pdtsEX.Clear();
    }

    public void ClearInput()
    {
        for (int i = 0; i < inputMaterials.Count; i++)
        {
            inputMaterials[i].inputAmount = 0;
        }
    }

    /// <summary>
    /// 更新全局数据
    /// </summary>
    /// <param name="i"></param>

    // 更新物品数据组中的数据
    public void UpdatePersistentObj(int id)
    {
        var arc = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex];

        if (products[id].type == TypeManager.ForgingProductType.prop)
        {
            var prop = arc.props[products[id].index];
            prop.propAmount++;
            arc.props[products[id].index] = prop;
        }
        else if (products[id].type == TypeManager.ForgingProductType.target)
        {
            var tarcard = arc.tarCards[products[id].index];
            tarcard.tarCardAmount++;
            arc.tarCards[products[id].index] = tarcard;
        }
        else if (products[id].type == TypeManager.ForgingProductType.number)
        {
            var numcard = arc.numCards[products[id].index];
            numcard.numCardAmount++;
            arc.numCards[products[id].index] = numcard;
        }
        else if (products[id].type == TypeManager.ForgingProductType.element)
        {
            var elecard = arc.eleCards[products[id].index];
            elecard.eleCardAmount++;
            arc.eleCards[products[id].index] = elecard;
        }

        ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex] = arc;
    }
}
