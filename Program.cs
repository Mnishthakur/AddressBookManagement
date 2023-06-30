namespace AdressBookManagement
{
    class Program
    {
    static void Main(string[] args)
    {
        AddressBookSystem addressBookSystem = new AddressBookSystem();

        while (true)
        {
            Console.WriteLine("********** Address Book System **********");
            Console.WriteLine("1. Add Address Book");
            Console.WriteLine("2. Add Contact");
            Console.WriteLine("3. Remove Contact");
            Console.WriteLine("4. Search Contact by City");
            Console.WriteLine("5. Search Contact by State");
            Console.WriteLine("6. Get Contact Count by City");
            Console.WriteLine("7. Get Contact Count by State");
            Console.WriteLine("8. Sort Contacts by City");
            Console.WriteLine("9. Sort Contacts by State");
            Console.WriteLine("10. Sort Contacts by Zip");
            Console.WriteLine("11. Display Contacts");
            Console.WriteLine("12. Exit");
            Console.WriteLine("*****************************************");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter the name of the Address Book: ");
                string addressBookName = Console.ReadLine();
                addressBookSystem.AddAddressBook(addressBookName);
            }
            else if (choice == "2")
            {
                Console.Write("Enter the name of the Address Book: ");
                string addressBookName = Console.ReadLine();

                Console.Write("Enter the First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter the Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter the Address: ");
                string address = Console.ReadLine();
                Console.Write("Enter the City: ");
                string city = Console.ReadLine();
                Console.Write("Enter the State: ");
                string state = Console.ReadLine();
                Console.Write("Enter the ZIP Code: ");
                string zipCode = Console.ReadLine();
                Console.Write("Enter the Phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter the Email: ");
                string email = Console.ReadLine();

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

                addressBookSystem.AddContact(addressBookName, contact);
            }
            else if (choice == "3")
            {
                Console.Write("Enter the name of the Address Book: ");
                string addressBookName = Console.ReadLine();

                Console.Write("Enter the First Name of the contact to remove: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter the Last Name of the contact to remove: ");
                string lastName = Console.ReadLine();

                addressBookSystem.RemoveContact(addressBookName, firstName, lastName);
            }
            else if (choice == "4")
            {
                Console.Write("Enter the City to search contacts: ");
                string city = Console.ReadLine();

                List<Contact> contactsInCity = addressBookSystem.SearchByCity(city);
                if (contactsInCity.Count > 0)
                {
                    Console.WriteLine("Contacts in the specified city '{0}':", city);
                    foreach (var contact in contactsInCity)
                    {
                        Console.WriteLine(contact.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("No contacts found in the specified city.");
                }
            }
            else if (choice == "5")
            {
                Console.Write("Enter the State to search contacts: ");
                string state = Console.ReadLine();

                List<Contact> contactsInState = addressBookSystem.SearchByState(state);
                if (contactsInState.Count > 0)
                {
                    Console.WriteLine("Contacts in the specified state '{0}':", state);
                    foreach (var contact in contactsInState)
                    {
                        Console.WriteLine(contact.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("No contacts found in the specified state.");
                }
            }
            else if (choice == "6")
            {
                Console.Write("Enter the City to get contact count: ");
                string city = Console.ReadLine();

                int contactCount = addressBookSystem.GetContactCountByCity(city);
                Console.WriteLine("Contact Count in the specified city '{0}': {1}", city, contactCount);
            }
            else if (choice == "7")
            {
                Console.Write("Enter the State to get contact count: ");
                string state = Console.ReadLine();

                int contactCount = addressBookSystem.GetContactCountByState(state);
                Console.WriteLine("Contact Count in the specified state '{0}': {1}", state, contactCount);
            }
            else if (choice == "8")
            {
                Console.Write("Enter the name of the Address Book to sort by City: ");
                string addressBookName = Console.ReadLine();

                if (addressBookSystem.GetAddressBook(addressBookName) != null)
                {
                    AddressBook addressBook = addressBookSystem.GetAddressBook(addressBookName);
                    addressBook.SortByCity();
                    addressBook.DisplayContacts();
                }
                else
                {
                    Console.WriteLine("Address Book not found.");
                }
            }
            else if (choice == "9")
            {
                Console.Write("Enter the name of the Address Book to sort by State: ");
                string addressBookName = Console.ReadLine();

                if (addressBookSystem.GetAddressBook(addressBookName) != null)
                {
                    AddressBook addressBook = addressBookSystem.GetAddressBook(addressBookName);
                    addressBook.SortByState();
                    addressBook.DisplayContacts();
                }
                else
                {
                    Console.WriteLine("Address Book not found.");
                }
            }
            else if (choice == "10")
            {
                Console.Write("Enter the name of the Address Book to sort by Zip: ");
                string addressBookName = Console.ReadLine();

                if (addressBookSystem.GetAddressBook(addressBookName) != null)
                {
                    AddressBook addressBook = addressBookSystem.GetAddressBook(addressBookName);
                    addressBook.SortByZipCode();
                    addressBook.DisplayContacts();
                }
                else
                {
                    Console.WriteLine("Address Book not found.");
                }
            }
            else if (choice == "11")
            {
                Console.Write("Enter the name of the Address Book to display contacts: ");
                string addressBookName = Console.ReadLine();

                if (addressBookSystem.GetAddressBook(addressBookName) != null)
                {
                    AddressBook addressBook = addressBookSystem.GetAddressBook(addressBookName);
                    addressBook.DisplayContacts();
                }
                else
                {
                    Console.WriteLine("Address Book not found.");
                }
            }
            else if (choice == "12")
            {
                Console.WriteLine("Exiting the program...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            Console.WriteLine();
        }
    }
}



