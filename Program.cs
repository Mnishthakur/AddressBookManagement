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
    public class Program
    {
        public static void Main()
        {
            AddressBookSystem addressBookSystem = new AddressBookSystem();

            while (true)
            {
                Console.WriteLine("Address Book System");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Create Address Book");
                Console.WriteLine("2. Add Contact");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Search Contacts by City");
                Console.WriteLine("6. Search Contacts by State");
                Console.WriteLine("7. Get Contact Count by City");
                Console.WriteLine("8. Get Contact Count by State");
                Console.WriteLine("9. Sort Address Book by Name");
                Console.WriteLine("10. Sort Address Book by City");
                Console.WriteLine("11. Sort Address Book by State");
                Console.WriteLine("12. Sort Address Book by Zip Code");
                Console.WriteLine("13. Save Address Book to File");
                Console.WriteLine("14. Load Address Book from File");
                Console.WriteLine("0. Exit");
                Console.WriteLine();

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter the name of the address book: ");
                        string addressBookName = Console.ReadLine();
                        addressBookSystem.CreateAddressBook(addressBookName);
                        Console.WriteLine($"Address book '{addressBookName}' created successfully.");
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Write("Enter the name of the address book to add the contact: ");
                        addressBookName = Console.ReadLine();
                        Console.WriteLine();

                        Contact contact = new Contact();
                        Console.Write("Enter the first name: ");
                        contact.FirstName = Console.ReadLine();
                        Console.Write("Enter the last name: ");
                        contact.LastName = Console.ReadLine();
                        Console.Write("Enter the address: ");
                        contact.Address = Console.ReadLine();
                        Console.Write("Enter the city: ");
                        contact.City = Console.ReadLine();
                        Console.Write("Enter the state: ");
                        contact.State = Console.ReadLine();
                        Console.Write("Enter the zip code: ");
                        contact.ZipCode = Console.ReadLine();
                        Console.Write("Enter the phone number: ");
                        contact.PhoneNumber = Console.ReadLine();
                        Console.Write("Enter the email: ");
                        contact.Email = Console.ReadLine();

                        addressBookSystem.AddContactToAddressBook(addressBookName, contact);
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.Write("Enter the name of the address book to edit the contact: ");
                        addressBookName = Console.ReadLine();
                        Console.Write("Enter the first name of the contact to edit: ");
                        string editFirstName = Console.ReadLine();
                        Console.Write("Enter the last name of the contact to edit: ");
                        string editLastName = Console.ReadLine();
                        Console.WriteLine();

                        Contact newContact = new Contact();
                        Console.Write("Enter the new first name: ");
                        newContact.FirstName = Console.ReadLine();
                        Console.Write("Enter the new last name: ");
                        newContact.LastName = Console.ReadLine();
                        Console.Write("Enter the new address: ");
                        newContact.Address = Console.ReadLine();
                        Console.Write("Enter the new city: ");
                        newContact.City = Console.ReadLine();
                        Console.Write("Enter the new state: ");
                        newContact.State = Console.ReadLine();
                        Console.Write("Enter the new zip code: ");
                        newContact.ZipCode = Console.ReadLine();
                        Console.Write("Enter the new phone number: ");
                        newContact.PhoneNumber = Console.ReadLine();
                        Console.Write("Enter the new email: ");
                        newContact.Email = Console.ReadLine();

                        addressBookSystem.EditContactInAddressBook(addressBookName, editFirstName, editLastName, newContact);
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.Write("Enter the name of the address book to delete the contact: ");
                        addressBookName = Console.ReadLine();
                        Console.Write("Enter the first name of the contact to delete: ");
                        string deleteFirstName = Console.ReadLine();
                        Console.Write("Enter the last name of the contact to delete: ");
                        string deleteLastName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.DeleteContactFromAddressBook(addressBookName, deleteFirstName, deleteLastName);
                        Console.WriteLine();
                        break;

                    case "5":
                        Console.Write("Enter the city to search for contacts: ");
                        string searchCity = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SearchByCity(searchCity);
                        Console.WriteLine();
                        break;

                    case "6":
                        Console.Write("Enter the state to search for contacts: ");
                        string searchState = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SearchByState(searchState);
                        Console.WriteLine();
                        break;

                    case "7":
                        Console.Write("Enter the city to get contact count: ");
                        string countCity = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.GetContactCountByCity(countCity);
                        Console.WriteLine();
                        break;

                    case "8":
                        Console.Write("Enter the state to get contact count: ");
                        string countState = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.GetContactCountByState(countState);
                        Console.WriteLine();
                        break;

                    case "9":
                        Console.Write("Enter the name of the address book to sort by name: ");
                        addressBookName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SortAddressBookByName(addressBookName);
                        Console.WriteLine();
                        break;

                    case "10":
                        Console.Write("Enter the name of the address book to sort by city: ");
                        addressBookName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SortAddressBookByCity(addressBookName);
                        Console.WriteLine();
                        break;

                    case "11":
                        Console.Write("Enter the name of the address book to sort by state: ");
                        addressBookName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SortAddressBookByState(addressBookName);
                        Console.WriteLine();
                        break;

                    case "12":
                        Console.Write("Enter the name of the address book to sort by zip code: ");
                        addressBookName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SortAddressBookByZipCode(addressBookName);
                        Console.WriteLine();
                        break;

                    case "13":
                        Console.Write("Enter the name of the address book to save to file: ");
                        addressBookName = Console.ReadLine();
                        Console.Write("Enter the file name: ");
                        string saveFileName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.SaveAddressBookToFile(addressBookName, saveFileName);
                        Console.WriteLine();
                        break;

                    case "14":
                        Console.Write("Enter the name of the address book to load from file: ");
                        addressBookName = Console.ReadLine();
                        Console.Write("Enter the file name: ");
                        string loadFileName = Console.ReadLine();
                        Console.WriteLine();

                        addressBookSystem.LoadAddressBookFromFile(addressBookName, loadFileName);
                        Console.WriteLine();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }

}


