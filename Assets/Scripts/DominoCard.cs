using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominoCard : MonoBehaviour
{
    public int spriteIndex;
    public int Value1;
    public int Value2 ;
    public bool s;
    private DominoDirection dominoDirection = DominoDirection.Vertical_AZ;
    private bool isObservableByAll = true;
    public GameRole ownership= GameRole.Board;

    
    public bool IsObservableByAll
    {
        get
        {
          return  isObservableByAll ;
        }
        set
        {
            isObservableByAll = value;
            if (isObservableByAll)
            {
              //  GetComponent<SpriteRenderer>().sprite = GameManager_S.CloneSprite(spriteIndex);
                GetComponent<Image>().sprite =  GameManager_S.CloneSprite(spriteIndex);

            }
            else
            {
              //  GetComponent<SpriteRenderer>().sprite = GameManager_S.CloneSprite(29);
                GetComponent<Image>().sprite = GameManager_S.CloneSprite(29);

            }
        }
    }

    public DominoDirection DominoDirection
    {
        get
        {
            return dominoDirection;
        }
        set
        {
            dominoDirection = value;
            switch (dominoDirection)
            {
                case DominoDirection.Vertical_AZ:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case DominoDirection.Vertical_ZA:
                    transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case DominoDirection.Horizontal_AZ:
                    transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case DominoDirection.Horizontal_ZA:
                    transform.eulerAngles = new Vector3(0, 0, 90);
                    break;

                default:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
            }
        }
    }

  
    public void ValuesArrange()
    {
        if (Value1 >= Value2)
        {
            int tmp1 = Value1;
            int tmp2 = Value2;
            this.Value1 = tmp2;
            this.Value2 = tmp1;
        }
    }


    void Start()
    {
     //   GetComponent<SpriteRenderer>().sprite = GameManager_S.CloneSprite(29);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
         //if (spriteIndex == 0)
         //   {

         //   GetComponent<RectTransform>().localPosition = Vector2.zero;
         //     //  GetComponent<RectTransform>().localPosition = Vector3.zero;​
         //       transform.localScale = Vector3.one;
         //       ownership = GameRole.Board;
            
         //  }

    }
}
