using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		//if(eventData.pointerDrag == null)
		//	return;

		//Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		//if(d != null) {
		//	d.placeholderParent = this.transform;
		//}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		////Debug.Log("OnPointerExit");
		//if(eventData.pointerDrag == null)
		//	return;

		//Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		//if(d != null && d.placeholderParent==this.transform) {
		//	d.placeholderParent = d.parentToReturnTo;
		//}
	}
	
	public void OnDrop(PointerEventData eventData) {
		
		DominoCard d = eventData.pointerDrag.GetComponent<DominoCard>();
		Debug.Log(GameManager_S.Instance.LastLeftNum + " / " + GameManager_S.Instance.LastRightNum
			+ " / "+d.Value1 + " / "+d.Value2);

		if (GameManager_S.Instance.linkedDominoes.Count > 0)
		{

			if ((d.Value1 == GameManager_S.Instance.LastLeftNum && d.Value2 == GameManager_S.Instance.LastRightNum) ||
				(d.Value1 == GameManager_S.Instance.LastRightNum && d.Value2 == GameManager_S.Instance.LastLeftNum))
			{
				DominoCard L = GameManager_S.Instance.linkedDominoes.First.Value;
				DominoCard R = GameManager_S.Instance.linkedDominoes.Last.Value;
				Debug.Log("fits both sides");

				if (Vector2.Distance(L.transform.position, eventData.position) <
					Vector2.Distance(R.transform.position, eventData.position))
				{
					GameManager_S.Instance.SettleTheCardInTheBoard(d.spriteIndex, false);
				}
				else
				{
					GameManager_S.Instance.SettleTheCardInTheBoard(d.spriteIndex, true);
				}


			}
			else if ((d.Value1 == GameManager_S.Instance.LastLeftNum || d.Value1 == GameManager_S.Instance.LastRightNum) ||
				(d.Value2 == GameManager_S.Instance.LastRightNum || d.Value2 == GameManager_S.Instance.LastLeftNum))
			{
				Debug.Log("one side only");
				if ((GameManager_S.Instance.LastRightNum == d.Value1) ||(GameManager_S.Instance.LastRightNum == d.Value2))
				{
					GameManager_S.Instance.SettleTheCardInTheBoard(d.spriteIndex, true);
				}
				if ((GameManager_S.Instance.LastLeftNum == d.Value1) ||
					(GameManager_S.Instance.LastLeftNum == d.Value2))
				{
					GameManager_S.Instance.SettleTheCardInTheBoard(d.spriteIndex, false);
				}
			}
			else
			{
				Debug.Log("does not match");

			}

		}
		else 
		{
			GameManager_S.Instance.SettleTheCardInTheBoard(d.spriteIndex, true);
		}

		//Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		//if(d != null) {
		//	d.parentToReturnTo = this.transform;
		//}

	}
}
