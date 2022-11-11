using PRN211_Asm2_Salemanagement_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211_Asm2_Salemanagement_Library.Repos.MemberRepo
{
    public interface IMemberRepo
    {
        public IEnumerable<Member> GetAllMembers();
        public Member Login(string email, string password);
        public Member GetMemberById(int id);
        public Member GetMemberByEmail(string email);
        public bool AddMember(Member member);
        public bool UpdateMember(Member member);
        public bool DeleteMember(int id);
    }
}
