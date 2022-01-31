using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class AssetsBOL
    {
        public int AssetID;
        public int AssetTypeID;
        public int BranchID;
        public string AssetCode;
        public string AssetName;
        public string AssetDesc;
        public decimal AssetPrice;
        public string PurchasedOn;
        public string ExpiryDate;
        public string PurchasedFrom;
        public string AdditionalInfo;
        public string AssetImage1;
        public string AssetImage2;
        public string AssetCount;
        public string Status;
        public string CreatedBy;

        //Transfer Assets
        public int AssetTransferID;
        public int BranchFrom;
        public int BranchTo;
        public string TransferredBy;

        //Assign Employee
        public int EmployeeID;
        public string AssignedBy;
    }
}
