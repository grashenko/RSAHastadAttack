using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RSAHastad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<PublicKey> publicKeys = new List<PublicKey>();

        private void button1_Click(object sender, EventArgs e)
        {
            
            label5.Text = "";
            richTextBox1.Text = "";
            var publicE = BigInteger.Parse(textBox1.Text);
            var listPrimes = get_primes(2000);
            listPrimes.Reverse();
            var listP = listPrimes.Skip(0).Take((listPrimes.Count/2) - 0).ToList();
            var listQ = listPrimes.Skip(listPrimes.Count / 2).Take(listPrimes.Count / 2).ToList();
            int counter = 0;
            for (var i = 0; i < listP.Count; i++)
            {
                if (((listP[i] - 1) * (listQ[i] - 1)) % publicE != 0)
                {
                    counter++;
                    publicKeys.Add(new PublicKey(listP[i] * listQ[i], publicE));
                    if (counter == publicE)
                    {
                        break;
                    }
                }
            }

            var message = BigInteger.Parse(textBox2.Text);
            counter = 0;
            foreach (var pk in publicKeys)
            {
                counter++;
                pk.C = BigInteger.ModPow(message, pk.e , pk.n);
                richTextBox1.AppendText($"{counter}.< e={pk.e}; n={pk.n}; c={pk.C}>\n");
            }


            BigInteger commonN = 1;
            foreach (var pk in publicKeys)
            {
                commonN *= pk.n;
            }

            foreach (var pk in publicKeys)
            {
                pk.Ni = commonN / pk.n;
                pk.NiMultiplicative = modInverse(pk.Ni, pk.n);
                pk.Xi = pk.Ni * pk.NiMultiplicative * pk.C;

            }

            BigInteger Z = 0;
            foreach (var pk in publicKeys)
            {
                Z += pk.Xi;
            }

            Z = Z % commonN;

            var openMessage = Math.Round(Math.Pow(Math.E, BigInteger.Log(Z) / (int)publicE));

            label5.Text = openMessage.ToString();

            publicKeys = new List<PublicKey>();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public static List<BigInteger> get_primes(int n)
        {

            bool[] is_prime = new bool[n + 1];
            for (int i = 2; i <= n; ++i)
                is_prime[i] = true;

            List<BigInteger> primes = new List<BigInteger>();

            for (int i = 2; i <= n; ++i)
                if (is_prime[i])
                {
                    primes.Add((BigInteger)i);
                    if (i * i <= n)
                        for (int j = i * i; j <= n; j += i)
                            is_prime[j] = false;
                }

            return primes;
        }

        static BigInteger modInverse(BigInteger a, BigInteger m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
