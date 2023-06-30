using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdressBookManagement
{
class Contact : IComparable<Contact>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public int CompareTo(Contact other)
    {
        if (other == null)
            return 1;
        
        int result = string.Compare(City, other.City, StringComparison.OrdinalIgnoreCase);
        if (result == 0)
            result = string.Compare(State, other.State, StringComparison.OrdinalIgnoreCase);
        if (result == 0)
            result = string.Compare(ZipCode, other.ZipCode, StringComparison.OrdinalIgnoreCase);
        if (result == 0)
            result = string.Compare(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
        if (result == 0)
            result = string.Compare(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase);

        return result;
    }

    public override string ToString()
    {
        return $"Contact Name: {FirstName} {LastName}\n" +
               $"Address: {Address}\n" +
               $"City: {City}\n" +
               $"State: {State}\n" +
               $"ZIP Code: {ZipCode}\n" +
               $"Phone Number: {PhoneNumber}\n" +
               $"Email: {Email}\n";
    }
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
        contacts.Sort(); // Sort the contacts alphabetically by default
    }

    public void SortByCity()
    {
        contacts = contacts.OrderBy(c => c.City).ToList();
    }

    public void SortByState()
    {
        contacts = contacts.OrderBy(c => c.State).ToList();
    }

    public void SortByZipCode()
    {
        contacts = contacts.OrderBy(c => c.ZipCode).ToList();
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
            Console.WriteLine(contact.ToString());
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

        // Add contact to cityToContacts dictionary
        if (!cityToContacts.ContainsKey(contact.City))
        {
            cityToContacts[contact.City] = new List<Contact>();
        }
        cityToContacts[contact.City].Add(contact);

        // Add contact to stateToContacts dictionary
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

    public List<Contact> SearchByCity(string city)
    {
        if (!cityToContacts.ContainsKey(city))
        {
            return new List<Contact>();
        }

        return cityToContacts[city];
    }

    public List<Contact> SearchByState(string state)
    {
        if (!stateToContacts.ContainsKey(state))
        {
            return new List<Contact>();
        }

        return stateToContacts[state];
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

    public void SaveToFile(string fileName)
{
    using (StreamWriter writer = new StreamWriter(fileName))
    {
        foreach (AddressBook addressBook in addressBooks.Values)
        {
            foreach (Contact contact in addressBook.GetContacts())
            {
                writer.WriteLine($"{contact.FirstName},{contact.LastName},{contact.Address},{contact.City},{contact.State},{contact.ZipCode},{contact.PhoneNumber},{contact.Email}");
            }
        }
    }

    Console.WriteLine("Address Book saved to file successfully.");
}

public void LoadFromFile(string fileName)
{
    if (!File.Exists(fileName))
    {
        Console.WriteLine("File does not exist.");
        return;
    }

    ClearAddressBooks();

    using (StreamReader reader = new StreamReader(fileName))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(',');

            if (parts.Length == 8)
            {
                string firstName = parts[0];
                string lastName = parts[1];
                string address = parts[2];
                string city = parts[3];
                string state = parts[4];
                string zipCode = parts[5];
                string phoneNumber = parts[6];
                string email = parts[7];

                Contact contact = new Contact()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    City = city,
                    State = state,
                    ZipCode = zipCode,
                    PhoneNumber = phoneNumber,
                    Email = email
                };

                AddContactToAddressBook(contact);
            }
        }
    }

    Console.WriteLine("Address Book loaded from file successfully.");
}

private void ClearAddressBooks()
{
    foreach (AddressBook addressBook in addressBooks.Values)
    {
        addressBook.ClearContacts();
    }

    addressBooks.Clear();
    cityToContacts.Clear();
    stateToContacts.Clear();
}
}
}

