using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IFinalGradesService
    {

        Final_Grades FindById(int id);

        void Add(Final_Grades grade);
        void Edit(Final_Grades grade);
        void Delete(int id);
        void Dispose();
    }
}
