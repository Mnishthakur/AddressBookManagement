namespace AdressBookManagement
{
    class Program
    {
        static void Main(string[] args)

        {
            // Create a dictionary to store Address Books
            Dictionary<string, AddressBook> addressBooks = new Dictionary<string, AddressBook>();

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create a new Address Book");
                Console.WriteLine("2. Add a contact to an Address Book");
                Console.WriteLine("3. Display contacts in an Address Book");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter the name for the new Address Book: ");
                    string addressBookName = Console.ReadLine();

                    // Check if the Address Book name already exists
                    if (addressBooks.ContainsKey(addressBookName))
                    {
                        Console.WriteLine("Address Book with the same name already exists. Please choose a different name.");
                        continue;
                    }

                    // Create a new Address Book
                    AddressBook addressBook = new AddressBook();
                    addressBooks.Add(addressBookName, addressBook);

                    Console.WriteLine("Address Book created successfully.");
                }
                else if (choice == "2")
                {
                    Console.Write("Enter the name of the Address Book to add a contact: ");
                    string addressBookName = Console.ReadLine();

                    // Check if the Address Book exists
                    if (!addressBooks.ContainsKey(addressBookName))
                    {
                        Console.WriteLine("Address Book not found. Please create the Address Book first.");
                        continue;
                    }

                    AddressBook addressBook = addressBooks[addressBookName];

                    Console.WriteLine("Enter contact details:");
                    Console.Write("First Name: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Last Name: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Address: ");
                    string address = Console.ReadLine();

                    Console.Write("City: ");
                    string city = Console.ReadLine();

                    Console.Write("State: ");
                    string state = Console.ReadLine();

                    Console.Write("ZIP Code: ");
                    string zipCode = Console.ReadLine();

                    Console.Write("Phone Number: ");
                    string phoneNumber = Console.ReadLine();

                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    // Create a new contact
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

                    // Add the contact to the Address Book
                    addressBook.AddContact(contact);

                    Console.WriteLine("Contact added to the Address Book.");
                }
                else if (choice == "3")
                {
                    Console.Write("Enter the name of the Address Book to display contacts: ");
                    string addressBookName = Console.ReadLine();

                    // Check if the Address Book exists
                    if (!addressBooks.ContainsKey(addressBookName))
                    {
                        Console.WriteLine("Address Book not found. Please create the Address Book first.");
                        continue;
                    }

                    AddressBook addressBook = addressBooks[addressBookName];

                    Console.WriteLine("Contacts in Address Book '{0}':", addressBookName);
                    addressBook.DisplayContacts();
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }

                Console.WriteLine();

            }
        }
    }

}



