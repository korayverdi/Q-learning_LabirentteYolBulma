using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kormus
{
    public class Class1
    {   
        public  Double[,] QMat;
        public  string[,] RMat;
        public int[] pathMatris;
        public Class1() { 

         QMat = new Double[Form1.satirGlobal, Form1.satirGlobal];
         RMat = new string[Form1.satirGlobal, Form1.satirGlobal];
         pathMatris = new int[Form1.pathb];
        }

        }
        
        
    
}
