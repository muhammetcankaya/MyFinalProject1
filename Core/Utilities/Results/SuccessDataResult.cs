using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        // Bu yapıda DataResulttan miras aslında bir bunu ikiye parçadık 
        // biri succes biri error bunları ayırmamızda sistemi mükemmelleiştirdi
        public SuccessDataResult(T data,string message):base(data,true,message)
        {
            
        }
        public SuccessDataResult(T data):base(data,true) 
        {
            
        }
        //Bu ikisini alttakileri çok fazla kullanmayız

        public SuccessDataResult(string message):base(default,true,message)
        {
            
        }
        public SuccessDataResult():base(default,true)
        {
            
        }


    }
}
