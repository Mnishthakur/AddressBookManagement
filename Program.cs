using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdressBookManagement
{
    public class Program
{
    public static void Main()
    {
        AddressBookSystem addressBookSystem = new AddressBookSystem();
        string currentAddressBook = null;

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create Address Book");
            Console.WriteLine("2. Add Contact");
            Console.WriteLine("3. Edit Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Search Contacts by City");
            Console.WriteLine("6. Search Contacts by State");
            Console.WriteLine("7. Get Contact Count by City");
            Console.WriteLine("8. Get Contact Count by State");
            Console.WriteLine("9. Sort Address Book by Name");
            Console.WriteLine("10. Save Address Book to File");
            Console.WriteLine("11. Load Address Book from File");
            Console.WriteLine("12. Print All Address Books");
            Console.WriteLine("13. Exit");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the name of the Address Book: ");
                    string addressBookName = Console.ReadLine();
                    addressBookSystem.CreateAddressBook(addressBookName);
                    Console.WriteLine($"Address Book '{addressBookName}' created.");
                    break;
                case 2:
                    Console.Write("Enter the name of the Address Book: ");
                    addressBookName = Console.ReadLine();
                    Console.Write("Enter the contact's first name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter the contact's last name: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Enter the contact's address: ");
                    string address = Console.ReadLine();
                    Console.Write("Enter the contact's city: ");
                    string city = Console.ReadLine();
                    Console.Write("Enter the contact's state: ");
                    string state = Console.ReadLine();
                    Console.Write("Enter the contact's zip code: ");
                    string zipCode = Console.ReadLine();
                    Console.Write("Enter the contact's phone number: ");
                    string phoneNumber = Console.ReadLine();
                    Console.Write("Enter the contact's email: ");
                    string email = Console.ReadLine();
                    Contact contact = new Contact
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
                    addressBookSystem.AddContactToAddressBook(addressBookName, contact);
                    break;
                case 3:
                    Console.Write("Enter the name of the Address Book: ");
                    addressBookName = Console.ReadLine();
                    Console.Write("Enter the first name of the contact to edit: ");
                    firstName = Console.ReadLine();
                    Console.Write("Enter the last name of the contact to edit: ");
                    lastName = Console.ReadLine();
                    Console.Write("Enter the new first name: ");
                    string newFirstName = Console.ReadLine();
                    Console.Write("Enter the new last name: ");
                    string newLastName = Console.ReadLine();
                    Console.Write("Enter the new address: ");
                    string newAddress = Console.ReadLine();
                    Console.Write("Enter the new city: ");
                    string newCity = Console.ReadLine();
                    Console.Write("Enter the new state: ");
                    string newState = Console.ReadLine();
                    Console.Write("Enter the new zip code: ");
                    string newZipCode = Console.ReadLine();
                    Console.Write("Enter the new phone number: ");
                    string newPhoneNumber = Console.ReadLine();
                    Console.Write("Enter the new email: ");
                    string newEmail = Console.ReadLine();
                    Contact newContact = new Contact
                    {
                        FirstName = newFirstName,
                        LastName = newLastName,
                        Address = newAddress,
                        City = newCity,
                        State = newState,
                        ZipCode = newZipCode,
                        PhoneNumber = newPhoneNumber,
                        Email = newEmail
                    };
                    addressBookSystem.EditContactInAddressBook(addressBookName, firstName, lastName, newContact);
                    break;
                case 4:
                    Console.Write("Enter the name of the Address Book: ");
                    addressBookName = Console.ReadLine();
                    Console.Write("Enter the first name of the contact to delete: ");
                    firstName = Console.ReadLine();
                    Console.Write("Enter the last name of the contact to delete: ");
                    lastName = Console.ReadLine();
                    addressBookSystem.DeleteContactFromAddressBook(addressBookName, firstName, lastName);
                    break;
                case 5:
                    Console.Write("Enter the city name to search for contacts: ");
                    city = Console.ReadLine();
                    addressBookSystem.SearchByCity(city);
                    break;
                case 6:
                    Console.Write("Enter the state name to search for contacts: ");
                    state = Console.ReadLine();
                    addressBookSystem.SearchByState(state);
                    break;
                case 7:
                    Console.Write("Enter the city name to get contact count: ");
                    city = Console.ReadLine();
                    addressBookSystem.GetContactCountByCity(city);
                    break;
                case 8:
                    Console.Write("Enter the state name to get contact count: ");
                    state = Console.ReadLine();
                    addressBookSystem.GetContactCountByState(state);
                    break;
                case 9:
                    Console.Write("Enter the name of the Address Book: ");
                    addressBookName = Console.ReadLine();
                    addressBookSystem.SortAddressBookByName(addressBookName);
                    break;
                case 10:
                    Console.Write("Enter the name of the Address Book to save: ");
                    addressBookName = Console.ReadLine();
                    Console.Write("Enter the file path to save the Address Book as a JSON file: ");
                    string filePath = Console.ReadLine();
                    addressBookSystem.SaveAddressBookToFile(addressBookName, filePath);
                    break;
                case 11:
                    Console.Write("Enter the name of the Address Book to load: ");
                    addressBookName = Console.ReadLine();
                    Console.Write("Enter the file path to load the Address Book from a JSON file: ");
                    filePath = Console.ReadLine();
                    addressBookSystem.LoadAddressBookFromFile(addressBookName, filePath);
                    break;
                case 12:
                    addressBookSystem.PrintAllAddressBooks();
                    break;
                case 13:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
}


