using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdressBookManagement
{
    public class AddressBookSystem
{
    private Dictionary<string, AddressBook> addressBooks;

    public AddressBookSystem()
    {
        addressBooks = new Dictionary<string, AddressBook>();
    }

    public void CreateAddressBook(string addressBookName)
    {
        AddressBook addressBook = new AddressBook();
        addressBooks.Add(addressBookName, addressBook);
    }

    public void AddContactToAddressBook(string addressBookName, Contact contact)
    {
        if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
        {
            if (addressBook.AddContact(contact))
            {
                Console.WriteLine("Contact added successfully.");
            }
            else
            {
                Console.WriteLine("Contact with the same name already exists.");
            }
        }
        else
        {
            Console.WriteLine($"Address book '{addressBookName}' not found.");
        }
    }

    public void EditContactInAddressBook(string addressBookName, string firstName, string lastName, Contact newContact)
    {
        if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
        {
            if (addressBook.EditContact(firstName, lastName, newContact))
            {
                Console.WriteLine("Contact edited successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        else
        {
            Console.WriteLine($"Address book '{addressBookName}' not found.");
        }
    }

    public void DeleteContactFromAddressBook(string addressBookName, string firstName, string lastName)
    {
        if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
        {
            if (addressBook.DeleteContact(firstName, lastName))
            {
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        else
        {
            Console.WriteLine($"Address book '{addressBookName}' not found.");
        }
    }

    public void SearchByCity(string city)
    {
        bool found = false;
        foreach (AddressBook addressBook in addressBooks.Values)
        {
            List<Contact> contacts = addressBook.SearchByCity(city);
            if (contacts.Count > 0)
            {
                Console.WriteLine($"Contacts in {city}:");
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine(contact);
                }
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No contacts found in the specified city.");
        }
    }

    public void SearchByState(string state)
    {
        bool found = false;
        foreach (AddressBook addressBook in addressBooks.Values)
        {
            List<Contact> contacts = addressBook.SearchByState(state);
            if (contacts.Count > 0)
            {
                Console.WriteLine($"Contacts in {state}:");
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine(contact);
                }
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No contacts found in the specified state.");
        }
    }

    public void GetContactCountByCity(string city)
    {
        int count = 0;
        foreach (AddressBook addressBook in addressBooks.Values)
        {
            count += addressBook.GetContactCountByCity(city);
        }
        Console.WriteLine($"Contact count in {city}: {count}");
    }

    public void GetContactCountByState(string state)
    {
        int count = 0;
        foreach (AddressBook addressBook in addressBooks.Values)
        {
            count += addressBook.GetContactCountByState(state);
        }
        Console.WriteLine($"Contact count in {state}: {count}");
    }

    public void SortAddressBookByName(string addressBookName)
    {
        if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
        {
            addressBook.SortByName();
            Console.WriteLine($"Address book '{addressBookName}' sorted by name.");
            Console.WriteLine("Sorted Contacts:");
            foreach (Contact contact in addressBook.GetAllContacts())
            {
                Console.WriteLine(contact);
            }
        }
        else
        {
            Console.WriteLine($"Address book '{addressBookName}' not found.");
        }
    }

    public void SaveAddressBookToFile(string addressBookName, string filePath)
    {
        if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
        {
            string json = JsonConvert.SerializeObject(addressBook.GetAllContacts(), Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Address book '{addressBookName}' saved to file '{filePath}'.");
        }
        else
        {
            Console.WriteLine($"Address book '{addressBookName}' not found.");
        }
    }

    public void LoadAddressBookFromFile(string addressBookName, string filePath)
    {
        if (addressBooks.ContainsKey(addressBookName))
        {
            Console.WriteLine($"Address book '{addressBookName}' already exists. Loading contacts from file '{filePath}' will replace existing contacts.");
        }

        try
        {
            string json = File.ReadAllText(filePath);
            List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            if (contacts != null)
            {
                if (addressBooks.ContainsKey(addressBookName))
                {
                    addressBooks[addressBookName].ClearContacts();
                }
                else
                {
                    addressBooks.Add(addressBookName, new AddressBook());
                }

                addressBooks[addressBookName].AddContacts(contacts);

                Console.WriteLine($"Address book '{addressBookName}' loaded from file '{filePath}'.");
            }
            else
            {
                Console.WriteLine("Error: Invalid file format or no contacts found in the file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading address book '{addressBookName}' from file '{filePath}': {ex.Message}");
        }
    }

    public void PrintAllAddressBooks()
    {
        foreach (var entry in addressBooks)
        {
            Console.WriteLine($"Address Book Name: {entry.Key}");
            AddressBook addressBook = entry.Value;
            foreach (Contact contact in addressBook.GetAllContacts())
            {
                Console.WriteLine(contact);
            }
            Console.WriteLine();
        }
    }
}

public class AddressBook
{
    private List<Contact> contacts;

    public AddressBook()
    {
        contacts = new List<Contact>();
    }

    public bool AddContact(Contact contact)
    {
        if (!ContactExists(contact))
        {
            contacts.Add(contact);
            return true;
        }
        return false;
    }

    private bool ContactExists(Contact contact)
    {
        return contacts.Exists(c => c.FirstName.Equals(contact.FirstName, StringComparison.OrdinalIgnoreCase) &&
                                   c.LastName.Equals(contact.LastName, StringComparison.OrdinalIgnoreCase));
    }

    public bool EditContact(string firstName, string lastName, Contact newContact)
    {
        Contact existingContact = contacts.Find(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                                     c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        if (existingContact != null)
        {
            existingContact.FirstName = newContact.FirstName;
            existingContact.LastName = newContact.LastName;
            existingContact.Address = newContact.Address;
            existingContact.City = newContact.City;
            existingContact.State = newContact.State;
            existingContact.ZipCode = newContact.ZipCode;
            existingContact.PhoneNumber = newContact.PhoneNumber;
            existingContact.Email = newContact.Email;
            return true;
        }
        return false;
    }

    public bool DeleteContact(string firstName, string lastName)
    {
        Contact contact = contacts.Find(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                             c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        if (contact != null)
        {
            contacts.Remove(contact);
            return true;
        }
        return false;
    }

    public List<Contact> SearchByCity(string city)
    {
        return contacts.FindAll(c => c.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    }

    public List<Contact> SearchByState(string state)
    {
        return contacts.FindAll(c => c.State.Equals(state, StringComparison.OrdinalIgnoreCase));
    }

    public int GetContactCountByCity(string city)
    {
        return contacts.Count(c => c.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    }

    public int GetContactCountByState(string state)
    {
        return contacts.Count(c => c.State.Equals(state, StringComparison.OrdinalIgnoreCase));
    }

    public void SortByName()
    {
        contacts.Sort((c1, c2) => string.Compare(c1.FirstName, c2.FirstName));
    }

    public List<Contact> GetAllContacts()
    {
        return contacts;
    }

    public void AddContacts(List<Contact> contacts)
    {
        this.contacts.AddRange(contacts);
    }

    public void ClearContacts()
    {
        contacts.Clear();
    }
}

public class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"Name: {FirstName} {LastName}, Address: {Address}, City: {City}, State: {State}, Zip: {ZipCode}, Phone: {PhoneNumber}, Email: {Email}";
    }
}

