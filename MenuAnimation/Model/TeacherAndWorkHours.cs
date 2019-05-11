using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astmara6.Model
{
    class TeacherAndWorkHours
    {
        private int id;
        private string nickName;
        private string name;
        private string rank;
        private int quorum;
        private bool academicOrVirtual;

        public int Id { get => id; set => id = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public string Name { get => name; set => name = value; }
        public string Rank { get => rank; set => rank = value; }
        public int Quorum { get => quorum; set => quorum = value; }
        public bool AcademicOrVirtual { get => academicOrVirtual; set => academicOrVirtual = value; }

        public TeacherAndWorkHours()
        {

        }
        public TeacherAndWorkHours(int id, string nickName, string name, string rank, int quorum, bool academicOrVirtual)
        {
            Id = id;
            NickName = nickName;
            Name = name;
            Rank = rank;
            Quorum = quorum;
            AcademicOrVirtual = academicOrVirtual;
        }
    }
}
