using Hit.Services.Helpers.Classes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hit.Services.EncryptDecrypt
{
   public class TextConfig
    {

        EncryptionHelper enHlp;

        public TextConfig()
        {
            enHlp = new EncryptionHelper();
        }

        public string Encrypt(string text)
        {
            try
            {
                return enHlp.Encrypt(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }

        public string Decrypt(string text)
        {
            try
            {
                return enHlp.Decrypt(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }
    }
}
