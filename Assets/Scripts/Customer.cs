using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TeaShop/Customer")]
public class Customer : MonoBehaviour 
{   
	public string firstName;

	public Sprite sprite;

	public int stressLevel;

	public int insomniaLevel;

    public string firstEnquiry;

	public string thankYou;

	public bool beenServed;

	public bool successfulTea;

	public bool IsValid() //TODO: move this to be an editor check instead
	{
		return !string.IsNullOrEmpty(firstName);
	}
}
