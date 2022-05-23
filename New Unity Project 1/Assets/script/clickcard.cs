using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickcard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click ()//点击后进行手牌判定
    {   
        if(transform.parent == GameObject.Find("Player1Cards").transform && GameObject.Find("GameObject").GetComponent<GameManage>().turn ==2)//回合1时玩家2无法操作手牌
        return;
        if(transform.parent == GameObject.Find("Player2Cards").transform && GameObject.Find("GameObject").GetComponent<GameManage>().turn ==1)//回合2时玩家1无法操作手牌
        return;
        Card temp = transform.GetComponent<Card>();
        if(GameObject.Find("GameObject").GetComponent<GameManage>().cardout.Count == 0 || GameObject.Find("GameObject").GetComponent<GameManage>().top.GetCardType != temp.GetComponent<Card>().GetCardType)//为空或值不重复则加入牌堆
        {  
            transform.SetParent(GameObject.Find("P1Card").transform);//转移手牌所有者
            GameObject.Find("GameObject").GetComponent<GameManage>().cardout.Enqueue(temp);//数据存入牌堆队列
            GameObject.Find("GameObject").GetComponent<GameManage>().top =temp;//更新牌堆顶
        }
        else
        {
        if(GameObject.Find("GameObject").GetComponent<GameManage>().top.GetCardType == temp.GetComponent<Card>().GetCardType )
        {   
            if(GameObject.Find("GameObject").GetComponent<GameManage>().turn == 1 )
            {   
                Card Tcard;//临时变量接受要转移的牌
                int length = GameObject.Find("GameObject").GetComponent<GameManage>().cardout.Count;
                transform.SetParent(GameObject.Find("Player1Cards").transform);
                for(int i=0;i<=length-1;i++)
                {
                   Tcard=GameObject.Find("GameObject").GetComponent<GameManage>().cardout.Dequeue();
                   Tcard.transform.SetParent(GameObject.Find("Player1Cards").transform) ;
                }
            }
            else if(GameObject.Find("GameObject").GetComponent<GameManage>().turn == 2 )
            {   
                Card Tcard;//临时变量接受要转移的牌
                int length = GameObject.Find("GameObject").GetComponent<GameManage>().cardout.Count;
                transform.SetParent(GameObject.Find("Player2Cards").transform);
                for(int i=0;i<=length-1;i++)
                {
                   Tcard=GameObject.Find("GameObject").GetComponent<GameManage>().cardout.Dequeue();
                   Tcard.transform.SetParent(GameObject.Find("Player2Cards").transform) ;
                }
            }
           
        }
        }
        GameObject.Find("GameObject").GetComponent<GameManage>().Turnshift();
    }
}
