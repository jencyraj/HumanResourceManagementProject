using System;
using System.Data;

using HRM.DAL;
using HRM.BOL;

namespace HRM.BAL
{
    public class AssetTypesBAL
    {
        public int Save(AssetTypesBOL objAssetType)
        {
            AssetTypesDAL objDAL = new AssetTypesDAL();
            return objDAL.Save(objAssetType);
        }

        public int Delete(int nAssetTypeID)
        {
            AssetTypesDAL objDAL = new AssetTypesDAL();
            return objDAL.Delete(nAssetTypeID);
        }

        public DataTable SelectAll(int nAssetTypeID)
        {
            AssetTypesDAL objDAL = new AssetTypesDAL();
            return objDAL.SelectAll(nAssetTypeID);
        }
    }
}
