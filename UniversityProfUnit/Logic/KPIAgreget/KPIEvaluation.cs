using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic.KPIAgreget
{
    public class KPIEvaluation
    {
        private KPIEvaluation()
        {

        }
        public int KPIEvaluationId { get; internal set; }
        public string KPIEvaluationName { get; internal set; }
        public int KPIEvaluationDegree { get; internal set; }
    }
}
