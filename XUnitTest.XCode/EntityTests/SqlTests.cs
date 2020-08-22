﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using XCode.DataAccessLayer;
using XCode.Membership;
using Xunit;

namespace XUnitTest.XCode.EntityTests
{
    public class SqlTests
    {
        public SqlTests()
        {
            DAL.AddConnStr("mysql", "Server=.;Port=3306;Database=membership;Uid=root;Pwd=root", null, "mysql");
            DAL.AddConnStr("mysql_underline", "Server=.;Port=3306;Database=membership_underline;Uid=root;Pwd=root;NameFormat=underline", null, "mysql");
        }

        [Fact]
        public void InsertTestSQLite()
        {
            var factory = UserX.Meta.Factory;
            var session = UserX.Meta.Session;

            var user = new UserX
            {
                Name = "Stone",
                DisplayName = "大石头",
                Enable = true,

                RegisterTime = new DateTime(2020, 8, 22),
                UpdateTime = new DateTime(2020, 9, 1),
            };

            var sql = factory.Persistence.GetSql(session, user, DataObjectMethodType.Insert);
            Assert.Equal(@"Insert Into User(Name,Password,DisplayName,Sex,Mail,Mobile,Code,Avatar,RoleID,RoleIds,DepartmentID,Online,Enable,Logins,LastLogin,LastLoginIP,RegisterTime,RegisterIP,Ex1,Ex2,Ex3,Ex4,Ex5,Ex6,UpdateUser,UpdateUserID,UpdateIP,UpdateTime,Remark) Values('Stone',null,'大石头',0,null,null,null,null,0,null,0,0,1,0,null,null,'2020-08-22 00:00:00',null,0,0,0,null,null,null,null,0,null,'2020-09-01 00:00:00',null)", sql);
        }

        [Fact]
        public void InsertTestMySqlUnderline()
        {
            using var split = UserX.Meta.CreateSplit("mysql_underline", null);

            var factory = UserX.Meta.Factory;
            var session = UserX.Meta.Session;

            var user = new UserX
            {
                Name = "Stone",
                DisplayName = "大石头",
                Enable = true,

                RegisterTime = new DateTime(2020, 8, 22),
                UpdateTime = new DateTime(2020, 9, 1),
            };

            var sql = factory.Persistence.GetSql(session, user, DataObjectMethodType.Insert);
            Assert.Equal(@"Insert Into `user`(name,password,display_name,sex,mail,mobile,code,avatar,role_id,role_ids,department_id,online,enable,logins,last_login,last_login_ip,register_time,register_ip,ex1,ex2,ex3,ex4,ex5,ex6,update_user,update_user_id,update_ip,update_time,remark) Values('Stone',null,'大石头',0,null,null,null,null,0,null,0,0,1,0,null,null,'2020-08-22 00:00:00',null,0,0,0,null,null,null,null,0,null,'2020-09-01 00:00:00',null)", sql);
        }

        [Fact]
        public void UpdateTestSQLite()
        {
            var factory = UserX.Meta.Factory;
            var session = UserX.Meta.Session;

            var user = new UserX
            {
                ID = 2,
                Name = "Stone",
                DisplayName = "大石头",
                Enable = true,

                RegisterTime = new DateTime(2020, 8, 22),
                UpdateTime = new DateTime(2020, 9, 1),
            };

            var sql = factory.Persistence.GetSql(session, user, DataObjectMethodType.Update);
            Assert.Equal(@"Update User Set Name='Stone',DisplayName='大石头',Enable=1,RegisterTime='2020-08-22 00:00:00',UpdateTime='2020-09-01 00:00:00' Where ID=2", sql);
        }

        [Fact]
        public void UpdateTestMySqlUnderline()
        {
            using var split = UserX.Meta.CreateSplit("mysql_underline", null);

            var factory = UserX.Meta.Factory;
            var session = UserX.Meta.Session;

            var user = new UserX
            {
                ID = 2,
                Name = "Stone",
                DisplayName = "大石头",
                Enable = true,

                RegisterTime = new DateTime(2020, 8, 22),
                UpdateTime = new DateTime(2020, 9, 1),
            };

            var sql = factory.Persistence.GetSql(session, user, DataObjectMethodType.Update);
            Assert.Equal(@"Update `user` Set name='Stone',display_name='大石头',enable=1,register_time='2020-08-22 00:00:00',update_time='2020-09-01 00:00:00' Where id=2", sql);
        }

        [Fact]
        public void DeleteTestSQLite()
        {
            var factory = UserX.Meta.Factory;
            var session = UserX.Meta.Session;

            var user = new UserX
            {
                ID = 2,
            };

            var sql = factory.Persistence.GetSql(session, user, DataObjectMethodType.Delete);
            Assert.Equal(@"Delete From User Where ID=2", sql);
        }

        [Fact]
        public void DeleteTestMySqlUnderline()
        {
            using var split = UserX.Meta.CreateSplit("mysql_underline", null);

            var factory = UserX.Meta.Factory;
            var session = UserX.Meta.Session;

            var user = new UserX
            {
                ID = 2,
            };

            var sql = factory.Persistence.GetSql(session, user, DataObjectMethodType.Delete);
            Assert.Equal(@"Delete From `user` Where id=2", sql);
        }
    }
}