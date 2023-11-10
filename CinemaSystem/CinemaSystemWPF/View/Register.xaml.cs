// Register.xaml.cs
using System;
using System.Windows;
using CinemaSystemLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;


namespace CinemaSystemWPF.View
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly CinemaSystemContext _context; // Assuming your DbContext is named CinemaSystemContext

        public Register()
        {
            InitializeComponent();
            _context = new CinemaSystemContext(); // Initialize your DbContext
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Validation logic
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtNewPassword.Password))
            {
                MessageBox.Show("Username and New Password are required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Additional validation for Name
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name is a required field.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Additional validation for PhoneNumber
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                MessageBox.Show("Phone Number is a required field.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Additional validation for Password complexity
            if (txtNewPassword.Password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if the username already exists
            if (_context.Users.Any(u => u.Username == txtUsername.Text))
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Hash the password
                string hashedPassword = HashPassword(txtNewPassword.Password);

                // Create a new User object with the registration data
                var newUser = new User
                {
                    Username = txtUsername.Text,
                    Password = hashedPassword,
                    Name = txtName.Text,
                    PhoneNumber = txtPhoneNumber.Text
                };

                // Add the new user to the database
                _context.Users.Add(newUser);

                // Save changes to the database
                _context.SaveChanges();

                // Your registration logic goes here

                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the current window
                Login login = new Login();
                login.Show();
                this.Close();

                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Create a new Rfc2898DeriveBytes object and hash the password with the salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and password hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert the combined salt and hash to a base64-encoded string
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the login window
            Login login = new Login();
            login.Show();
            this.Close(); // Close the current window
        }
    }
}
