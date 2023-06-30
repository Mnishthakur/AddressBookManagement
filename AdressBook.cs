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

        public void EditContact(string firstName, string lastName)
        {
            Contact contactToEdit = FindContactByName(firstName, lastName);

            if (contactToEdit != null)
            {
                Console.WriteLine("Enter the new contact information:");

                Console.Write("First Name: ");
                contactToEdit.FirstName = Console.ReadLine();

                Console.Write("Last Name: ");
                contactToEdit.LastName = Console.ReadLine();

                Console.Write("Address: ");
                contactToEdit.Address = Console.ReadLine();

                Console.Write("City: ");
                contactToEdit.City = Console.ReadLine();

                Console.Write("State: ");
                contactToEdit.State = Console.ReadLine();

                Console.Write("ZIP Code: ");
                contactToEdit.ZipCode = Console.ReadLine();

                Console.Write("Phone Number: ");
                contactToEdit.PhoneNumber = Console.ReadLine();

                Console.Write("Email: ");
                contactToEdit.Email = Console.ReadLine();

                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        private Contact FindContactByName(string firstName, string lastName)
        {
            return contacts.Find(contact => contact.FirstName == firstName && contact.LastName == lastName);
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
    }

}

