using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;
namespace Contacts.Maui.Views;

//when receive param value from previous page, assign the value to the property
[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private Contact? contact;
	public EditContactPage()
	{
		InitializeComponent();
		
	}

	//create property
	public string ContactId
	{
		set
		{
			contact = ContactRepository.GetContactById(Convert.ToInt32(value));
			if (contact != null)
			{
				contactCtrl.Address = contact.Address;
				contactCtrl.Name = contact.Name;
                contactCtrl.Email = contact.Email;
                contactCtrl.Phone = contact.Phone;
			}
	
		}
	}

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)

    {
		
		if (contact != null)
		{
			contact.Name = contactCtrl.Name;
			contact.Phone = contactCtrl.Phone;
            contact.Email = contactCtrl.Email;
            contact.Address = contactCtrl.Address;

            ContactRepository.UpdateContact(contact.ContactId, contact);
            Shell.Current.GoToAsync("..");
        }
		
    }

    private void contactCtrl_OnError(object sender, string e)
    {
		DisplayAlert("Error", e, "OK");
    }
}