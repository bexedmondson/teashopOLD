using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLibrary : MonoBehaviour {

	public List<Customer> customers;

	private List<Customer> unservedCustomers = new List<Customer>();

	private void Awake()
	{
		foreach (Customer c in customers)
			unservedCustomers.Add(c);
	}   

	public Customer GetRandomUnservedCustomer()
	{      
		Customer customer = null;

		if (unservedCustomers.Count == 0)
			return customer;

		customer = unservedCustomers[Random.Range(0, unservedCustomers.Count)];

		return customer;
    }
    
	public void MarkCustomerServed(Customer customer)
	{
        customer.beenServed = true;
		unservedCustomers.Remove(customer);
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
