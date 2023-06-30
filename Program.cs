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
                Console.WriteLine("8. Exit");
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

                    addressBookSystem.SearchByCity(city);
                }
                else if (choice == "5")
                {
                    Console.Write("Enter the State to search contacts: ");
                    string state = Console.ReadLine();

                    addressBookSystem.SearchByState(state);
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
}



