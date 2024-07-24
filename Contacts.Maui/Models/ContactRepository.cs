using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact{ContactId=1,Name="John Doe", Email="John@email.com"},

            new Contact{ContactId=2,Name="Jane Doe", Email="Jane@email.com"},

            new Contact{ContactId=3,Name="Tom Hanks", Email="Tom@email.com"},

            new Contact{ContactId=4,Name="Frank Liu", Email="Frank@email.com"}
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null)
            {
                return new Contact
                {
                    Address = contact.Address,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    ContactId=contactId,
                };
            }
            return null;
        }

        public static void UpdateContact(int contactId,Contact contact)
        {
            if (contactId != contact.ContactId) return;
            var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Phone = contact.Phone;
            }
        }

        public static void DeleteContact(int contactId)
        {
            var contactToDelete = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contactToDelete != null)
            {
                _contacts.Remove(contactToDelete);
            }
        }

        public static void AddContact (Contact contact)
        {
            var maxId = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
        }

        public static List<Contact> SearchContacts(string filtertext)
        {
            var contacts=_contacts.Where(x =>!string.IsNullOrWhiteSpace(x.Name)&& x.Name.StartsWith(filtertext, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filtertext, StringComparison.OrdinalIgnoreCase))?.ToList();
            else return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filtertext, StringComparison.OrdinalIgnoreCase))?.ToList();
            else return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filtertext, StringComparison.OrdinalIgnoreCase))?.ToList();
            else return contacts;

            return contacts;
        }
    }
}
