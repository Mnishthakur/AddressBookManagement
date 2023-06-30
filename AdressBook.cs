using System;
using System.Collections.Generic;
namespace AdressBookManagement
{
    class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

class AddressBook
{
    private List<Contact> contacts;

    public AddressBook()
    {
        contacts = new List<Contact>();
    }

    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
    }

    public void RemoveContact(string firstName, string lastName)
    {
        Contact contactToRemove = contacts.Find(c => c.FirstName == firstName && c.LastName == lastName);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
        }
    }

    public List<Contact> SearchByCity(string city)
    {
        return contacts.FindAll(c => c.City == city);
    }

    public List<Contact> SearchByState(string state)
    {
        return contacts.FindAll(c => c.State == state);
    }

    public void DisplayContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found.");
            return;
        }

        foreach (var contact in contacts)
        {
            Console.WriteLine("Contact Name: {0} {1}", contact.FirstName, contact.LastName);
            Console.WriteLine("Address: {0}", contact.Address);
            Console.WriteLine("City: {0}", contact.City);
            Console.WriteLine("State: {0}", contact.State);
            Console.WriteLine("ZIP Code: {0}", contact.ZipCode);
            Console.WriteLine("Phone Number: {0}", contact.PhoneNumber);
            Console.WriteLine("Email: {0}\n", contact.Email);
        }
    }
}

class AddressBookSystem
{
    private Dictionary<string, AddressBook> addressBooks;
    private Dictionary<string, List<Contact>> cityToContacts;
    private Dictionary<string, List<Contact>> stateToContacts;

    public AddressBookSystem()
    {
        addressBooks = new Dictionary<string, AddressBook>();
        cityToContacts = new Dictionary<string, List<Contact>>();
        stateToContacts = new Dictionary<string, List<Contact>>();
    }

    public void AddAddressBook(string addressBookName)
    {
        AddressBook newAddressBook = new AddressBook();
        addressBooks.Add(addressBookName, newAddressBook);
        Console.WriteLine("Address Book '{0}' created successfully.", addressBookName);
    }

    public void AddContact(string addressBookName, Contact contact)
    {
        if (!addressBooks.ContainsKey(addressBookName))
        {
            Console.WriteLine("Address Book not found. Please create the Address Book first.");
            return;
        }

        AddressBook addressBook = addressBooks[addressBookName];

        if (addressBook.SearchByCity(contact.City).Count > 0)
        {
            Console.WriteLine("Duplicate entry. Contact already exists in the specified city.");
            return;
        }

        if (addressBook.SearchByState(contact.State).Count > 0)
        {
            Console.WriteLine("Duplicate entry. Contact already exists in the specified state.");
            return;
        }

        addressBook.AddContact(contact);

        // Update cityToContacts dictionary
        if (!cityToContacts.ContainsKey(contact.City))
        {
            cityToContacts[contact.City] = new List<Contact>();
        }
        cityToContacts[contact.City].Add(contact);

        // Update stateToContacts dictionary
        if (!stateToContacts.ContainsKey(contact.State))
        {
            stateToContacts[contact.State] = new List<Contact>();
        }
        stateToContacts[contact.State].Add(contact);

        Console.WriteLine("Contact added successfully.");
    }

    public void RemoveContact(string addressBookName, string firstName, string lastName)
    {
        if (!addressBooks.ContainsKey(addressBookName))
        {
            Console.WriteLine("Address Book not found. Please create the Address Book first.");
            return;
        }

        AddressBook addressBook = addressBooks[addressBookName];
        addressBook.RemoveContact(firstName, lastName);

        // Remove contact from cityToContacts dictionary
        foreach (var cityContacts in cityToContacts.Values)
        {
            Contact contactToRemove = cityContacts.Find(c => c.FirstName == firstName && c.LastName == lastName);
            if (contactToRemove != null)
            {
                cityContacts.Remove(contactToRemove);
                break;
            }
        }

        // Remove contact from stateToContacts dictionary
        foreach (var stateContacts in stateToContacts.Values)
        {
            Contact contactToRemove = stateContacts.Find(c => c.FirstName == firstName && c.LastName == lastName);
            if (contactToRemove != null)
            {
                stateContacts.Remove(contactToRemove);
                break;
            }
        }

        Console.WriteLine("Contact removed successfully.");
    }

    public void SearchByCity(string city)
    {
        if (!cityToContacts.ContainsKey(city))
        {
            Console.WriteLine("No contacts found in the specified city.");
            return;
        }

        List<Contact> contactsInCity = cityToContacts[city];
        int contactCount = contactsInCity.Count;

        Console.WriteLine("Search results in the specified city '{0}':", city);
        Console.WriteLine("Contact Count: {0}", contactCount);

        foreach (var contact in contactsInCity)
        {
            Console.WriteLine("Contact Name: {0} {1}", contact.FirstName, contact.LastName);
            Console.WriteLine("Address: {0}", contact.Address);
            Console.WriteLine("City: {0}", contact.City);
            Console.WriteLine("State: {0}", contact.State);
            Console.WriteLine("ZIP Code: {0}", contact.ZipCode);
            Console.WriteLine("Phone Number: {0}", contact.PhoneNumber);
            Console.WriteLine("Email: {0}\n", contact.Email);
        }
    }

    public void SearchByState(string state)
    {
        if (!stateToContacts.ContainsKey(state))
        {
            Console.WriteLine("No contacts found in the specified state.");
            return;
        }

        List<Contact> contactsInState = stateToContacts[state];
        int contactCount = contactsInState.Count;

        Console.WriteLine("Search results in the specified state '{0}':", state);
        Console.WriteLine("Contact Count: {0}", contactCount);

        foreach (var contact in contactsInState)
        {
            Console.WriteLine("Contact Name: {0} {1}", contact.FirstName, contact.LastName);
            Console.WriteLine("Address: {0}", contact.Address);
            Console.WriteLine("City: {0}", contact.City);
            Console.WriteLine("State: {0}", contact.State);
            Console.WriteLine("ZIP Code: {0}", contact.ZipCode);
            Console.WriteLine("Phone Number: {0}", contact.PhoneNumber);
            Console.WriteLine("Email: {0}\n", contact.Email);
        }
    }

    public int GetContactCountByCity(string city)
    {
        if (!cityToContacts.ContainsKey(city))
        {
            return 0;
        }

        List<Contact> contactsInCity = cityToContacts[city];
        return contactsInCity.Count;
    }

    public int GetContactCountByState(string state)
    {
        if (!stateToContacts.ContainsKey(state))
        {
            return 0;
        }

        List<Contact> contactsInState = stateToContacts[state];
        return contactsInState.Count;
    }
}

