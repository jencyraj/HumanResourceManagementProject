using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class AssetsBAL
    {
        public int Save(AssetsBOL objAsset)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.Save(objAsset);
        }

        public int Delete(int nAssetID)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.Delete(nAssetID);
        }

        public DataTable SelectAll(AssetsBOL objBOL)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.SelectAll(objBOL);
        }
        public DataTable getunreturnedAssets(AssetsBOL objBOL)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.getunreturnedAssets(objBOL);
        }
        public DataTable SelectAsset(int Id)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.SelectAsset(Id);
        }
        public AssetsBOL SelectByID(int nAssetID)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.SelectByID(nAssetID);
        }

        public int TransferAsset(AssetsBOL objAsset)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.TransferAsset(objAsset);
        }
        public int UpdateassetonEmployee(int AID)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.UpdateassetonEmployee(AID);
        }

        public int AssignToEmployee(AssetsBOL objAsset)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.AssignToEmployee(objAsset);
        }

        public int RemoveAssignment(AssetsBOL objAsset)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.RemoveAssignment(objAsset);
        }

        public DataTable SelectAssignedAssets(AssetsBOL objAsset)
        {
            AssetsDAL objDAL = new AssetsDAL();
            return objDAL.SelectAssignedAssets(objAsset);
        }
    }
}
