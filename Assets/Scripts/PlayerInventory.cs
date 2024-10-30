using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Permite que la clase se muestre en el Inspector
public class InventoryItem
{
    public string itemName; // Nombre del objeto
    public int quantity; // Cantidad de este objeto
}

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>(); // Lista de objetos recogidos

    // Método para añadir un objeto al inventario
    public void AddItem(string itemName)
    {
        // Verificar si el objeto ya está en el inventario
        InventoryItem existingItem = items.Find(item => item.itemName == itemName);

        if (existingItem != null)
        {
            // Si existe, aumentar la cantidad
            existingItem.quantity++;
        }
        else
        {
            // Si no existe, añadirlo con cantidad 1
            items.Add(new InventoryItem { itemName = itemName, quantity = 1 });
        }

        Debug.Log($"Objeto '{itemName}' añadido al inventario. Total de objetos: {items.Count}");
    }

    // Método para verificar si el inventario contiene un objeto específico
    public bool HasItem(string itemName)
    {
        return items.Exists(item => item.itemName == itemName);
    }

    // Método para obtener la lista de todos los objetos en el inventario
    public List<InventoryItem> GetItems()
    {
        return items;
    }
}
