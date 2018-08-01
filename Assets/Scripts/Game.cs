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

	private Color selectedColour = new Color(0.55f, 0.82f, 0.96f);
    private Color unselectedColour = new Color(1f, 1f, 1f);
    
	private Ingredient chosenIngredient = null;

	public Button teaChosenButton;

	public GameObject resultScreen;
	public Text resultText;

	void Start()
	{
		customerLibrary = GetComponent<CustomerLibrary>();
		ingredientLibrary = GetComponent<IngredientLibrary>();

		if (customerLibrary.IsValid() && ingredientLibrary.IsValid())
			CustomerArrives();
		else
			QuitGame();
	}

	private void CustomerArrives()
	{
		currentCustomer = customerLibrary.GetRandomUnservedCustomer();

		if (currentCustomer == null)
		{
			ShowResults();
			return;
		}

		SoundManager.instance.PlaySingle("shop_door_bell");

		teaScreen.SetActive(false);
		resultScreen.SetActive(false);

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
        
		if (ingredient.itemName == "Green Tea")
		{
			greenTeaButton.image.color = selectedColour;
			rooibosButton.image.color = unselectedColour;
			chamomileButton.image.color = unselectedColour;
		}
		else if (ingredient.itemName == "Rooibos")
		{
			greenTeaButton.image.color = unselectedColour;
			rooibosButton.image.color = selectedColour;
			chamomileButton.image.color = unselectedColour;
		}
		else if (ingredient.itemName == "Chamomile")
		{
			greenTeaButton.image.color = unselectedColour;
			rooibosButton.image.color = unselectedColour;
			chamomileButton.image.color = selectedColour;
		}
	}
    
	public void TeaChosen()
	{
		greenTeaButton.image.color = unselectedColour;
        rooibosButton.image.color = unselectedColour;
		chamomileButton.image.color = unselectedColour;
		teaScreen.SetActive(false);
		speechText.text = currentCustomer.thankYou;

		SetSuccess();

		customerLibrary.MarkCustomerServed(currentCustomer);
	}

	private void SetSuccess()
	{
		if (currentCustomer.insomniaLevel <= chosenIngredient.insomniaRelief
			&& currentCustomer.stressLevel <= chosenIngredient.stressRelief)
		{
			currentCustomer.successfulTea = true;
		}
		else
		{
			currentCustomer.successfulTea = false;
		}
	}

	public void ShowResults()
	{
		resultScreen.SetActive(true);

		string resultString = "";

		foreach (Customer customer in customerLibrary.customers)
		{
			if (customer.beenServed)
			{
				resultString += customer.firstName;
				resultString += ": ";

				if (customer.successfulTea)
					resultString += "your tea solved their problem!";
				else
					resultString += "your tea didn't solve their problem this time.";

				resultString += "\n";
			}
		}

		resultText.text = resultString;
	}

	public void QuitGame()
	{
		// save any game data here

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}
}
