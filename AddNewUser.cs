private void AddUser(string username, string password, string confirmPass, string email)// add new user the the DB
{
    //Local variables to hold values
    string smtpEmail = smtpUserNameTextBox.Text;
    string smtpPassword = smtpPasswordTextBox.Text;
    int smtpPort = (int)smtpPortNumericUD.Value;
    string smtpAddress = smtpAddressTextBox.Text;

    //Regex for making sure Email is valid
    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    Match match = regex.Match(email);

    //Loop through Logins Table
    foreach (DataRow row in loginsData.Logins)
    {
        //And look for matching usernames
        if (row.ItemArr[0].Equals(username))
        {
            //If one is found, show message:
            MessageBox.Show("Username already exists");
            return;
        }
    }

    //Confirm pass must equal password.
    if (password != confirmPass)
    {
        MessageBox.Show("Passwords do not match");
    }
    //Password must be at least 8 characters long
    else if (password.Length < 8)
    {
        MessageBox.Show("Password must be at least 8 characters long");
    }
    //If email is NOT valid
    else if (!match.Success)
    {
        MessageBox.Show("Invalid Email");
    }
    //If there is no username
    else if (username == null)
    {
        MessageBox.Show("Must have Username");
    }
    //If all is well, create the new user!
    else
    {
        loginsData.LoginsRow newUser = loginsData.Logins.NewLoginsRow();
		//Encrypted data
        string EncryptPass = HashPass(password);// the function is below
        newUser.Username = username;
        newUser.Password = EncryptPass;
        newUser.Email = email;
		//raw data
        loginsData.Logins.Rows.Add(newUser);
        UserName.Text = String.Empty;
        Password.Text = String.Empty;
        ConfirmPassword.Text = String.Empty;
        Email.Text = String.Empty;
        MessageBox.Show("Thanks for Registering!");
		//check if email is good
        if (String.IsNullOrWhiteSpace(smtpEmail) || String.IsNullOrWhiteSpace(smtpPassword) || String.IsNullOrWhiteSpace(smtpAddress) || smtpPort <= 0)
        {
            MessageBox.Show("Email configuration is not set up correctly! \nCannot sent email!");
            
        }
        else
        {
            SendMessage(email.ToString(), username.ToString(), password.ToString());
        }
    }
}




