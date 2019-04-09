﻿using System.Windows.Controls;
using ArduinoScadaManager.Gui.ViewModels.MasterModuleViewModels;

namespace ArduinoScadaManager.Gui.Views {
    public partial class ScadaPanelView : UserControl {
        public ScadaPanelView(MasterModuleViewModel jakasKlasa) {
            InitializeComponent();
            DataContext = jakasKlasa;
        }
    }
}