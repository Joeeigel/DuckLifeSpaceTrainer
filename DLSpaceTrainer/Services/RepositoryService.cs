using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSpaceTrainer.Services
{
    class RegistryService : IRegistryService
    {
        private RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Mofunzone\\Duck Life: Space");

        public Dictionary<string, string> GetKey(string key)
        {
            return new Dictionary<string, string>();
        }

        public void SetKey(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IList<string> ListValues()
        {
            return regKey.GetValueNames().ToList();
        }
    }

    public interface IRegistryService
    {
        void SetKey(string key, string value);
        Dictionary<string, string> GetKey(string key);
        IList<string> ListValues();
    }
}
