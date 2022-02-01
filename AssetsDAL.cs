using System;
using System.Data;
using System.Data.SqlClient;

using HRM.BOL;

namespace HRM.DAL
{
    public class AssetsDAL
    {
        public int Save(AssetsBOL objAsset)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[16];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@AssetID", objAsset.AssetID);
                objParam[++i] = new SqlParameter("@AssetTypeID", objAsset.AssetTypeID);
                objParam[++i] = new SqlParameter("@BranchID", objAsset.BranchID);
                objParam[++i] = new SqlParameter("@AssetCode", objAsset.AssetCode);
                objParam[++i] = new SqlParameter("@AssetName", objAsset.AssetName);
                objParam[++i] = new SqlParameter("@AssetDesc", objAsset.AssetDesc);
                objParam[++i] = new SqlParameter("@AssetPrice", objAsset.AssetPrice);
                objParam[++i] = new SqlParameter("@PurchasedOn", objAsset.PurchasedOn);
                objParam[++i] = new SqlParameter("@ExpiryDate", objAsset.ExpiryDate);
                objParam[++i] = new SqlParameter("@PurchasedFrom", objAsset.PurchasedFrom);
                objParam[++i] = new SqlParameter("@AdditionalInfo", objAsset.AdditionalInfo);
                objParam[++i] = new SqlParameter("@AssetImage1", objAsset.AssetImage1);
                objParam[++i] = new SqlParameter("@AssetImage2", objAsset.AssetImage2);
                objParam[++i] = new SqlParameter("@AssetCount", objAsset.AssetCount);
                objParam[++i] = new SqlParameter("@CreatedBy", objAsset.CreatedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Assets_INSERT_UPDATE";
                objDA.sqlParam = objParam;
                return int.Parse(objDA.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int nAssetID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                objParam[0] = new SqlParameter("@AssetID", nAssetID);
                objDA.sqlCmdText = "hrm_Assets_DELETE";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAll(AssetsBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            int i = -1;
            try
            {
                if (objBOL.AssetID > 0)
                    objParam[++i] = new SqlParameter("@AssetID", objBOL.AssetID);
                if(objBOL.BranchID>0)
                    objParam[++i] = new SqlParameter("@BranchID", objBOL.BranchID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "hrm_Assets_SELECT";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectAsset(int Id)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            int i = -1;
            try
            {


                objParam[++i] = new SqlParameter("@EmployeeID", Id);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_Asset_getAssetsOnEmployee]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable getunreturnedAssets(AssetsBOL objBOL)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[2];

            int i = -1;
            try
            {


                objParam[++i] = new SqlParameter("@EmployeeID", objBOL.EmployeeID);

                objDA.sqlParam = objParam;
                objDA.sqlCmdText = "[hrm_Asset_getUnreturnedAssets]";
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public AssetsBOL SelectByID(int nAssetID)
        {
            AssetsBOL objBOL = null;
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];

            try
            {
                if (nAssetID > 0)
                {
                    objBOL = new AssetsBOL();
                    objParam[0] = new SqlParameter("@AssetID", nAssetID);
                    objDA.sqlParam = objParam;

                    objDA.sqlCmdText = "hrm_Assets_SELECT";
                    SqlDataReader dReader = objDA.ExecuteDataReader();
                    while (dReader.Read())
                    {
                        objBOL.AssetID = nAssetID;
                        objBOL.AssetTypeID = Util.ToInt("" + dReader["AssetTypeID"]);
                        objBOL.BranchID = Util.ToInt("" + dReader["BranchID"]);
                        objBOL.AssetCode = "" + dReader["AssetCode"];
                        objBOL.AssetName = "" + dReader["AssetName"];
                        objBOL.AssetDesc = "" + dReader["AssetDesc"];
                        objBOL.AssetPrice = Util.ToDecimal("" + dReader["AssetPrice"]);
                        objBOL.PurchasedOn = "" + dReader["PurchasedOn"];
                        objBOL.ExpiryDate = "" + dReader["ExpiryDate"];
                        objBOL.PurchasedFrom = "" + dReader["PurchasedFrom"];
                        objBOL.AdditionalInfo = "" + dReader["AdditionalInfo"];
                        objBOL.AssetImage1 = "" + dReader["AssetImage1"];
                        objBOL.AssetImage2 = "" + dReader["AssetImage2"];
                        objBOL.AssetCount = "" + dReader["AssetCount"];
                        objBOL.Status = "" + dReader["Status"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBOL;
        }

        public int TransferAsset(AssetsBOL objAsset)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@AssetTransferID", objAsset.AssetTransferID);
                objParam[++i] = new SqlParameter("@AssetID", objAsset.AssetID);
                objParam[++i] = new SqlParameter("@BranchFrom", objAsset.BranchFrom);
                objParam[++i] = new SqlParameter("@BranchTo", objAsset.BranchTo);
                objParam[++i] = new SqlParameter("@TransferredBy", objAsset.TransferredBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Assets_Transfer_Insert_Update";
                objDA.sqlParam = objParam;
                return  objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateassetonEmployee(int AID)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[1];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@AEId", AID);


                objDA.sqlCmdText = "[hrm_Asset_UpdateassetonEmployee]";
                objDA.sqlParam = objParam;
                return objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AssignToEmployee(AssetsBOL objAsset)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@AssetID", objAsset.AssetID);
                objParam[++i] = new SqlParameter("@EmployeeID", objAsset.EmployeeID);
                objParam[++i] = new SqlParameter("@AssignedBy", objAsset.AssignedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Assets_Employees_Insert_Update";
                objDA.sqlParam = objParam;
                return  objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RemoveAssignment(AssetsBOL objAsset)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {

                objParam[++i] = new SqlParameter("@AssetID", objAsset.AssetID);
                objParam[++i] = new SqlParameter("@EmployeeID", objAsset.EmployeeID);
                objParam[++i] = new SqlParameter("@AssignedBy", objAsset.AssignedBy);
                objParam[++i] = new SqlParameter("@Status", "Y");

                objDA.sqlCmdText = "hrm_Assets_Employees_Delete";
                objDA.sqlParam = objParam;
                return  objDA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAssignedAssets(AssetsBOL objAsset)
        {
            DataAccess objDA = new DataAccess();
            SqlParameter[] objParam = new SqlParameter[10];
            int i = -1;

            try
            {
                if(objAsset.AssetID>0)
                objParam[++i] = new SqlParameter("@AssetID", objAsset.AssetID);
                if (objAsset.EmployeeID > 0)
                    objParam[++i] = new SqlParameter("@EmployeeID", objAsset.EmployeeID);

                objDA.sqlCmdText = "hrm_Assets_Employees_Select";
                objDA.sqlParam = objParam;
                return objDA.ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
