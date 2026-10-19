using UnityEngine;
using System.Collections.Generic;

public class OnClickWorld : MonoBehaviour
{
    public List<TriggerActionSO> TriggerActions = new List<TriggerActionSO>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D Hit = Physics2D.Raycast(WorldPosition, Vector2.zero);

            if (Hit.collider != null && Hit.collider.gameObject == gameObject)
            {
                TriggerManager.Instance.Execute(TriggerActions);
            }
        }
    }
}