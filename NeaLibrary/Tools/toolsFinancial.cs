using NeaLibrary.DataStructures;
namespace NeaLibrary.Tools
{
    public partial class Tools
    {
        /// <summary>
        /// Stochastic oscillator over Closing prices, of length closing prices. Between 0 and 1
        /// </summary>
        /// <param name="ClosingPrices"></param>
        /// <returns>between 0 and 1</returns>
        public static double StochasticOscillator(Vector ClosingPrices)
        {
            double max = ClosingPrices.Max();
            double min = ClosingPrices.Min();
            return (ClosingPrices[0]-min)/(max-min);
        }

        public static double AroonOscillator(Vector ClosingPrices)
        {
            int n = ClosingPrices.dimension;
            int aroon_up = (n - IndexOf(ClosingPrices,ClosingPrices.Max())) / n;
            int aroon_down = (n - IndexOf(ClosingPrices,ClosingPrices.Min())) / n;

            return aroon_up-aroon_down;
        }
        public static (double,double) RS_Step1(Vector closes, int span=14)///<summary>Vector of close prices, most recent last</summary>
        {
            int n = closes.dimension;
            if (n < span) throw new Exception("Need more data values");
            double gains=0;
            double losses=0;
            double temp = closes[0];
            for (int i = 1; i < span; i++)
            {
                if (closes[i]-temp>0)
                {
                    gains += closes[i]-temp;
                }
                else
                {
                    losses += temp - closes[i];
                }
                temp = closes[i];
            }
            gains = gains/n;
            losses = losses/n;
            return (gains, losses);
        }
        public static (double,double) RS_Repetitive(double prev_av_gain, double prev_av_loss, double gain, double loss, int span=14)
        {
            prev_av_gain = ((span - 1) * prev_av_gain + gain)/span;
            prev_av_loss = ((span - 1) * prev_av_loss + loss)/span;
            return (prev_av_gain,prev_av_loss);
        }
        public static double RSI(double prev_av_gain, double prev_av_loss) => 1-(1 / (1+ (prev_av_gain/prev_av_loss) ) );
        public static IEnumerable<double> RSI(IEnumerable<double> closing_prices, int span=14)
        { 
            int n = 0;
            double temp = 0;
            double gain=0, loss=0;
            Vector stage1 = new Vector(span);
            foreach (double val in closing_prices)
            {
                if (n<span)
                {//first stage
                    stage1.Insert_at_end_Same_Length(val);
                    yield return 0.5; //sensible default value
                }
                else if(n==span)
                {
                    (gain, loss) = RS_Step1(stage1,span);
                    yield return RSI(gain,loss);
                }
                else 
                {
                    double cur_g = (val - temp > 0) ? val-temp : 0;
                    double cur_l = (val - temp < 0) ? temp-val : 0;
                    (gain,loss)=RS_Repetitive(gain,loss, cur_g, cur_l);
                    yield return RSI(gain,loss);
                }
                temp = val;
                n++;
            }
        }
    }
}