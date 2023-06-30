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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Contact otherContact = (Contact)obj;
            return string.Equals(FirstName, otherContact.FirstName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(LastName, otherContact.LastName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
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
            if (contacts.Contains(contact))
            {
                Console.WriteLine("Duplicate entry. Contact already exists in the Address Book.");
                return;
            }

            contacts.Add(contact);
            Console.WriteLine("Contact added successfully.");
        }

        public void RemoveContact(string firstName, string lastName)
        {
            Contact contactToRemove = FindContactByName(firstName, lastName);

            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public List<Contact> SearchByCity(string city)
        {
            return contacts.FindAll(contact => string.Equals(contact.City, city, StringComparison.OrdinalIgnoreCase));
        }

        public List<Contact> SearchByState(string state)
        {
            return contacts.FindAll(contact => string.Equals(contact.State, state, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
            }
            else
            {
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

        private Contact FindContactByName(string firstName, string lastName)
        {
            return contacts.Find(contact => string.Equals(contact.FirstName, firstName, StringComparison.OrdinalIgnoreCase) &&
                                             string.Equals(contact.LastName, lastName, StringComparison.OrdinalIgnoreCase));
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
            if (addressBooks.ContainsKey(addressBookName))
            {
                Console.WriteLine("Address Book with the same name already exists. Please choose a different name.");
                return;
            }

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
        }

        public void RemoveContact(string addressBookName, string firstName, string lastName)
        {
            if (!addressBooks.ContainsKey(addressBookName))
            {
                Console.WriteLine("Address Book not found. Please create the Address Book first.");
                return;
            }

            AddressBook addressBook = addressBooks[addressBookName];
            Contact contactToRemove = addressBook.SearchByCity(firstName, lastName).FirstOrDefault();

            if (contactToRemove == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            addressBook.RemoveContact(contactToRemove.FirstName, contactToRemove.LastName);

            // Update cityToContacts dictionary
            if (cityToContacts.ContainsKey(contactToRemove.City))
            {
                cityToContacts[contactToRemove.City].Remove(contactToRemove);
            }

            // Update stateToContacts dictionary
            if (stateToContacts.ContainsKey(contactToRemove.State))
            {
                stateToContacts[contactToRemove.State].Remove(contactToRemove);
            }
        }

        public void SearchByCity(string city)
        {
            if (!cityToContacts.ContainsKey(city))
            {
                Console.WriteLine("No contacts found in the specified city.");
                return;
            }

            List<Contact> contactsInCity = cityToContacts[city];

            Console.WriteLine("Search results in the specified city '{0}':", city);
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

            Console.WriteLine("Search results in the specified state '{0}':", state);
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

        public void DisplayAddressBook(string addressBookName)
        {
            if (!addressBooks.ContainsKey(addressBookName))
            {
                Console.WriteLine("Address Book not found. Please create the Address Book first.");
                return;
            }

            AddressBook addressBook = addressBooks[addressBookName];
            Console.WriteLine("Contacts in Address Book '{0}':", addressBookName);
            addressBook.DisplayContacts();
        }
    }
}

