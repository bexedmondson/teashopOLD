using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CustomerLibrary))]
[RequireComponent(typeof(IngredientLibrary))]
public class Game : MonoBehaviour
{   
	public static Game instance = null;

    private void Awake()
    {
        //singleton time!
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


	public CustomerLibrary customerLibrary;
    
    public IngredientLibrary ingredientLibrary;

	private Customer currentCustomer;

	//TODO make a ui system because this is gross
	public GameObject teaScreen;

	public Text speechText;

	public Image speakerSprite;
    
	public Button greenTeaButton;
	public Button rooibosButton;
	public Button chamomileButton;
    
	private Ingredient chosenIngredient = null;

	public Button teaChosenButton;

	void Start()
	{
		customerLibrary = GetComponent<CustomerLibrary>();
		ingredientLibrary = GetComponent<IngredientLibrary>();

		if (customerLibrary.IsValid() && ingredientLibrary.IsValid())
		{
			Debug.Log("valid");
			CustomerArrives();
		}
		else
		{
			Debug.Log("not valid");
			QuitGame();
		}
	}

	private void CustomerArrives()
	{
		currentCustomer = customerLibrary.GetRandomCustomer();

		SoundManager.instance.PlaySingle("shop_door_bell");

		teaScreen.SetActive(false);

		speechText.text = currentCustomer.firstEnquiry;
		speakerSprite.sprite = currentCustomer.sprite;
	}

	public void SpeechNext()
	{
		if (chosenIngredient == null)
		{
			teaScreen.SetActive(true);
			teaChosenButton.interactable = false;
		}
		else
		{
			chosenIngredient = null;
			CustomerArrives();
		}
	}

	public void ChooseTea(Ingredient ingredient)
	{
		chosenIngredient = ingredient;

		teaChosenButton.interactable = true;

		Color selected = new Color(0.55f, 0.82f, 0.96f);
		Color unselected = new Color(1f, 1f, 1f);
        
		if (ingredient.itemName == "Green Tea")
		{
			greenTeaButton.image.color = selected;
			rooibosButton.image.color = unselected;
			chamomileButton.image.color = unselected;
		}
		else if (ingredient.itemName == "Rooibos")
		{
			greenTeaButton.image.color = unselected;
			rooibosButton.image.color = selected;
			chamomileButton.image.color = unselected;
		}
		else if (ingredient.itemName == "Chamomile")
		{
			greenTeaButton.image.color = unselected;
			rooibosButton.image.color = unselected;
			chamomileButton.image.color = selected;
		}
	}

	public void TeaChosen()
	{
		teaScreen.SetActive(false);
		speechText.text = currentCustomer.thankYou;
	}

	private void QuitGame()
	{
		// save any game data here

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}
}
