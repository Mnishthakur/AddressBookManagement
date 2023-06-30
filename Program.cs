namespace AdressBookManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the address book
            AddressBook addressBook = new AddressBook();

            // Create a contact
            Contact contact1 = new Contact
            {
                FirstName = "Manish",
                LastName = "Thakur",
                Address = "123 Gurgaon",
                City = "Gurugram",
                State = "Haryana",
                ZipCode = "12345",
                PhoneNumber = "(555) 123-4567",
                Email = "abc@abc.com"
            };

            // Add the contact to the address book
            addressBook.AddContact(contact1);

            // Display all contacts in the address book
            addressBook.DisplayContacts();

        }
    }

}



