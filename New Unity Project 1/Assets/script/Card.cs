using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public enum CardType//牌色
{
    SPADES,
    HARTS,
    CLUBS,
    DIAMONDS, 
}
public class Card : MonoBehaviour
{  
    private CardType cardType;//花色

    private int cardValue;//权值

    private bool canMove = true;
    
    

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
        //鼠标进入范围
    
    public void Enter ()
    {    
        transform.localScale = new Vector3(1.3f,1.3f,1.3f);//1.3倍大小
    }
    //鼠标移出范围
    public void Exit ()
    {
        //恢复原本大小
        transform.localScale = new Vector3(1f,1f,1f);//1倍大小
    } 
    //点击卡牌出牌
    public void Up()
    {
       
    }
    public int GetCardValue
    {
        set { cardValue = value; }
        get { return cardValue; }
    }
    public CardType GetCardType
    {
        set { cardType = value; } 
        get { return cardType; }
    }
    public bool GetCanMove
    {
        set { canMove = value; }
        get { return canMove; }
    }
}
