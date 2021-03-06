﻿using System;

using Xamarin.Forms;

namespace XamarinForms.IWA
{
	public partial class LoginPage : ContentPage
	{
        // Event to provide login information when the user dismisses the view
        public event EventHandler<LoginEventArgs> OnLoginInfoEntered;

        // Event to report that the login was canceled
        public event EventHandler OnCanceled;

        public LoginPage ()
		{
			InitializeComponent ();
		}

        private void LoginButtonClicked(object sender, EventArgs e)
        { 
            // Get the values entered in the text fields
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            var domain = DomainEntry.Text;

            // Make sure the user entered all values
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(domain))
            {
                DisplayAlert("Login", "Please enter a username, password, and domain","OK");
                return;
            }

            // Fire the OnLoginInfoEntered event and provide the login values
            if (OnLoginInfoEntered != null)
            {
                // Create a new LoginEventArgs to contain the user's values
                var loginEventArgs = new LoginEventArgs(username.Trim(), password.Trim(), domain.Trim());

                // Raise the event
                OnLoginInfoEntered(sender, loginEventArgs);
            }
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            // Fire the OnCanceled event to let the calling code no the login was canceled
            if (OnCanceled != null)
            {
                OnCanceled(this, null);
            }
        }
    }

    // Custom EventArgs implementation to hold login information (username, password, and domain)
    public class LoginEventArgs : EventArgs
    {
        // Username property
        public string Username { get; set; }

        // Password property
        public string Password { get; set; }

        // Domain property
        public string Domain { get; set; }

        // Store login values passed into the constructor
        public LoginEventArgs(string username, string password, string domain)
        {
            Username = username;
            Password = password;
            Domain = domain;
        }
    }
}
