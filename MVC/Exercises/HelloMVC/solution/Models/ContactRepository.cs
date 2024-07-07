namespace HelloMvc.Models;

public static class ContactRepository
{
    private static List<Contact> _contacts = new List<Contact>
    {
        new Contact { ContactID = 1, Name = "Joe Schmoe", Email = "jschmoe@example.com", CountryCode="US" },
        new Contact { ContactID = 2, Name = "Jane Doe", Email = "jdoe@example.com", CountryCode="IN" },
        new Contact { ContactID = 3, Name = "Dwayne Robinson", Email = "drobinson@example.com", CountryCode="UK" },
    };

    private static int _nextId = 4;

    public static List<Contact> GetAll()
    {
        return _contacts;
    }

    public static Contact GetById(int id)
    {
        return _contacts.FirstOrDefault(c => c.ContactID == id);
    }

    public static void Add(Contact contact)
    {
        contact.ContactID = _nextId++;
        _contacts.Add(contact);
    }

    public static void Update(Contact contact)
    {
        var existingContact = _contacts.FirstOrDefault(c => c.ContactID == contact.ContactID);
        if (existingContact != null)
        {
            existingContact.Name = contact.Name;
            existingContact.Email = contact.Email;
            existingContact.CountryCode = contact.CountryCode;
        }
    }

    public static void Delete(int id)
    {
        var contact = _contacts.FirstOrDefault(c => c.ContactID == id);
        if (contact != null)
        {
            _contacts.Remove(contact);
        }
    }
}
