using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PRN211_Asm2_Salemanagement_Library.Models;

namespace PRN211_Asm2_Salemanagement_Library.DAOs
{
    public class MemberDAO
    {
        // Singleton pattern 
        private static MemberDAO _instance;
        private static readonly object _lock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MemberDAO();
                    }
                    return _instance;
                }
            }
        }

        //Get admin account
        private Member GetAdminAccount()
        {
            Member admin = null;
            using (StreamReader sr = new StreamReader("appsettings.json"))
            {
                string json = sr.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                string email = config.GetSection("AdminAccount").GetSection("Email").Value;
                string password = config.GetSection("AdminAccount").GetSection("Password").Value;
                admin = new Member()
                {
                    MemberId = 0,
                    Email = email,
                    Password = password,
                    CompanyName = "",
                    City = "",
                    Country = "",
                    Orders = null
                };
            }
            return admin;
        }

        //Get all members
        public IEnumerable<Member> GetAllMembers()
        {
            IEnumerable<Member> members = null;
            try
            {
                var db = new SaleManagermentContext();
                members = db.Members;
                members = members.Append(GetAdminAccount());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return members;
        }

        //Login
        public Member Login(string email, string password)
        {
            IEnumerable<Member> members = GetAllMembers();
            Member member = members.FirstOrDefault(m => m.Email == email && m.Password == password);
            return member;
        }

        //Get member by id
        public Member GetMemberById(int id)
        {
            Member member = null;
            try
            {
                var db = new SaleManagermentContext();
                member = db.Members.Find(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return member;
        }

        //Get member by email
        public Member GetMemberByEmail(string email)
        {
            Member member = null;
            try
            {
                var db = new SaleManagermentContext();
                member = db.Members.FirstOrDefault(m => m.Email == email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return member;
        }

        //Add new member
        public bool AddMember(Member member)
        {
            bool result = false;
            try
            {
                var db = new SaleManagermentContext();
                //Check duplicate id and email
                if (db.Members.Find(member.MemberId) == null && db.Members.FirstOrDefault(m => m.Email == member.Email) == null)
                {
                    db.Members.Add(member);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("Member id or email is existed!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        //Update member
        public bool UpdateMember(Member member)
        {
            bool result = false;
            try
            {
                var db = new SaleManagermentContext();
                //Check duplicate email
                if (db.Members.FirstOrDefault(m => m.Email == member.Email && m.MemberId != member.MemberId) == null)
                {
                    db.Members.Update(member);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("Member email is existed!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        //Delete member
        public bool DeleteMember(int id)
        {
            bool result = false;
            try
            {
                var db = new SaleManagermentContext();
                Member member = db.Members.Find(id);
                if (member != null)
                {
                    db.Members.Remove(member);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("Member id does not existed!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
    }
}
