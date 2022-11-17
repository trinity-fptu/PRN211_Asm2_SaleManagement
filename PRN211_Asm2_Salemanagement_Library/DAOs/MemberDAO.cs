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
                db.Members.Add(member);
                db.SaveChanges();
                result = true;
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
                db.Members.Update(member);
                db.SaveChanges();
                result = true;
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
                db.Members.Remove(member);
                db.SaveChanges();
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        //Search member by id
        public IEnumerable<Member> SearchMemberById(int id)
        {
            IEnumerable<Member> members = null;
            try
            {
                var db = new SaleManagermentContext();
                members = db.Members.Where(m => m.MemberId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return members;
        }

        //Search member by email
        public IEnumerable<Member> SearchMemberByEmail(string email)
        {
            IEnumerable<Member> members = null;
            try
            {
                var db = new SaleManagermentContext();
                members = db.Members.Where(m => m.Email.Contains(email));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return members;
        }

        //Get city list
        public IEnumerable<string> GetCityList(string country)
        {
            IEnumerable<string> cities = null;
            try
            {
                var db = new SaleManagermentContext();
                cities = db.Members.Where(x=>x.Country==country).Select(m => m.City).Distinct();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cities;
        }

        //Get country list
        public IEnumerable<string> GetCountryList()
        {
            IEnumerable<string> countries = null;
            try
            {
                var db = new SaleManagermentContext();
                countries = db.Members.Select(m => m.Country).Distinct();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return countries;
        }

        //Filter member by city
        public IEnumerable<Member> FilterMemberByCity(string city, string country)
        {
            IEnumerable<Member> members = null;
            try
            {
                var db = new SaleManagermentContext();
                members = db.Members.Where(m => m.City == city && m.Country == country);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return members;
        }

        //Filter member by country
        public IEnumerable<Member> FilterMemberByCountry(string country)
        {
            IEnumerable<Member> members = null;
            try
            {
                var db = new SaleManagermentContext();
                members = db.Members.Where(m => m.Country == country);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return members;
        }

        //Check duplicate id
        public bool CheckDuplicateId(int id)
        {
            bool result = false;
            try
            {
                var db = new SaleManagermentContext();
                if (db.Members.Find(id) != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        //Check duplicate email
        public bool CheckDuplicateEmail(string email)
        {
            bool result = false;
            try
            {
                var db = new SaleManagermentContext();
                if (db.Members.FirstOrDefault(m => m.Email == email) != null)
                {
                    result = true;
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
