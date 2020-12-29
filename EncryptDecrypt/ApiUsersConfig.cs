using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hit.Services.EncryptDecrypt
{
   public class ApiUsersConfig
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<LoginModel> Users = null;
            
        ConfigHelper config;

        public ApiUsersConfig()
        {
            // read Users.json file
            config = new ConfigHelper();
            try
            {
                Users = config.ReadUsersFile();
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Reading Api Users file");
            }
        }

        public void SaveUsers()
        {
            try
            {
                config.WriteUsersFile(Users);
                MessageBox.Show("Users are saved!", "SAVE");
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show(ex.Message, "Error Writing Api Users file");
            }
        }
    }
}
