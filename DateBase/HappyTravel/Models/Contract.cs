using System;

namespace HappyTravel.Models
{
    internal class Contract
    {
        #region Fields
        private int _contractNumber;
        private DateTime _dateOfMaking;
        private int _clientCode;
        #endregion

        #region Properties
        public int ClientCode { get => _clientCode; set => _clientCode = value; }
        public DateTime DateOfMaking { get => _dateOfMaking; set => _dateOfMaking = value; }
        public int ContractNumber { get => _contractNumber; set => _contractNumber = value; }
        #endregion

        #region Constructor
        internal Contract(int contractNumber, DateTime dateOfMaking, int clientCode)
        {
            this.ContractNumber = contractNumber;
            this.DateOfMaking = dateOfMaking;
            this.ClientCode = clientCode;
        }
        #endregion
    }
}