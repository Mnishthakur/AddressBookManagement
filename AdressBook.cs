using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdressBookManagement
{
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
    }

    public class AddressBook
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

        public bool EditContact(string firstName, string lastName, Contact newContact)
        {
            Contact existingContact = FindContact(firstName, lastName);
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
            Contact contact = FindContact(firstName, lastName);
            if (contact != null)
            {
                contacts.Remove(contact);
                return true;
            }
            return false;
        }

        public Contact FindContact(string firstName, string lastName)
        {
            return contacts.FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                                && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Contact> SearchContactsByCity(string city)
        {
            return contacts.Where(c => c.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Contact> SearchContactsByState(string state)
        {
            return contacts.Where(c => c.State.Equals(state, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void DisplayContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("Contacts:");
            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Address: {contact.Address}");
                Console.WriteLine($"City: {contact.City}");
                Console.WriteLine($"State: {contact.State}");
                Console.WriteLine($"Zip: {contact.ZipCode}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine();
            }
        }

        public List<Contact> GetAllContacts()
        {
            return contacts;
        }

        public void SortByName()
        {
            contacts = contacts.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();
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
    }

    public class AddressBookSystem
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

        public void CreateAddressBook(string addressBookName)
        {
            AddressBook addressBook = new AddressBook();
            addressBooks.Add(addressBookName, addressBook);
        }

        public void AddContactToAddressBook(string addressBookName, Contact contact)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                if (IsDuplicateContact(contact))
                {
                    Console.WriteLine("Contact with the same name already exists. Duplicate entry not allowed.");
                    return;
                }

                addressBook.AddContact(contact);
                UpdateCityAndStateMappings(contact);
                Console.WriteLine("Contact added successfully.");
            }
            else
            {
                Console.WriteLine($"Address book '{addressBookName}' not found.");
            }
        }

        private bool IsDuplicateContact(Contact contact)
        {
            foreach (AddressBook addressBook in addressBooks.Values)
            {
                Contact existingContact = addressBook.FindContact(contact.FirstName, contact.LastName);
                if (existingContact != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateCityAndStateMappings(Contact contact)
        {
            if (!string.IsNullOrEmpty(contact.City))
            {
                if (cityToContacts.ContainsKey(contact.City))
                {
                    cityToContacts[contact.City].Add(contact);
                }
                else
                {
                    cityToContacts[contact.City] = new List<Contact> { contact };
                }
            }

            if (!string.IsNullOrEmpty(contact.State))
            {
                if (stateToContacts.ContainsKey(contact.State))
                {
                    stateToContacts[contact.State].Add(contact);
                }
                else
                {
                    stateToContacts[contact.State] = new List<Contact> { contact };
                }
            }
        }

        public void EditContactInAddressBook(string addressBookName, string firstName, string lastName, Contact newContact)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                bool isContactEdited = addressBook.EditContact(firstName, lastName, newContact);
                if (isContactEdited)
                {
                    UpdateCityAndStateMappings(newContact);
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
                bool isContactDeleted = addressBook.DeleteContact(firstName, lastName);
                if (isContactDeleted)
                {
                    RemoveFromCityAndStateMappings(firstName, lastName);
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

        private void RemoveFromCityAndStateMappings(string firstName, string lastName)
        {
            foreach (var cityContacts in cityToContacts)
            {
                List<Contact> contacts = cityContacts.Value;
                Contact contact = contacts.FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                                                && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
                if (contact != null)
                {
                    contacts.Remove(contact);
                    break;
                }
            }

            foreach (var stateContacts in stateToContacts)
            {
                List<Contact> contacts = stateContacts.Value;
                Contact contact = contacts.FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                                                && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
                if (contact != null)
                {
                    contacts.Remove(contact);
                    break;
                }
            }
        }

        public void SearchByCity(string city)
        {
            if (cityToContacts.ContainsKey(city))
            {
                List<Contact> contacts = cityToContacts[city];
                Console.WriteLine($"Contacts in city '{city}':");
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                    Console.WriteLine($"Address: {contact.Address}");
                    Console.WriteLine($"City: {contact.City}");
                    Console.WriteLine($"State: {contact.State}");
                    Console.WriteLine($"Zip: {contact.ZipCode}");
                    Console.WriteLine($"Phone: {contact.PhoneNumber}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"No contacts found in city '{city}'.");
            }
        }

        public void SearchByState(string state)
        {
            if (stateToContacts.ContainsKey(state))
            {
                List<Contact> contacts = stateToContacts[state];
                Console.WriteLine($"Contacts in state '{state}':");
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                    Console.WriteLine($"Address: {contact.Address}");
                    Console.WriteLine($"City: {contact.City}");
                    Console.WriteLine($"State: {contact.State}");
                    Console.WriteLine($"Zip: {contact.ZipCode}");
                    Console.WriteLine($"Phone: {contact.PhoneNumber}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"No contacts found in state '{state}'.");
            }
        }

        public void GetContactCountByCity(string city)
        {
            if (cityToContacts.ContainsKey(city))
            {
                int count = cityToContacts[city].Count;
                Console.WriteLine($"Number of contacts in city '{city}': {count}");
            }
            else
            {
                Console.WriteLine($"No contacts found in city '{city}'.");
            }
        }

        public void GetContactCountByState(string state)
        {
            if (stateToContacts.ContainsKey(state))
            {
                int count = stateToContacts[state].Count;
                Console.WriteLine($"Number of contacts in state '{state}': {count}");
            }
            else
            {
                Console.WriteLine($"No contacts found in state '{state}'.");
            }
        }

        public void SortAddressBookByName(string addressBookName)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                addressBook.SortByName();
                Console.WriteLine("Address book sorted by name.");
            }
            else
            {
                Console.WriteLine($"Address book '{addressBookName}' not found.");
            }
        }

        public void SortAddressBookByCity(string addressBookName)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                addressBook.SortByCity();
                Console.WriteLine("Address book sorted by city.");
            }
            else
            {
                Console.WriteLine($"Address book '{addressBookName}' not found.");
            }
        }

        public void SortAddressBookByState(string addressBookName)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                addressBook.SortByState();
                Console.WriteLine("Address book sorted by state.");
            }
            else
            {
                Console.WriteLine($"Address book '{addressBookName}' not found.");
            }
        }

        public void SortAddressBookByZipCode(string addressBookName)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                addressBook.SortByZipCode();
                Console.WriteLine("Address book sorted by zip code.");
            }
            else
            {
                Console.WriteLine($"Address book '{addressBookName}' not found.");
            }
        }

        public void SaveAddressBookToFile(string addressBookName, string fileName)
        {
            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
            {
                List<Contact> contacts = addressBook.GetAllContacts();
                using (var writer = new StreamWriter(fileName))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(contacts);
                    }
                }
                Console.WriteLine($"Address book '{addressBookName}' saved to file '{fileName}' successfully.");
            }
            else
            {
                Console.WriteLine($"Address book '{addressBookName}' not found.");
            }
        }

        public void LoadAddressBookFromFile(string addressBookName, string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    using (var reader = new StreamReader(fileName))
                    {
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            List<Contact> contacts = csv.GetRecords<Contact>().ToList();
                            if (addressBooks.TryGetValue(addressBookName, out AddressBook addressBook))
                            {
                                addressBook.GetAllContacts().Clear();
                                foreach (Contact contact in contacts)
                                {
                                    addressBook.AddContact(contact);
                                    UpdateCityAndStateMappings(contact);
                                }
                                Console.WriteLine($"Address book '{addressBookName}' loaded from file '{fileName}' successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Address book '{addressBookName}' not found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while loading address book from file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"File '{fileName}' not found.");
            }
        }
    }

}

