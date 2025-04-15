using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Reflection;

namespace KodirovaniePr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ViewImpulse();
        }

        private TringleImpulse trImpulse1 = new TringleImpulse();
        private CosSquareImpulse cSImpulse = new CosSquareImpulse();

        private void amplitude_ValueChanged(object sender, EventArgs e)
        {
            trImpulse1.A = (byte)amplitude.Value;
            cSImpulse.A = (byte)amplitude.Value;
        }
        private void tau1_ValueChanged(object sender, EventArgs e)
        {
            trImpulse1.Tu1 = (byte)tau1.Value;
            cSImpulse.Tu1 = (byte)tau1.Value;
        }
        private void tau2_ValueChanged(object sender, EventArgs e)
        {
            trImpulse1.Tu2 = (byte)tau2.Value;
            cSImpulse.Tu2 = (byte)tau2.Value;
        }
        private void deltaN_ValueChanged(object sender, EventArgs e)
        {
            trImpulse1.DeltaN = (byte)deltaN.Value;
            cSImpulse.DeltaN = (byte)deltaN.Value;
        }

        private void ViewImpulse()
        {
            cSImpulse.DirectDC();

            var modelTr = new PlotModel { Title = "Треугольный импульс" };
            var modelcS = new PlotModel { Title = "Косинусквадратный импульс" };
            var modelLF = new PlotModel { Title = "НЧ дискретное преобразование" };
            var modelHF = new PlotModel { Title = "ВЧ дискретное преобразование" };
            var modelrDC = new PlotModel { Title = "Обратное дискретное преобразование" };

            AreaSeries seriesTr = new AreaSeries();
            AreaSeries seriescS = new AreaSeries();
            AreaSeries seriesLF = new AreaSeries();
            AreaSeries seriesHF = new AreaSeries();
            AreaSeries seriesrDC = new AreaSeries();

            DiagBilder(ref seriesTr, trImpulse1.ImpulseCoordinations());
            DiagBilder(ref seriescS, cSImpulse.ImpulseCoordinations());
            DiagBilder(ref seriesLF, cSImpulse.LowFreq());
            DiagBilder(ref seriesHF, cSImpulse.HighFreq());
            DiagBilder(ref seriesrDC, cSImpulse.ReversDC());

            modelTr.Series.Add(seriesTr);
            modelcS.Series.Add(seriescS);
            modelLF.Series.Add(seriesLF);
            modelHF.Series.Add(seriesHF);
            modelrDC.Series.Add(seriesrDC);

            plotView1.Model = modelTr;
            plotView2.Model = modelcS;
            plotView3.Model = modelLF;
            plotView5.Model = modelHF;
            plotView4.Model = modelrDC;
        }

        private void DiagBilder(ref AreaSeries series, byte[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                series.Points.Add(new DataPoint(i, arr[i]));
            }
        }
        private void DiagBilder(ref AreaSeries series, double[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                series.Points.Add(new DataPoint(i, arr[i]));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewImpulse();
        }
    }
}
