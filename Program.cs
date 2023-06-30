namespace AdressBookManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the address book
            AddressBook addressBook = new AddressBook();

            // Prompt for adding multiple contacts
            Console.WriteLine("Enter contact details. Enter 'done' to finish.");

            while (true)
            {
                Console.Write("First Name: ");
                string firstName = Console.ReadLine();

                if (firstName.ToLower() == "done")
                    break;

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

                // Add the contact to the address book
                addressBook.AddContact(contact);

                Console.WriteLine("Contact added successfully.\n");
            }



            addressBook.DisplayContacts();
        }
    }

}



