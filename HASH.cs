public string HashPass(string password)
       {
           SHA256 sha256 = new SHA256CryptoServiceProvider();

           //compute hash from the bytes of text
           sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(email + password));

           //get hash answer after compute it
           byte[] answer = sha256.Hash;

           StringBuilder strBuilder = new StringBuilder();
           for (int i = 0; i < answer.Length; i++)
           {
               //change it into 2 hexadecimal digits
               //for each byte
               strBuilder.Append(answer[i].ToString("x2"));
           }

           return strBuilder.ToString();
       }