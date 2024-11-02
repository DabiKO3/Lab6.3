using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6._3
{
    public class Processor : ProcessorBase
    {
       public Processor() : base()
        {

        }

        public double CalculateTotalPerformance()
        {
            return core * performancePerCore;
        }

        public double yearIncomePerInhabitant
        {
            get
            {
                return CalculateTotalPerformance();
            }

        }

        public Processor(string Name, string Manufacturer, int Core, double Frequency, double TDP, double PerformancePerCore, bool MP,
                bool ES) : base( Name, Manufacturer, Core, Frequency,TDP, PerformancePerCore, MP, ES )
        {

        }

    }

}
