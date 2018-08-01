using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TeaShop/Ingredient")]
public class Ingredient : ScriptableObject 
{
	public string itemName;

	public int stressRelief;

	public int insomniaRelief;

	public bool IsValid() //TODO: move this to be an editor check instead
    {
		return !string.IsNullOrEmpty(itemName);
    }
}
