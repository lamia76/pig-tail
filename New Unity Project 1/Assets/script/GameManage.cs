using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameManage : MonoBehaviour
{   
    public GameObject st;//开始按钮按钮
    public GameObject et;//开始按钮按钮
    public GameObject P1AB;//P1托管按钮
    public GameObject P1ACB;//P1取消托管按钮
    public GameObject P2AB;//P2托管按钮
    public GameObject P2ACB;//P2取消托管按钮
    public GameObject P1T;//p1托管状态显示
    public GameObject P2T;//p2托管状态显示
    public GameObject srry;//sorry界面
    public GameObject panelX;//游戏说明界面
    public GameObject choosed;//游戏模式选择界面
    public GameObject TURN1;//显示回合1
    public GameObject TURN2;//显示回合2
    public GameObject cardimage;//牌堆图片
    private int P1automode=0;//P1托管
    private int P2automode=0;//P2托管
    public Button button;//开始按钮
    public Button buttondeal;//抽牌按钮
    public int turn = 0;//回合控制
    private Card card;//定义牌类型的变量来接受牌
    public Card top;//存取当前牌堆顶部卡牌
    private int cardType = 0;//定义一种花色变量
    private int gamemode = 0;//模式判断
    private List<Card> cardLibrary = new List<Card>();//牌库（列表） 用来保存创建的牌
    private Queue<Card> cardQueue =  new Queue<Card>();//可发牌库 
    public Queue<Card> cardout = new Queue<Card>();//出牌区    
    public void CreateCard()//创建一副牌
    {
        for(int i = 0; i < 52; i ++ )
        {
            if( i != 0 && i % 13 == 0)
            {
                cardType++;
            }
            card = Instantiate(Resources.Load<Card>("Card"));//实例化卡牌
            //card.gameObject.transform.position =Vector3.forward;//初始化位置
            card.GetComponent<Card>().GetCardType = (CardType)cardType;//牌的花色
            card.GetComponent<Card>().GetCardValue = i % 13 + 3;//牌的权值
            card.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pokers" + "/" + (CardType)cardType+(i % 13 + 3).ToString());//牌的精灵
            card.transform.SetParent(GameObject.Find("cardbox").transform);
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);//存入牌库
        }
         
    }
    public void shuffle()//洗牌
    {
        List<Card> tempLibiry = new List<Card>();
        foreach (var cards in cardLibrary)
        {
            int cardIndex = Random.Range(0,tempLibiry.Count + 1 );//随机下标
            tempLibiry.Insert(cardIndex,cards);//插入新的列表
        }
        cardLibrary.Clear();
        foreach (var cardss in tempLibiry)
        {
            cardQueue.Enqueue(cardss);
        }
        tempLibiry.Clear();//清空临时列表
    }

    
    //发牌
    public void Deal()//抽卡并做判断
    { 
        if(cardQueue.Count <= 0)
      {  
         if(GameObject.Find("Player1Cards").transform.childCount<GameObject.Find("Player2Cards").transform.childCount)
         {
           SceneManager.LoadScene("P1Win");
         }
         else if(GameObject.Find("Player1Cards").transform.childCount>=GameObject.Find("Player2Cards").transform.childCount)
         {
           SceneManager.LoadScene("P2Win");
         }
      } 
        Card cardnext;//定义牌类型接受牌顶牌 
        cardnext = cardQueue.Dequeue();
        cardnext.gameObject.SetActive(true);
        if(cardout.Count == 0)//出牌区为空直接加入
        {
            cardnext.transform.SetParent(GameObject.Find("P1Card").transform);
            top = cardnext;
            cardout.Enqueue(cardnext);
            
        }
        else //否则对比
        {
           if(top.GetComponent<Card>().GetCardType == cardnext.GetComponent<Card>().GetCardType)
           {   
               int length = cardout.Count;
               if(turn ==1)
               cardnext.transform.SetParent(GameObject.Find("Player1Cards").transform);
               else if(turn == 2)
               cardnext.transform.SetParent(GameObject.Find("Player2Cards").transform);
               Card cardafter;
               for(int i=0;i<=length-1;i++)
               {
                    cardafter = cardout.Dequeue();
                    if(turn == 1)
                    {cardafter.transform.SetParent(GameObject.Find("Player1Cards").transform);}
                    else if(turn == 2)
                    {cardafter.transform.SetParent(GameObject.Find("Player2Cards").transform);}
                }
           }
           else
           {
              cardnext.transform.SetParent(GameObject.Find("P1Card").transform);
              cardout.Enqueue(cardnext);
              top = cardnext;
           }
        }
        Turnshift();
    }  
    public void AIDeal()//AI抽卡并做判断
    {   
        if(cardQueue.Count <= 0)
      {  
         if(GameObject.Find("Player1Cards").transform.childCount<GameObject.Find("Player2Cards").transform.childCount)
         {
           SceneManager.LoadScene("P1Win");
         }
         else if(GameObject.Find("Player1Cards").transform.childCount>=GameObject.Find("Player2Cards").transform.childCount)
         {
           SceneManager.LoadScene("P2Win");
         }
      } 
        Card cardnext;//定义牌类型接受牌顶牌 
        cardnext = cardQueue.Dequeue();
        cardnext.gameObject.SetActive(true);
        if(cardout.Count == 0)//出牌区为空直接加入
        {
            cardnext.transform.SetParent(GameObject.Find("P1Card").transform);
            top = cardnext;
            cardout.Enqueue(cardnext);
            
        }
        else //否则对比
        {
           if(top.GetComponent<Card>().GetCardType == cardnext.GetComponent<Card>().GetCardType)
           {   
               int length = cardout.Count;
               if(turn ==1)
               cardnext.transform.SetParent(GameObject.Find("Player1Cards").transform);
               else if(turn == 2)
               cardnext.transform.SetParent(GameObject.Find("Player2Cards").transform);
               Card cardafter;
               for(int i=0;i<=length-1;i++)
               {
                    cardafter = cardout.Dequeue();
                    if(turn == 1)
                    {cardafter.transform.SetParent(GameObject.Find("Player1Cards").transform);}
                    else if(turn == 2)
                    {cardafter.transform.SetParent(GameObject.Find("Player2Cards").transform);}
                }
           }
           else
           {
              cardnext.transform.SetParent(GameObject.Find("P1Card").transform);
              cardout.Enqueue(cardnext);
              top = cardnext;
           }
        }
    }    
    public void OnDeal()//开始游戏
    {   
        CreateCard();
        shuffle();
        Turnshift();
        button.gameObject.SetActive(false);
        buttondeal.gameObject.SetActive(true);
        cardimage.gameObject.SetActive(true);
        if(gamemode == 1 )
        {
         P1AB.gameObject.SetActive(true);
         P2AB.gameObject.SetActive(true);
        }
        else if(gamemode == 2)
        {
         P1AB.gameObject.SetActive(true);
        }
    }
    public void Turnshift()//回合切换
    {
        if( turn == 1 )
        {   
            TURN1.SetActive(false);
            TURN2.SetActive(true);
            if(P1automode==1)
            {
                AIDeal();
                turn = 2;
                Turnshift();
            }
            else 
            turn = 2;
        }
        else if(turn == 2 )
        {   
            TURN2.SetActive(false);
            TURN1.SetActive(true);
            if(gamemode == 2 || P2automode==1)
            {
            AIDeal();
            turn = 1;
            Turnshift();
            }
            else 
            turn = 1;
        }
        else if(turn == 0)
        {
            turn = 1;
            TURN1.SetActive(true);
        }
    }
    public void PVP()//选择PVP
    {
       choosed.gameObject.SetActive(false);
       st.gameObject.SetActive(true);
       et.gameObject.SetActive(true);
       gamemode = 1;
    }
    public void PVE()//选择PVE
    {
       choosed.gameObject.SetActive(false);
       st.gameObject.SetActive(true);
       et.gameObject.SetActive(true);
       gamemode = 2;
    }
    public void pre()//关闭游戏说明
    {
        panelX.gameObject.SetActive(false);
        choosed.gameObject.SetActive(true);
    }
    public void P1A()//P1开始托管
    {   
        P1automode = 1;
        P1AB.gameObject.SetActive(false);
        P1ACB.gameObject.SetActive(true);
        P1T.gameObject.SetActive(true);
        AIDeal();
    }
    public void P2A()//P2开始托管
    {
        P2automode = 1;
        P2AB.gameObject.SetActive(false);
        P2ACB.gameObject.SetActive(true);
        P2T.gameObject.SetActive(true);
        AIDeal();
    }
     public void P1AC()//P1取消托管
    {
        P1automode = 0;
        P1ACB.gameObject.SetActive(false);
        P1AB.gameObject.SetActive(true);
        P1T.gameObject.SetActive(false);
    }
     public void P2AC()//P2取消托管
    {
        P2automode = 0;
        P2ACB.gameObject.SetActive(false);
        P2AB.gameObject.SetActive(true);
        P2T.gameObject.SetActive(false);
    }
    public void sorry()
    {
        srry.gameObject.SetActive(true);
    }
    public void retrn()
    {
        srry.gameObject.SetActive(false);
    }
    void Start()
    {   
        TURN1.SetActive(false);
        TURN2.SetActive(false);
        st.gameObject.SetActive(false);
        et.gameObject.SetActive(false);
        buttondeal.gameObject.SetActive(false);
        cardimage.gameObject.SetActive(false);
        choosed.gameObject.SetActive(false);
        srry.gameObject.SetActive(false);
        P1ACB.gameObject.SetActive(false);
        P1AB.gameObject.SetActive(false);
        P2AB.gameObject.SetActive(false);
        P2ACB.gameObject.SetActive(false);
        P1T.gameObject.SetActive(false);
        P2T.gameObject.SetActive(false);
        panelX.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
