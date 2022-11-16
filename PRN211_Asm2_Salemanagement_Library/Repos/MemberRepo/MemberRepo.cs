using PRN211_Asm2_Salemanagement_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.DAOs;

namespace PRN211_Asm2_Salemanagement_Library.Repos.MemberRepo
{
    public class MemberRepo : IMemberRepo
    {
        public bool AddMember(Member member) => MemberDAO.Instance.AddMember(member);

        public bool DeleteMember(int id) => MemberDAO.Instance.DeleteMember(id);

        public IEnumerable<Member> GetAllMembers() => MemberDAO.Instance.GetAllMembers();

        public Member GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);

        public Member GetMemberById(int id) => MemberDAO.Instance.GetMemberById(id);

        public Member Login(string email, string password) => MemberDAO.Instance.Login(email, password);

        public bool UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);

        public IEnumerable<Member> SearchMemberById(int id) => MemberDAO.Instance.SearchMemberById(id);

        public IEnumerable<Member> SearchMemberByEmail(string email) => MemberDAO.Instance.SearchMemberByEmail(email);

        public IEnumerable<string> GetCityList() => MemberDAO.Instance.GetCityList();

        public IEnumerable<string> GetCountryList() => MemberDAO.Instance.GetCountryList();

        public IEnumerable<Member> FilterMemberByCity(string city) => MemberDAO.Instance.FilterMemberByCity(city);

        public IEnumerable<Member> FilterMemberByCountry(string country) => MemberDAO.Instance.FilterMemberByCountry(country);

        public bool CheckDuplicateEmail(string email) => MemberDAO.Instance.CheckDuplicateEmail(email);

        public bool CheckDuplicateId(int id) => MemberDAO.Instance.CheckDuplicateId(id);
    }
}
