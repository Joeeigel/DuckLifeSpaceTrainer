using DLSpaceTrainer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLSpaceTrainer
{
    public partial class Form1 : Form
    {
        #region Dependencies

        private readonly IRegistryService _registryService;

        #endregion

        #region Constructor

        public Form1(IRegistryService registryService)
        {
            _registryService = registryService;

            InitializeComponent();

            SetupButtons();
        }

        #endregion

        #region Event Handlers

        private void btnUnlockEarth_Click(object sender, EventArgs e)
        {
            var mapKey = "unlockmap1";
            handleButton(mapKey);
        }
        
        private void btnUnlockMoon_Click(object sender, EventArgs e)
        {
            //unlockmap2_h1723740667
            var mapKey = "unlockmap2";
            handleButton(mapKey);
        }

        private void btnUnlockMars_Click(object sender, EventArgs e)
        {
            var mapKey = "unlockmap3";
            handleButton(mapKey);
        }

        private void btnUnlockTerra_Click(object sender, EventArgs e)
        {
            var mapKey = "unlockmap4";
            handleButton(mapKey);
        }

        private void btnUnlockFreez_Click(object sender, EventArgs e)
        {
            var mapKey = "unlockmap5";
            handleButton(mapKey);
        }

        private void btnUnlockPyro_Click(object sender, EventArgs e)
        {
            var mapKey = "unlockmap6";
            handleButton(mapKey);
        }

        #endregion

        #region Helpers

        private void SetupButtons()
        {
            btnUnlockEarth.Text = "Unlock Earth";
            btnUnlockMoon.Text = "Unlock Moon";
            btnUnlockMars.Text = "Unlock Mars";
            btnUnlockTerra.Text = "Unlock Terra";
            btnUnlockFreez.Text = "Unlock Freez";
            btnUnlockPyro.Text = "Unlock Pyro";

            var keys = _registryService.ListValues().Where(x => x.Contains("unlockmap"));

            foreach (var key in keys)
                SetupButton(key.Split('_')[0]);
        }

        private void SetupButton(string key)
        {
            ///TODO:
            ///Make this less garbage
            if (key.Contains("1"))  
                btnUnlockEarth.Text = "Lock Earth";
            if (key.Contains("2"))
                btnUnlockMoon.Text = "Lock Moon";
            if (key.Contains("3"))
                btnUnlockMars.Text = "Lock Mars";
            if (key.Contains("4"))
                btnUnlockTerra.Text = "Lock Terra";
            if (key.Contains("5"))
                btnUnlockFreez.Text = "Lock Freez";
            if (key.Contains("6"))
                btnUnlockPyro.Text = "Lock Pyro";
        }

        private void handleButton(string mapKey)
        {
            var keys = _registryService.ListValues().Where(x => x.Contains(mapKey));

            if (keys != null && keys.Count() > 0) { 
                foreach (var key in keys)
                    if (!_registryService.DeleteKey(key)) 
                        MessageBox.Show(string.Format("Error locking level {0}", key));
            }
            else
                _registryService.SetKeyValue(mapKey, true);

            SetupButtons();
        }

        #endregion

        private void btnSurfsUp_Click(object sender, EventArgs e)
        {
            _registryService.SetKeyValue("duckData1_costume", 2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var key = _registryService.ListValues().Where(x => x.Contains("1.run_exp")).First();

            if(_registryService.GetKeyValue(key).ToString() != lblRunExp.Text)
                _registryService.GetKeyValue(key);

            lblRunExp.Text = _registryService.GetKeyValue(key).ToString();
        }

        private void btnIncRunExp_Click(object sender, EventArgs e)
        {
            var key = _registryService.ListValues().Where(x => x.Contains("1.run_exp")).FirstOrDefault();

            UInt64 currentExp = (ulong)_registryService.GetKeyValue(key);
            
            _registryService.SetKeyValue(key, (long)currentExp + 20000000000000000);
        }
    }
}
