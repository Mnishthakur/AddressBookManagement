namespace AdressBookManagement
{
    class Program
    {
        static void Main(string[] args)

        {
            while (true)
            {
                Console.WriteLine("Address Book System Menu");
                Console.WriteLine("1. Create a new Address Book");
                Console.WriteLine("2. Add a contact to an Address Book");
                Console.WriteLine("3. Remove a contact from an Address Book");
                Console.WriteLine("4. Display contacts in an Address Book");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter the name of the new Address Book: ");
                    string addressBookName = Console.ReadLine();

                    AddressBookSystem.AddAddressBook(addressBookName);
                }
                else if (choice == "2")
                {
                    Console.Write("Enter the name of the Address Book to add the contact: ");
                    string addressBookName = Console.ReadLine();

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

                    addressBookSystem.AddContact(addressBookName, contact);
                }
                else if (choice == "3")
                {
                    Console.Write("Enter the name of the Address Book to remove the contact: ");
                    string addressBookName = Console.ReadLine();

                    Console.Write("Enter the First Name of the contact: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Enter the Last Name of the contact: ");
                    string lastName = Console.ReadLine();

                    addressBookSystem.RemoveContact(addressBookName, firstName, lastName);
                }
                else if (choice == "4")
                {
                    Console.Write("Enter the name of the Address Book to display contacts: ");
                    string addressBookName = Console.ReadLine();

                    addressBookSystem.DisplayAddressBook(addressBookName);
                }
                else if (choice == "5")
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


