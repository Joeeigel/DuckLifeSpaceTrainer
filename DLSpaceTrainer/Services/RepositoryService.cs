﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSpaceTrainer.Services
{
    class RegistryService : IRegistryService
    {
        private RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Mofunzone\\Duck Life: Space", true);

        public object GetKeyValue(string key)
        {
            return regKey.GetValue(key);
        }

        public void SetKeyValue(string key, object value)
        {
            var matchingKeys = ListValues().Where(x => x.StartsWith(key));

            foreach (var match in matchingKeys)
                regKey.DeleteValue(match);

            regKey.SetValue(key, value);
        }

        public IList<string> ListValues()
        {
            return regKey.GetValueNames().ToList();
        }

        public bool DeleteKey(string key)
        {
            if(regKey.GetValueNames().Any(x => x == key))
            { 
                regKey.DeleteValue(key);
                return true;
            }

            return false;
        }
    }

    public interface IRegistryService
    {
        void SetKeyValue(string key, object value);
        bool DeleteKey(string key);
        object GetKeyValue(string key);
        IList<string> ListValues();

    }
}
