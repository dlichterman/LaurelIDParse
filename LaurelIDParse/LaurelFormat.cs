using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaurelIDParse
{
    
    public class LaurelFormat
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_INITIAL { get; set; }
        public string LAST_NAME { get; set; }
        public string NAME_SUFFIX { get; set; }
        public string STREET_ADDRESS { get; set; }
        public string PO_BOX { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP_CODE { get; set; }
        public string SSN { get; set; }
        public string FRN { get; set; }
        public string CALL_SIGN { get; set; }
        public string TELEPHONE { get; set; }
        public string E_MAIL { get; set; }

        public void SetLaurelFormat(string txt)
        {
            string[] s = txt.Split('\n');
            foreach(string s2 in s)
            {
                if(s2.Length > 3)
                {
                    switch (s2.Substring(0,3))
                    {
                        case "DCS" :
                            this.LAST_NAME = s2.Substring(3,s2.Length-3);
                            break;
                        case "DAC":
                            this.FIRST_NAME = s2.Substring(3, s2.Length - 3);
                            break;
                        case "DAD":
                            this.MIDDLE_INITIAL = s2.Substring(3, s2.Length - 3);
                            break;
                        case "DAG":
                            this.STREET_ADDRESS = s2.Substring(3, s2.Length - 3);
                            break;
                        case "DAI":
                            this.CITY = s2.Substring(3, s2.Length - 3);
                            break;
                        case "DAJ":
                            this.STATE = s2.Substring(3, s2.Length - 3);
                            break;
                        case "DAK":
                            this.ZIP_CODE = s2.Substring(3, s2.Length - 3);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    
}
