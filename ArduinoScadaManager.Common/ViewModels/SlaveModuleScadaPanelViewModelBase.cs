﻿using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        protected const int ErrorCommand = 255;

        private readonly IModbusTransferManager _modbusTransferManager;
        protected readonly ILogger Logger;

        public ISlaveModuleProcess SlaveModuleProcess { get; private set; }
        public IMasterModuleProcess MasterModuleProcess { get; private set; }

        public UserControl View { get; set; }

        protected SlaveModuleScadaPanelViewModelBase(
            IModbusTransferManager modbusTransferManager,
            IMasterModuleProcess masterModuleProcess, 
            ISlaveModuleProcess slaveModuleProcess)
        {
            Logger = modbusTransferManager;
            _modbusTransferManager = modbusTransferManager;
            SlaveModuleProcess = slaveModuleProcess;
            MasterModuleProcess = masterModuleProcess;

            PrepareUsageOfModbusTransferManager();
        }

        private void PrepareUsageOfModbusTransferManager()
        {
            _modbusTransferManager.MastersDataReceived += OnMastersDataReceived;
        }

        private void OnMastersDataReceived(ModbusTransferData modbusTransferData)
        {
            if (modbusTransferData.DeviceAddress == 0)
                OnDataReceived(modbusTransferData);

            if (modbusTransferData.DeviceAddress == MasterModuleProcess.Identifier)
                OnDataReceived(modbusTransferData);
        }

        protected void SendRequest(byte command, string data = "")
        {
            SendRequest(command, data.StringToByteArray());
        }

        protected void SendRequest(byte command, byte[] data)
        {
            if (data == null) data = new byte[0];

            _modbusTransferManager.SendAsMaster(new ModbusTransferData(
                SlaveModuleProcess.Identifier, command, data));
        }

        protected abstract void OnDataReceived(ModbusTransferData modbusTransferData);
    }
}
