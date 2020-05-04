using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileExtension;
using UnityEngine.UI;
using System.Data;

public class GameManager_S : MonoBehaviour
{
    public static Sprite[] sprites;
//    [SerializeField] public GameObject Player1_Gameobj;
    
    [SerializeField] private RectTransform Player1_Hand;
    [SerializeField] private RectTransform Player2_Hand;
    [SerializeField] private RectTransform Player3_Hand;
    [SerializeField] private RectTransform Player4_Hand;
    
    [SerializeField] private RectTransform GameBoard;

    public static RectTransform gameBoard;



    [SerializeField] public int LastRightNum;
    [SerializeField] public int LastLeftNum;



    [SerializeField] private DominoCard card;
    private Vector2 leftMost = Vector2.zero, rightMost = Vector2.zero;
    private Vector2 XleftMost = Vector2.zero, XrightMost = Vector2.zero;

    private Vector2 screenBound = Vector2.zero;
    private Vector2 SndRightJoin = Vector2.zero;
    private Vector2 SndLeftJoin = Vector2.zero;

    DominoDirection SettleDirection;


    public List<DominoCard> dominoes = new List<DominoCard>();
    public LinkedList<DominoCard> linkedDominoes = new LinkedList<DominoCard>();

    public  static Sprite CloneSprite(int index)
    {
        if (index > sprites.Length)
        {
            return null;
        }
        return Instantiate<Sprite>(sprites[index]);
    }

    private static GameManager_S instance;
    public static GameManager_S Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager_S>();
            }

            return instance;

        }
    }
    void Awake()
    {
        gameBoard = GameBoard;
          sprites = Resources.LoadAll<Sprite>("DominoRocks/StyleA");
        
    }
    float timeLeft = 0.1f;
    int t = 0;
    bool zaza;

    

    private void FixedUpdate()
    {

        //timeLeft -= Time.deltaTime;
        //if (timeLeft < 0)
        //{
        //    timeLeft = 0.1f;
        //    zaza = !zaza;
        //    SettleCardInTheBoard(t, false);
        //    t += 1;
        //}
        //if (t > 27) t = 0;
        //SettleCardInTheBoard(0, true);
        //SettleCardInTheBoard(2, true);
        //SettleCardInTheBoard(13, true);
        //SettleCardInTheBoard(16, true);
        //SettleCardInTheBoard(23, true);
        //SettleCardInTheBoard(15, true);
        //SettleCardInTheBoard(17, true);
        //SettleCardInTheBoard(27, true);
        //SettleCardInTheBoard(21, true);
        //SettleCardInTheBoard(14, true);
        //        SettleCardInTheBoard(8, true);
        
        
        
        //SettleCardInTheBoard(1, false);
        //SettleCardInTheBoard(9, false);
        //SettleCardInTheBoard(19, false);

        //SettleCardInTheBoard(7, true);

        //SettleCardInTheBoard(4, false);
        // SettleCardInTheBoard(0, true);
        //SettleCardInTheBoard(1, true);
        //SettleCardInTheBoard(2, false);
        //SettleCardInTheBoard(3, true);
        //SettleCardInTheBoard(4, false);

    }


    private void Start()
    {
        screenBound = new Vector2((GameBoard.rect.width/2)-(PublicStuf.dominoHeight*1.5f),(GameBoard.rect.height/2) - (PublicStuf.dominoHeight*1.5f));
        int SpriteIndex =0;
        for (int i = 6; i >= 0; i--)
        {
            for (int ii = i; ii >= 0; ii--)
            {
               
                DominoCard temp =// Instantiate(card);
                Instantiate(card,new Vector3(i,2-ii,0) , Quaternion.identity);
                
                temp.Value1 = i;
                temp.Value2 = ii;
                temp.ValuesArrange();
                temp.spriteIndex = SpriteIndex;

                temp.IsObservableByAll=true;
                
                dominoes.Add(temp);
           
                SpriteIndex++;
            }

         
        }
        NewGame();

        SettleTheCardInTheBoard(16, false);
        SettleTheCardInTheBoard(20, false);
        SettleTheCardInTheBoard(19, false);
        SettleTheCardInTheBoard(15, false);

        SettleTheCardInTheBoard(14, false);
        SettleTheCardInTheBoard(18, false);
        SettleTheCardInTheBoard(3, false);
        SettleTheCardInTheBoard(0, false);

        SettleTheCardInTheBoard(1, false);
        SettleTheCardInTheBoard(7, false);
        SettleTheCardInTheBoard(9, false);
        SettleTheCardInTheBoard(21, false);
        SettleTheCardInTheBoard(27, false);

        SettleTheCardInTheBoard(17, true);


        SettleTheCardInTheBoard(24, true);
        SettleTheCardInTheBoard(23, true);

    }


    void NewGame()
    {
        dominoes.Shuffle();
        for (int i = 0; i < 7; i++)
        {
            dominoes[i].ownership = GameRole.Player1;
            dominoes[i].transform.SetParent(Player1_Hand);
            dominoes[i].DominoDirection = DominoDirection.Vertical_AZ;
            dominoes[i].IsObservableByAll = true;
            dominoes[i].transform.localScale = Vector3.one;
        }
        for (int i = 7; i < 14; i++)
        {
            dominoes[i].ownership = GameRole.Player2;
            dominoes[i].transform.SetParent(Player2_Hand);

            dominoes[i].DominoDirection = DominoDirection.Horizontal_AZ;
            dominoes[i].transform.localScale = Vector3.one;
        }
        for (int i = 14; i < 21; i++)
        {
            dominoes[i].ownership = GameRole.Player3;
            dominoes[i].transform.SetParent(Player3_Hand);

            dominoes[i].DominoDirection = DominoDirection.Vertical_ZA;
            dominoes[i].transform.localScale = Vector3.one;
        }
        for (int i = 21; i < 28; i++)
        {
            dominoes[i].ownership = GameRole.Player4;
            dominoes[i].transform.SetParent(Player4_Hand);

            dominoes[i].DominoDirection = DominoDirection.Horizontal_ZA;
            dominoes[i].transform.localScale = Vector3.one;
        }
    }

    public void SettleTheCardInTheBoard(int SpriteIndex, bool addRight)
    {
        RectTransform domRect;
        foreach (DominoCard dominoCard in dominoes)
        {
            if (dominoCard.ownership != GameRole.Board)
            {
                if (dominoCard.spriteIndex == SpriteIndex)
                {
                    if (rightMost != Vector2.zero)
                    {
                        if (!(dominoCard.Value1 == LastRightNum || dominoCard.Value2 == LastRightNum) &&
                            !(dominoCard.Value1 == LastLeftNum || dominoCard.Value2 == LastLeftNum))
                        {
                            break;
                        }
                    }

                    domRect = dominoCard.GetComponent<RectTransform>();


                    if (rightMost   == Vector2.zero && leftMost == Vector2.zero)
                    {
                        #region First Card
                        if (dominoCard.Value1 == dominoCard.Value2)
                        {
                            FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ, new Vector3(0, 0, 0),
                                new Vector2(domRect.rect.width / 2, 0), -new Vector2(domRect.rect.width / 2, 0),false,
                                dominoCard.Value1, dominoCard.Value2, domRect.rect.height / 2, -domRect.rect.height / 2);
                        }
                        else
                        {
                            FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ, new Vector3(0, 0, 0),
                            new Vector2(domRect.rect.height / 2, 0), -new Vector2(domRect.rect.height / 2, 0), true,
                            dominoCard.Value1, dominoCard.Value2, domRect.rect.width / 2,- domRect.rect.width / 2);
                        }
                        #endregion
                    } 
                    else if (rightMost.x + Mathf.Abs(leftMost.x) < (screenBound.x*2))
                    {
                        if (dominoCard.Value1 == dominoCard.Value2 && addRight)
                        {
                            FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ,
                            new Vector3(rightMost.x + (domRect.rect.width / 2), 0, 0),
                            new Vector2(domRect.rect.width, 0), Vector2.zero, true,
                            LastLeftNum, dominoCard.Value2, domRect.rect.height / 2, 0);
                            if (rightMost.x > screenBound.x) { CenterCards(); }
                        }
                        else if (dominoCard.Value1 == dominoCard.Value2 && !addRight)
                        {
                            FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ,
                                new Vector3(leftMost.x - (domRect.rect.width / 2), 0, 0),
                                Vector2.zero, new Vector2(-domRect.rect.width, 0), false,
                                dominoCard.Value1, LastRightNum,0,- domRect.rect.height / 2);
                            if (Mathf.Abs(leftMost.x) > screenBound.x) { CenterCards(); }
                        }
                        else if (addRight && dominoCard.Value1 == LastRightNum) 
                        {   
                            FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ, 
                            new Vector3(rightMost.x + (domRect.rect.height / 2), 0, 0),
                            new Vector2(domRect.rect.height , 0), Vector2.zero, true,
                            LastLeftNum, dominoCard.Value2, domRect.rect.width / 2, 0);
                            if (rightMost.x > screenBound.x) { CenterCards(); }
                        } 
                        else if (!addRight && dominoCard.Value2 == LastLeftNum)
                        {
                            FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ,
                            new Vector3(leftMost.x - (domRect.rect.height / 2), 0, 0),
                            Vector2.zero, new Vector2(-domRect.rect.height, 0), false,
                            dominoCard.Value1, LastRightNum,0,- domRect.rect.width / 2);
                            if (Mathf.Abs( leftMost.x) > screenBound.x) { CenterCards(); }
                        }
                        else if (addRight && dominoCard.Value2 == LastRightNum)  
                        {
                            FirstCardPlacement(dominoCard, DominoDirection.Horizontal_ZA,
                            new Vector3(rightMost.x + (domRect.rect.height / 2), 0, 0),
                            new Vector2(domRect.rect.height , 0), Vector2.zero, true,
                            LastLeftNum, dominoCard.Value1, domRect.rect.width / 2, 0);
                            if (rightMost.x > screenBound.x) { CenterCards(); }
                        } 
                        else if (!addRight && dominoCard.Value1 == LastLeftNum)
                        {
                            //SettleDirection = DominoDirection.Horizontal_ZA;
                            //LastLeftNum = dominoCard.Value2;
                            FirstCardPlacement(dominoCard, DominoDirection.Horizontal_ZA,
                            new Vector3(leftMost.x - (domRect.rect.height / 2), 0, 0),
                            Vector2.zero, new Vector2(-domRect.rect.height, 0), false,
                            dominoCard.Value2, LastRightNum, 0,- domRect.rect.width / 2);
                            if (Mathf.Abs(leftMost.x) > screenBound.x) { CenterCards(); }
                        }
                    }
                    else if(rightMost.x + Mathf.Abs(leftMost.x) > (screenBound.x * 2))
                    {
                        if (rightMost.y < screenBound.y)
                        {
                            if (dominoCard.Value1 == dominoCard.Value2 && addRight)
                            {
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, domRect.rect.width / 2);
                                FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ,
                                new Vector3(rightMost.x - posAmend.x, rightMost.y + (domRect.rect.width / 2), 0),
                                new Vector2(0, domRect.rect.width), Vector2.zero, true,
                                LastLeftNum, dominoCard.Value2);
                                XrightMost = rightMost;
                                XrightMost.x -= domRect.rect.width / 2;
                            }
                            else if (addRight && dominoCard.Value1 == LastRightNum && dominoCard.Value1 != dominoCard.Value2)
                            {
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ,
                                new Vector3(rightMost.x - posAmend.x, rightMost.y + (domRect.rect.height / 2), 0),
                                new Vector2(0, domRect.rect.height), Vector2.zero, true,
                                LastLeftNum, dominoCard.Value2);
                                XrightMost = rightMost;
                            }
                            else if (addRight && dominoCard.Value2 == LastRightNum && dominoCard.Value1 != dominoCard.Value2)
                            {
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Vertical_ZA,
                                new Vector3(rightMost.x - posAmend.x, rightMost.y + (domRect.rect.height / 2), 0),
                                new Vector2(0, domRect.rect.height), Vector2.zero, true,
                                LastLeftNum, dominoCard.Value1);
                                XrightMost = rightMost;
                            }
                        }
                        else
                        {
                            if (dominoCard.Value1 == dominoCard.Value2 && addRight)
                            {
                                XrightMost -=new Vector2( domRect.rect.width,0);
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, domRect.rect.width / 2);
                                FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ,
                                new Vector3(XrightMost.x - posAmend.x, XrightMost.y - (domRect.rect.width / 2), 0),
                                Vector2.zero, Vector2.zero, true,
                                LastLeftNum, dominoCard.Value2);
                            }
                            else if (addRight && dominoCard.Value1 == LastRightNum && dominoCard.Value1 != dominoCard.Value2)
                            {
                               
                                Vector2 posAmend = new Vector2(domRect.rect.height / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Horizontal_ZA,
                                new Vector3(XrightMost.x - posAmend.x - (domRect.rect.height / 2), XrightMost.y - (domRect.rect.width / 2), 0),
                                 Vector2.zero, Vector2.zero, true,
                                 LastLeftNum, dominoCard.Value2);
                                XrightMost -= new Vector2(domRect.rect.height, 0);
                            }
                            else if (addRight && dominoCard.Value2 == LastRightNum && dominoCard.Value1 != dominoCard.Value2)
                            {
                               
                                Vector2 posAmend = new Vector2(domRect.rect.height / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ,
                                new Vector3(XrightMost.x - posAmend.x-( domRect.rect.height / 2), XrightMost.y - (domRect.rect.width / 2), 0),
                                 Vector2.zero, Vector2.zero, true,
                                 LastLeftNum, dominoCard.Value1);
                                XrightMost -= new Vector2(domRect.rect.height , 0);
                            }

                        }

                        if (Mathf.Abs(leftMost.y)<screenBound.y)
                        {
                            if (dominoCard.Value1 == dominoCard.Value2 && !addRight)
                            {
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, domRect.rect.width / 2);
                                FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ,
                                new Vector3(leftMost.x + posAmend.x, leftMost.y - (domRect.rect.width / 2), 0),
                                 Vector2.zero,-new Vector2(0, domRect.rect.width), false,
                                dominoCard.Value1, LastRightNum);
                                XleftMost = leftMost;
                                XleftMost.x += domRect.rect.width / 2;
                            }
                            else if (!addRight && dominoCard.Value2 == LastLeftNum && dominoCard.Value1 != dominoCard.Value2)
                            {
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ,
                                new Vector3(leftMost.x + posAmend.x, leftMost.y - (domRect.rect.height / 2), 0),
                                Vector2.zero,- new Vector2(0, domRect.rect.height), false,
                                dominoCard.Value1, LastRightNum);
                                XleftMost = leftMost;
                            }
                            else if (!addRight && dominoCard.Value1 == LastLeftNum && dominoCard.Value1 != dominoCard.Value2)
                            {
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Vertical_ZA,
                                new Vector3(leftMost.x + posAmend.x, leftMost.y - (domRect.rect.height / 2), 0),
                                Vector2.zero, -new Vector2(0, domRect.rect.height), false,
                                dominoCard.Value2, LastRightNum);
                                XleftMost = leftMost;
                            }
                        }
                        else
                        {
                            if (dominoCard.Value1 == dominoCard.Value2 && !addRight)
                            {
                                XleftMost += new Vector2(domRect.rect.width, 0);
                                Vector2 posAmend = new Vector2(domRect.rect.width / 2, domRect.rect.width / 2);
                                FirstCardPlacement(dominoCard, DominoDirection.Vertical_AZ,
                                new Vector3(XleftMost.x + posAmend.x, XleftMost.y + (domRect.rect.width / 2), 0),
                                Vector2.zero, Vector2.zero, false,
                                 dominoCard.Value2,LastRightNum);
                            }
                            else if (!addRight && dominoCard.Value1 == LastLeftNum && dominoCard.Value1 != dominoCard.Value2)
                            {

                                Vector2 posAmend = new Vector2(domRect.rect.height / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Horizontal_AZ,
                                new Vector3(XleftMost.x + posAmend.x + (domRect.rect.height / 2), XleftMost.y + (domRect.rect.width / 2), 0),
                                 Vector2.zero, Vector2.zero, false,
                                 dominoCard.Value2, LastRightNum);
                                XleftMost += new Vector2(domRect.rect.height, 0);
                            }
                            else if (!addRight && dominoCard.Value2 == LastLeftNum && dominoCard.Value1 != dominoCard.Value2)
                            {

                                Vector2 posAmend = new Vector2(domRect.rect.height / 2, 0);
                                FirstCardPlacement(dominoCard, DominoDirection.Horizontal_ZA,
                                new Vector3(XleftMost.x + posAmend.x + (domRect.rect.height / 2), XleftMost.y + (domRect.rect.width / 2), 0),
                                 Vector2.zero, Vector2.zero, false,
                                  dominoCard.Value1, LastRightNum);
                                XleftMost += new Vector2(domRect.rect.height, 0);
                            }

                        }

                    }

                }
            }
        }
    }

    void FirstCardPlacement(DominoCard dominoCard, DominoDirection dominoDirection, Vector3 localPosition,
    Vector2 addTorightMost, Vector2 addToLefttMost, bool AddLast, int TheLastLeftNum, int TheLastRightNum, float RightY = 0, float LeftY = 0)
    {
        dominoCard.transform.localScale = Vector3.one;
        dominoCard.ownership = GameRole.Board;
        dominoCard.transform.SetParent(GameBoard);
        RectTransform domRect = dominoCard.GetComponent<RectTransform>();
        dominoCard.DominoDirection = dominoDirection;
        domRect.localPosition = localPosition;
        rightMost += addTorightMost;
        leftMost += addToLefttMost;
        if(AddLast) linkedDominoes.AddLast(dominoCard); else linkedDominoes.AddFirst(dominoCard);
        LastLeftNum = TheLastLeftNum;
        LastRightNum = TheLastRightNum;
        if (RightY != 0) { rightMost.y = RightY; }
        if (LeftY  != 0) { leftMost.y  = LeftY; }

    }
  
    void CenterCards()
    {
        if (rightMost.x + Mathf.Abs(leftMost.x) < screenBound.x * 2)
        {
            float posAdj = linkedDominoes.Last.Value.GetComponent<RectTransform>().localPosition.x -
                ((linkedDominoes.Last.Value.GetComponent<RectTransform>().localPosition.x -
            linkedDominoes.First.Value.GetComponent<RectTransform>().localPosition.x) / 2);
            rightMost.x -= posAdj;
            leftMost.x -= posAdj;
            foreach (DominoCard D in linkedDominoes)
            {
                D.GetComponent<RectTransform>().localPosition = new Vector3(D.GetComponent<RectTransform>().localPosition.x - posAdj, D.GetComponent<RectTransform>().localPosition.y, 0);
            }
        }
    }
}
