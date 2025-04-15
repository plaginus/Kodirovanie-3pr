using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodirovaniePr2
{
    public abstract class Impulse
    {
        private byte a;
        public byte A
        {
            get
            {
                return a;
            }
            set
            {
                if (value > 255) a = 255;
                else a = value;
            }
        }

        private byte tu = 10;
        public byte Tu
        {
            get
            {
                return tu;
            }
        }

        private byte tu1 = 5;
        public byte Tu1
        {
            get
            {
                return tu1;
            }
            set
            {
                if (value > 127) tu1 = 127;
                else if (value < 5) tu1 = 5;
                else tu1 = value;
                tu = (byte)(tu1 + tu2);
            }
        }

        private byte tu2 = 5;
        public byte Tu2
        {
            get
            {
                return tu2;
            }
            set
            {
                if (value > 127) tu2 = 127;
                else if (value < 5) tu2 = 5;
                else tu2 = value;
                tu = (byte)(tu1 + tu2);
            }
        }

        private byte deltaN;
        public byte DeltaN
        {
            get { return deltaN; }
            set
            {
                if (value > 255) deltaN = 255;
                else deltaN = value;
            }
        }

        protected byte[] impulseCords = new byte[1024];
        protected double[] dcCoef = new double[1024];
        

        public void DirectDC()
        {
            for (int k = 0; k < 1024; k++)
            {
                double ak = k == 0 ? Math.Sqrt(1.0 / 1024) : Math.Sqrt(2.0 / 1024);
                double sum = 0;
                for (int n = 0; n < 1024; n++)
                {
                    sum += impulseCords[n] * Math.Cos((Math.PI * (2 * n + 1) * k) / 2048);
                }
                dcCoef[k] = sum * ak;
            }
        }
    
        public double[] ReversDC() 
        {
            double[] result = new double[1024];
            for (int n = 0; n < 1024; n++)
            {
                double sum = 0;
                for (int k = 0; k < 1024; k++)
                {
                    double ak = k == 0 ? Math.Sqrt(1.0 / 1024) : Math.Sqrt(2.0 / 1024);
                    sum += ak * dcCoef[k] * Math.Cos((Math.PI * (2 * n + 1) * k) / 2048);
                }
                result[n] = sum;
            }
            return result;
        }
    
        public double[] LowFreq()
        {
            double[] result = new double[1024];
            for (int k = 0; k < 1024; k++)
            {
                if (k < 1024 / 10 + 1) result[k] = dcCoef[k];
                else result[k] = 0;
            }

            return result;
        }

        public double[] HighFreq()
        {
            double[] result = new double[1024];
            for (int k = 0; k < 1024; k++)
            {
                if (k < 1024 / 10 + 1) result[k] = 0;
                else result[k] = dcCoef[k];
            }

            return result;
        }
    }
}
