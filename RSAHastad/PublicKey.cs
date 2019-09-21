using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSAHastad
{
    public class PublicKey
    {
        public BigInteger e {get; set;}
        public BigInteger n { get; set; }
        public BigInteger C { get; set; }
        public BigInteger Ni { get; set; }
        public BigInteger NiMultiplicative { get; set; }
        public BigInteger Xi { get; set; }


        public PublicKey(BigInteger _n, BigInteger _e)
        {
            n = _n;
            e = _e;
        }
    }
}
