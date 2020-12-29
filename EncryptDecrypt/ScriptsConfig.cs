using Hit.Services.Helpers.Classes.Classes;
using Hit.Services.Models.Models;
using Hit.Services.Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hit.Services.EncryptDecrypt
{
   public class ScriptsConfig
    {
        FilesEncryptStatus encrStatus;
        ConfigHelper config;

       public List<string> Files;

       public ScriptsConfig()
        {
            config = new ConfigHelper();
            Files = config.GetScriptFiles().ToList();
            encrStatus = FilesState.GetEncryptFilesStatus();
        }

        public string Readfile(string name)
        {
            try
            {
                return config.ReadScriptFile(name, encrStatus.ScriptFilesAreEncrypted);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return "";
            }
        }

        public bool Writefile(string name, string text)
        {
            try
            {
                 config.WriteScriptFile(name, text, encrStatus.ScriptFilesAreEncrypted);
                MessageBox.Show("Script is saved");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }

        }


        public bool Deletefile(string name)
        {
            try
            {
                config.DeleteScriptFile(name);
                MessageBox.Show("Script is Deleted");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }

        }

    }
}
