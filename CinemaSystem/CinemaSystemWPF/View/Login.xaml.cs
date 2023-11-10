using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemWPF.View
{
    public partial class Login : Window
    {
        private readonly CinemaSystemContext _context;

        public Login()
        {
            InitializeComponent();
            _context = new CinemaSystemContext();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Query the database to check if the user exists
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.Password))
            {
                // Password is correct, login successful
                MenuForm menu = new MenuForm();
                this.Visibility = Visibility.Hidden;
                menu.Show();
            }
            else
            {
                // User not found or incorrect password
                MessageBox.Show("Login fail");
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Convert the stored password from base64 to bytes
            byte[] storedPasswordBytes = Convert.FromBase64String(storedPassword);

            // Extract the salt from the stored password
            byte[] salt = new byte[16];
            Array.Copy(storedPasswordBytes, 0, salt, 0, 16);

            // Hash the entered password using the stored salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA256);
            byte[] enteredPasswordHash = pbkdf2.GetBytes(20);

            // Compare the stored hash with the computed hash
            for (int i = 0; i < 20; i++)
            {
                if (enteredPasswordHash[i] != storedPasswordBytes[i + 16])
                {
                    return false; // Passwords do not match
                }
            }

            return true; // Passwords match
        }


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }
    }
}
