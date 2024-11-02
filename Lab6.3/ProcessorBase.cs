using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab6._3
{
    public abstract class ProcessorBase
    {

        public string name { get; set; }
        public string manufacturer { get; set; }
        public int core { get; set; }
        public double frequency { get; set; }
        public double tdp { get; set; }
        public double performancePerCore { get; set; }
        public bool multiPrecision { get; set; }
        public bool energySaving { get; set; }


        public ProcessorBase()
        {

        }


        public ProcessorBase(string Name, string Manufacturer, int Core, double Frequency, double TDP, double PerformancePerCore, bool MP,
                bool ES)
        {
            name = Name;
            manufacturer = Manufacturer;
            core = Core;
            frequency = Frequency;
            tdp = TDP;
            performancePerCore = PerformancePerCore;
            multiPrecision = MP;
            energySaving = ES;
        }

    }

}
