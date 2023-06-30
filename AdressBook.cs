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

        //public void RemoveContact(Contact contact)
        //{
        //    contacts.Remove(contact);
        //}

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

