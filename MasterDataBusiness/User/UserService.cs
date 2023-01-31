using Business.Library;
using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MasterDataBusiness
{
    public class UserService
    {

        private MasterDataDbContext db;

        public UserService()
        {
            db = new MasterDataDbContext();
        }

        public UserService(MasterDataDbContext db)
        {
            this.db = db;
        }


        #region loginUser
        public actionResultUserMenuViewModel addUser(UserMenuViewModel data)
        {
            try
            {
                String State = "Start";
                String msglog = "";
                var olog = new logtxt();
                var userlogion = new userViewModelV2();
                var password = "";
                var queryUser = db.MS_User.AsQueryable();
                if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.UserPassword))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.UserPassword));

                    //get hash result after compute it  
                    byte[] result1 = md5.Hash;

                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result1.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilder.Append(result1[i].ToString("x2"));
                    }
                    password = strBuilder.ToString();
                    if (!string.IsNullOrEmpty(password))
                    {
                        queryUser = queryUser.Where(c => c.User_Name == data.UserName && c.User_Password == password && c.IsActive == 1 && c.IsDelete == 0);
                    }

                }
                var query = queryUser.ToList();

                Guid userGroupIndex = new Guid();
                string result = "";
                string toKen = "";
                string toKenRefresh = "";
                if (data.UserLogIndex.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    data.UserLogIndex = Guid.NewGuid();
                }

                if (data.UserKey.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    data.UserKey = Guid.NewGuid();
                }
                if (data.UserId == null)
                {
                    data.UserId = "UserId".genAutonumber();
                }

                foreach (var item in query)
                {
                    sy_UserLog Model = new sy_UserLog();
                    Model.UserLog_Index = data.UserLogIndex;
                    Model.User_Index = item.User_Index;
                    Model.User_Id = data.UserId;
                    Model.User_Name = item.User_Name;
                    Model.User_Key = data.UserKey;
                    Model.Status_Id = item.Status_Id;
                    Model.Create_By = "";
                    Model.Create_Date = DateTime.Now;
                    Model.Update_By = "";
                    Model.Update_Date = DateTime.Now;
                    result = item.User_Name;
                    userGroupIndex = item.UserGroup_Index;
                    db.sy_UserLog.Add(Model);


                    userlogion.user_Index = item.User_Index;
                    userlogion.user_Id = item.User_Id;
                    userlogion.user_Name = item.User_Name;
                    userlogion.first_Name = item.First_Name;
                    userlogion.last_Name = item.Last_Name;
                    userlogion.emp_Code = item.Emp_Code;
                    userlogion.position_Code = item.Position_Code;
                    userlogion.position_Name = item.Position_Name;

                }

                var transactionx = db.Database.BeginTransaction();
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveZoneLocation", msglog);
                    transactionx.Rollback();

                    throw exy;
                }


                var m = new List<userMenuViewModel>();
                if (query.Count > 0)
                {

                    //var ResponeToken = utils.SendDataApi<LoginResponseViewModel>(new AppSettingConfig().GetUrl("getToken"), new { username = data.UserName, password, systemcode="WMS" }.sJson());

                    //if (ResponeToken.status == "SUCCESS")
                    //{
                    //    toKen = ResponeToken?.model?.token;
                    //    toKenRefresh = ResponeToken?.model?.refresh;


                    //    var groupMenu = db.MS_UserGroupMenu.Where(c => c.UserGroup_Index == query.FirstOrDefault().UserGroup_Index).ToList();

                    //    foreach (var me in groupMenu)
                    //    {
                    //        m.Add(new userMenuViewModel { menuName = db.sy_Menu.FirstOrDefault(c => c.Menu_Index == me.Menu_Index)?.Menu_Name, isActive = me.IsActive, seq = Convert.ToInt16(me.UserGroupMenu_Id) });
                    //    }
                    //}
                    //else if (ResponeToken.status == "ERROR")
                    //{

                    //}


                    var groupMenu = db.MS_UserGroupMenu.Where(c => c.UserGroup_Index == query.FirstOrDefault().UserGroup_Index).ToList();

                    foreach (var me in groupMenu)
                    {
                        m.Add(new userMenuViewModel { menuName = db.sy_Menu.FirstOrDefault(c => c.Menu_Index == me.Menu_Index)?.Menu_Name, isActive = me.IsActive, seq = Convert.ToInt16(me.UserGroupMenu_Id) });
                    }


                }

                var queryGroup = db.MS_UserGroup.FirstOrDefault(c => c.UserGroup_Index == userGroupIndex);

                var res = new actionResultUserMenuViewModel();

                if (queryGroup != null)
                {
                    res.userGroupName = queryGroup.UserGroup_Name;
                }


                res.Userlogin = userlogion;
                res.userName = result;
                res.toKen = toKen;
                res.toKenRefresh = toKenRefresh;
                res.userMenuViewModel = m.OrderBy(o => o.seq).ToList();

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filter
        public actionResultUserViewModel filter(SeaechUserViewModel data)
        {
            try
            {

                var query = db.View_User.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.User_Id.Contains(data.key)
                                         || c.User_Name.Contains(data.key)
                                         || c.UserGroup_Name.Contains(data.key)
                                         || (c.First_Name + " " + c.Last_Name).Contains(data.key));
                }

                var Item = new List<View_User>();
                var TotalRow = new List<View_User>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.User_Id).ToList();

                var result = new List<SeaechUserViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SeaechUserViewModel();

                    resultItem.user_Index = item.User_Index;
                    resultItem.user_Id = item.User_Id;
                    resultItem.user_Name = item.User_Name;
                    resultItem.userGroup_Name = item.UserGroup_Name;
                    resultItem.fullname = item.First_Name + " " + item.Last_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultUserViewModel = new actionResultUserViewModel();
                actionResultUserViewModel.itemsUser = result.ToList();
                actionResultUserViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultUserViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(userViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var UserOld = db.MS_User.Find(data.user_Index);

                if (UserOld == null)
                {
                    // Convert Password To Md5
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.user_Password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder Password = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        Password.Append(result[i].ToString("x2"));
                    }


                    data.user_Id = "UserId".genAutonumber();


                    MS_User Model = new MS_User();

                    Model.User_Index = Guid.NewGuid();
                    Model.User_Id = data.user_Id;
                    Model.User_Name = data.user_Name;
                    Model.UserGroup_Index = data.userGroup_Index;
                    Model.User_Password = Password.ToString();
                    Model.First_Name = data.first_Name;
                    Model.Last_Name = data.last_Name;
                    Model.Position_Code = data.position_Code;
                    Model.Position_Name = data.position_Name;
                    Model.Station_Code = data.station_Code;
                    Model.Station_Name = data.station_Name;
                    Model.Branch_Code = data.branch_Code;
                    Model.Branch_Name = data.branch_Name;
                    Model.Status_Emp = data.status_Emp;
                    Model.Emp_Code = data.emp_Code;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 1;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;
                    Model.LastUpdate_UserPassword = DateTime.Now;

                    db.MS_User.Add(Model);
                }
                else
                {
                    StringBuilder Password = new StringBuilder();

                    if (UserOld.User_Password != data.user_Password)
                    {
                        // Convert Password To Md5
                        MD5 md5 = new MD5CryptoServiceProvider();

                        //compute hash from the bytes of text  
                        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.user_Password));

                        //get hash result after compute it  
                        byte[] result = md5.Hash;

                        for (int i = 0; i < result.Length; i++)
                        {
                            //change it into 2 hexadecimal digits  
                            //for each byte  
                            Password.Append(result[i].ToString("x2"));
                        }
                        if (UserOld.User_Password == Password.ToString())
                        {
                            return "fail";
                        }
                        UserOld.LastUpdate_UserPassword = DateTime.Now;

                    }


                    UserOld.User_Name = data.user_Name;
                    UserOld.User_Password = Password.ToString() == "" ? data.user_Password : Password.ToString();
                    UserOld.UserGroup_Index = data.userGroup_Index;
                    UserOld.First_Name = data.first_Name;
                    UserOld.Last_Name = data.last_Name;
                    UserOld.Position_Code = data.position_Code;
                    UserOld.Position_Name = data.position_Name;
                    UserOld.Station_Code = data.station_Code;
                    UserOld.Station_Name = data.station_Name;
                    UserOld.Branch_Code = data.branch_Code;
                    UserOld.Branch_Name = data.branch_Name;
                    UserOld.Status_Emp = data.status_Emp;
                    UserOld.Emp_Code = data.emp_Code;
                    UserOld.IsActive = Convert.ToInt32(data.isActive);
                    UserOld.Status_Id = 1;
                    UserOld.Update_By = data.create_By;
                    UserOld.Update_Date = DateTime.Now;
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveUser", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region find
        public userViewModelV2 find(Guid id)
        {
            try
            {

                var queryResult = db.View_User.Where(c => c.User_Index == id).FirstOrDefault();

                var result = new userViewModelV2();


                result.user_Index = queryResult.User_Index;
                result.user_Id = queryResult.User_Id;
                result.user_Name = queryResult.User_Name;
                result.user_Password = queryResult.User_Password;
                result.userGroup_Index = queryResult.UserGroup_Index;
                result.userGroup_Id = queryResult.UserGroup_Id;
                result.userGroup_Name = queryResult.UserGroup_Name;
                result.first_Name = queryResult.First_Name;
                result.last_Name = queryResult.Last_Name;
                result.position_Code = queryResult.Position_Code;
                result.position_Name = queryResult.Position_Name;
                result.station_Code = queryResult.Station_Code;
                result.station_Name = queryResult.Station_Name;
                result.branch_Code = queryResult.Branch_Code;
                result.branch_Name = queryResult.Branch_Name;
                result.status_Emp = queryResult.Status_Emp;
                result.emp_Code = queryResult.Emp_Code;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Export Excel
        public UserActionResultExportViewModel Export(UserExportViewModel data)
        {
            try
            {

                var query = db.View_User.AsQueryable();


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.User_Id.Contains(data.key)
                                        || c.User_Name.Contains(data.key));

                }

                var Item = new List<View_User>();
                var TotalRow = new List<View_User>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.User_Id).ToList();

                var result = new List<UserExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new UserExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.user_Id = item.User_Id;
                    resultItem.user_Name = item.User_Name;
                    resultItem.user_Index = item.User_Index;
                    resultItem.fullname = item.First_Name + " " + item.Last_Name; ;
                    resultItem.userGroup_Name = item.UserGroup_Name;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var UserActionResultExportViewModel = new UserActionResultExportViewModel();
                UserActionResultExportViewModel.itemsUser = result.ToList();

                return UserActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //public String SaveChanges(userViewModel data)
        //{
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {

        //        var UserOld = db.MS_User.Find(data.user_Index);

        //        if (UserOld == null)
        //        {
        //            // Convert Password To Md5
        //            MD5 md5 = new MD5CryptoServiceProvider();

        //            //compute hash from the bytes of text  
        //            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.user_Password));

        //            //get hash result after compute it  
        //            byte[] result = md5.Hash;

        //            StringBuilder Password = new StringBuilder();
        //            for (int i = 0; i < result.Length; i++)
        //            {
        //                //change it into 2 hexadecimal digits  
        //                //for each byte  
        //                Password.Append(result[i].ToString("x2"));
        //            }


        //            var Sys_Key = new SqlParameter("Sys_Key", "UserId");
        //            var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //            resultParameter.Size = 2000; // some meaningfull value
        //            resultParameter.Direction = ParameterDirection.Output;
        //            db.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);

        //            data.user_Id = resultParameter.Value.ToString();


        //            MS_User Model = new MS_User();

        //            Model.User_Index = Guid.NewGuid();
        //            Model.User_Id = data.user_Id;
        //            Model.User_Name = data.user_Name;
        //            Model.UserGroup_Index = data.userGroup_Index;
        //            Model.User_Password = Password.ToString();
        //            Model.IsActive = 1;
        //            Model.IsDelete = 0;
        //            Model.IsSystem = 0;
        //            Model.Status_Id = 0;
        //            Model.Create_By = data.create_By;
        //            Model.Create_Date = DateTime.Now;

        //            db.MS_User.Add(Model);
        //        }
        //        else
        //        {
        //            StringBuilder Password = new StringBuilder();

        //            if (UserOld.User_Password != data.user_Password)
        //            {
        //                // Convert Password To Md5
        //                MD5 md5 = new MD5CryptoServiceProvider();

        //                //compute hash from the bytes of text  
        //                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.user_Password));

        //                //get hash result after compute it  
        //                byte[] result = md5.Hash;

        //                for (int i = 0; i < result.Length; i++)
        //                {
        //                    //change it into 2 hexadecimal digits  
        //                    //for each byte  
        //                    Password.Append(result[i].ToString("x2"));
        //                }
        //            } 


        //            UserOld.User_Name = data.user_Name;
        //            //UserOld.User_Password = Password.ToString();
        //            UserOld.User_Password = Password.ToString() == "" ? data.user_Password : Password.ToString();
        //            UserOld.UserGroup_Index = data.userGroup_Index;
        //            UserOld.Update_By = data.create_By;
        //            UserOld.Update_Date = DateTime.Now;
        //        }

        //        var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
        //        try
        //        {
        //            db.SaveChanges();
        //            transactionx.Commit();
        //        }

        //        catch (Exception exy)
        //        {
        //            msglog = State + " ex Rollback " + exy.Message.ToString();
        //            olog.logging("SaveUser", msglog);
        //            transactionx.Rollback();

        //            throw exy;
        //        }

        //        return "Done"; ;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public actionResultUserMenuViewModel addUser(UserMenuViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pwhereFilter = "";
        //            string result = "";
        //            Guid userGroupIndex = new Guid();
        //            //Guid newUsername = new Guid();
        //            if (data.UserLogIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.UserLogIndex = Guid.NewGuid();
        //            }

        //            if (data.UserKey.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.UserKey = Guid.NewGuid();
        //            }
        //            int statusId = 0;
        //            if (data.UserId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "UserId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.UserId = resultParameter.Value.ToString();
        //            }

        //            if (data.UserName != "" && data.UserName != null)
        //            {
        //                pwhereFilter += " And User_Name = N'" + data.UserName + "'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            MD5 md5 = new MD5CryptoServiceProvider();

        //            //compute hash from the bytes of text  
        //            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(data.UserPassword));

        //            //get hash result after compute it  
        //            byte[] result1 = md5.Hash;

        //            StringBuilder strBuilder = new StringBuilder();
        //            for (int i = 0; i < result1.Length; i++)
        //            {
        //                //change it into 2 hexadecimal digits  
        //                //for each byte  
        //                strBuilder.Append(result1[i].ToString("x2"));
        //            }

        //            if (strBuilder.ToString() != "" && strBuilder.ToString() != null)
        //            {
        //                pwhereFilter += " And User_Password = '" + strBuilder.ToString() + "'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }

        //            pwhereFilter += " And isActive = '" + 1 + "'";
        //            pwhereFilter += " And isDelete = '" + 0 + "'";
        //            var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //            var query = context.MS_User.FromSql("sp_GetUser @strwhere ", strwhere).ToList();

        //            foreach (var item in query)
        //            {

        //                var UserLog_Index = new SqlParameter("UserLog_Index", data.UserLogIndex);
        //                var User_Index = new SqlParameter("User_Index", item.User_Index);
        //                var User_Id = new SqlParameter("User_Id", data.UserId);
        //                var User_Name = new SqlParameter("User_Name", data.UserName);
        //                var User_Key = new SqlParameter("User_Key", data.UserKey);
        //                var Status_Id = new SqlParameter("Status_Id", statusId);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_sy_UserLog  @UserLog_Index,@User_Index,@User_Id,@User_Name,@User_Key,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date ", UserLog_Index, User_Index, User_Id, User_Name, User_Key, Status_Id, Create_By, Create_Date, Update_By, Update_Date);
        //                result = item.User_Name;
        //                userGroupIndex = item.UserGroup_Index;
        //            }

        //            var m = new List<userMenuViewModel>();
        //            if (query.Count > 0)
        //            {
        //                var groupMenu = context.MS_UserGroupMenu.Where(c => c.UserGroup_Index == query.FirstOrDefault().UserGroup_Index).ToList();

        //                //var menu = context.sy_Menu.Where(c => groupMenu.Contains(c.Menu_Index)).ToList();


        //                foreach (var me in groupMenu)
        //                {
        //                    m.Add(new userMenuViewModel { menuName = context.sy_Menu.FirstOrDefault(c => c.Menu_Index == me.Menu_Index)?.Menu_Name, isActive = me.IsActive, seq = Convert.ToInt16(me.UserGroupMenu_Id) });
        //                }
        //            }


        //            //return result;

        //            if (result != "" && userGroupIndex != null)
        //            {
        //                pwhereFilter = " And UserGroup_Index = '" + userGroupIndex.ToString() + "'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "And UserGroup_Name = '0000000000'";
        //            }
        //            var strwhere1 = new SqlParameter("@strwhere1", pwhereFilter);
        //            var query1 = context.MS_UserGroup.FromSql("sp_GetUserGroup @strwhere1", strwhere1).FirstOrDefault();

        //            var res = new actionResultUserMenuViewModel();

        //            if (query1 != null)
        //            {
        //                res.userGroupName = query1.UserGroup_Name;
        //            }

        //            res.userName = result;
        //            res.userMenuViewModel = m.OrderBy(o => o.seq).ToList();

        //            return res;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public userViewModel find(Guid id)
        //{
        //    try
        //    {

        //        var queryResult = db.View_User.Where(c => c.User_Index == id).FirstOrDefault();

        //        var result = new userViewModel();


        //        result.user_Index = queryResult.User_Index;
        //        result.user_Id = queryResult.User_Id;
        //        result.user_Name = queryResult.User_Name;
        //        result.user_Password = queryResult.User_Password;
        //        result.userGroup_Index = queryResult.UserGroup_Index;
        //        result.userGroup_Id = queryResult.UserGroup_Id;
        //        result.userGroup_Name = queryResult.UserGroup_Name;

        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public Boolean getDelete(userViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var User = db.MS_User.Find(data.user_Index);

                if (User != null)
                {
                    User.IsActive = 0;
                    User.IsDelete = -1;


                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeleteUser", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }


                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<ItemListViewModel> autoUser(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_User.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.User_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.User_Index, c.User_Name, c.User_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.User_Index,
                            id = item.User_Id,
                            name = item.User_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<View_UserTaskGroupViewModel> configUserTaskGroup(View_UserTaskGroupViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_UserTaskGroup.AsQueryable();

                    if (!string.IsNullOrEmpty(data.user_Name))
                    {
                        query = query.Where(c => c.User_Name.Contains(data.user_Name));
                    }
                    if (!string.IsNullOrEmpty(data.taskGroup_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                    {
                        query = query.Where(c => c.TaskGroup_Index == data.taskGroup_Index);
                    }

                    var items = new List<View_UserTaskGroupViewModel>();

                    var result = query.ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new View_UserTaskGroupViewModel();

                        resultItem.user_Index = item.User_Index;
                        resultItem.user_Id = item.User_Id;
                        resultItem.user_Name = item.User_Name;
                        resultItem.taskGroupUser_Index = item.TaskGroupUser_Index;
                        resultItem.taskGroupUser_Id = item.TaskGroupUser_Id;
                        resultItem.taskGroup_Index = item.TaskGroup_Index;
                        resultItem.taskGroup_Id = item.TaskGroup_Id;
                        resultItem.taskGroup_Name = item.TaskGroup_Name;

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<userViewModel> dropdownUser(userViewModel model)
        {
            try
            {
                var items = new List<userViewModel>();
                var result = db.MS_User.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                if (!string.IsNullOrEmpty(model.userGroupString))
                {
                    var listIndex = new List<Guid>();

                    var userGroup = model.userGroupString.Split(',');
                    foreach (var u in userGroup)
                    {
                        var group = new Guid(u);
                        listIndex.Add(group);
                    }

                    result = result.Where(c => listIndex.Contains(c.UserGroup_Index));
                }

                var data = result.ToList();
                foreach (var item in data)
                {
                    var resultItem = new userViewModel
                    {
                        user_Index = item.User_Index,
                        user_Id = item.User_Id,
                        user_Name = item.User_Name
                    };

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public userViewModelV2 checkStatusUser(userViewModelV2 data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_User.AsQueryable();

                    if (!string.IsNullOrEmpty(data.user_Name))
                    {
                        query = query.Where(c => c.User_Name == data.user_Name);
                    }
                    var result = query.FirstOrDefault();


                    var resultItem = new userViewModelV2();

                    resultItem.user_Index = result.User_Index;
                    resultItem.user_Id = result.User_Id;
                    resultItem.user_Name = result.User_Name;
                    resultItem.status_Id = result.Status_Id;
                    resultItem.checkupdate = checkUpdatePassword(result.User_Index);
                    return resultItem;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool checkUpdatePassword(Guid user_index)
        {

            using (var context = new MasterDataDbContext())
            {
                var userCheck = context.MS_User.FirstOrDefault(c=> c.User_Index == user_index);
                DateTime dateset = DateTime.Now.AddDays(-90);
                if (userCheck.LastUpdate_UserPassword < dateset) {
                    return true;
                }
                return false;
            }
        }

    }
}
