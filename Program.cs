using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace ViewStateKeyGenerator
{
    /// <summary>
    /// KeyGenerator
    /// Britton Scritchfield
    /// MrRedBeard on Instructables
    /// Generate Cryptographically Random Keys
    /// This code generates validationKeys and decryptionKeys
    /// http://msdn.microsoft.com/en-us/library/ff649308.aspx
    /// </summary>
    class Program
    {
        [STAThread()]
        static void Main(string[] argv)
        {
            start:
            Console.WriteLine("Type the number value of choice below");
            Console.WriteLine("SHA1 validationKey 128");
            Console.WriteLine("AES decryptionKey 64");
            Console.WriteLine("3DES decryptionKey 48");
            //Console.WriteLine("Default is 128 & 64");

            int len = 128;

            try1:
            try
            {
                    len = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter a numeric value");
                goto try1;
            }

            nextkey1:
            byte[] buff = new byte[len / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < buff.Length; i++)
            {
                sb.Append(string.Format("{0:X2}", buff[i]));
            }

            Console.Clear();

            //write key to screen
            Console.WriteLine("Key: \r\n");
            Console.WriteLine(sb + "\r\n");
            
            //write key to clipboard
            Clipboard.SetText(sb.ToString());

            Console.WriteLine("The " + len + "bit key has been written to the clipboard.\r\n");

            Console.WriteLine("Here is an example");
            Console.WriteLine("<machineKey validationKey=" + (char)34 + " " + (char)34 + " decryptionKey=" + (char)34 + " " + (char)34 + " validation=" + (char)34 + "SHA1" + (char)34 + "decryption=" + (char)34 + "AES" + (char)34 + "/>" + "\r\n\r\n");

            Console.WriteLine("Type restart, hit enter to generate a new " + len + " bit key or quit:");
            
            var action = Console.ReadLine();
            if (action == "")
            {
                goto nextkey1;
            }
            else if (action == "restart")
            {
                goto start;
            }

        }

    }
}