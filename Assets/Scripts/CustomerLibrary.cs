using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLibrary : MonoBehaviour {

	public List<Customer> customers;
    
   
    public Customer GetRandomCustomer()
    {
		return customers[Random.Range(0, customers.Count)];
    }

	public bool IsValid() //TODO: move this to be an editor check instead
	{
		if (customers.Count == 0)
			return false;

		foreach (Customer c in customers)
		{
			if (!c.IsValid())
				return false;
		}

		return true;
	}
}
