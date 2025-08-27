using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class WinCondition
{
    public List <Item> requiredItens;
    public List <Item> interacted;
    public bool alreadyPlayed;
}


public class PlayerInventory : MonoBehaviour
{
    public WinCondition[] winConditions;
    public List<Item> itens;

    public void AddItem(Item item)
    {
        if (itens.Contains(item))
        {
            return;
        }
        UIManager.instance.SetItens(item, itens.Count);
        itens.Add(item);
    }
}