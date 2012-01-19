using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicrosoftResearch.Infer.Models;
using MicrosoftResearch.Infer;
using System.Diagnostics;

namespace BayesianNetworksSample
{
    public partial class BayesianNetworksSampleForm : Form
    {
        public BayesianNetworksSampleForm()
        {
            InitializeComponent();
        }
        public double ConvertToProb(decimal propDec)
        {
            return (double)propDec / 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inferString = InferGraph(
                                    ConvertToProb(numericCloudy.Value),
                                    ConvertToProb(numericSprinklerColudyTrue.Value),
                                    ConvertToProb(numericSprinklerCloudyFalse.Value),
                                    ConvertToProb(numericRainCloudyTrue.Value),
                                    ConvertToProb(numericRainCloudyFalse.Value),
                                    ConvertToProb(numericWetGrassSprinklerTrueRainTrue.Value),
                                    ConvertToProb(numericWetGrassSprinklerTrueRainFalse.Value),
                                    ConvertToProb(numericWetGrassSprinklerFalseRainTrue.Value),
                                    ConvertToProb(numericWetGrassSprinklerFalseRainFalse.Value));
           textBox1.Text = inferString;
        }

        private string InferGraph(double cloudyProb, 
            double sprinklerCloudyTProb, double sprinklerCloudyFProb,
            double rainCloudyTProb, double rainCloudyFProb,
            double wetGrassSprinklerTRainT, double wetGrassSprinklerTRainF,
            double wetGrassSprinklerFRainT, double wetGrassSprinklerFRainF

            )
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                Variable<bool> cloudy = Variable.Bernoulli(cloudyProb).Named("Cloudy");
                
                Variable<bool> sprinkler = Variable.New<bool>().Named("Sprinkler");
                using (Variable.If(cloudy)) sprinkler.SetTo(Variable.Bernoulli(sprinklerCloudyTProb));
                using (Variable.IfNot(cloudy)) sprinkler.SetTo(Variable.Bernoulli(sprinklerCloudyFProb));

                Variable<bool> rain = Variable.New<bool>().Named("Rain");
                using (Variable.If(cloudy)) rain.SetTo(Variable.Bernoulli(rainCloudyTProb));
                using (Variable.IfNot(cloudy)) rain.SetTo(Variable.Bernoulli(rainCloudyFProb));

                Variable<bool> wetGrass = Variable.New<bool>().Named("WetGrass");
                using (Variable.If(sprinkler))
                {
                    using (Variable.If(rain)) wetGrass.SetTo(Variable.Bernoulli(wetGrassSprinklerTRainT));
                    using (Variable.IfNot(rain)) wetGrass.SetTo(Variable.Bernoulli(wetGrassSprinklerTRainF));
                }
                using (Variable.IfNot(sprinkler))
                {
                    using (Variable.If(rain)) wetGrass.SetTo(Variable.Bernoulli(wetGrassSprinklerFRainT));
                    using (Variable.IfNot(rain)) wetGrass.SetTo(Variable.Bernoulli(wetGrassSprinklerFRainF));
                }

                string rFormat = "Time= {0}" + Environment.NewLine
                                + "P(R | W)= {1}" + Environment.NewLine
                                + "P(S | W)= {2}" + Environment.NewLine
                                + "P(R | W, ~C)= {3}" + Environment.NewLine
                                + "P(S | W, ~C)= {4}" + Environment.NewLine
                                + "P(R | W, S)={5}" + Environment.NewLine
                                + "P(R | W, S, ~C)={6}";


                wetGrass.ObservedValue = true; ;
                InferenceEngine ie = new InferenceEngine();
                ie.ShowProgress = true;
                ie.ShowTimings = true;
                ie.ShowWarnings = true;
               

                ie.ShowFactorGraph = ShowFactorGraphCheckbox.Checked;
                
                
                ie.ShowSchedule = ShowScheduleCheckbox.Checked;
                
                

                var p1 = (MicrosoftResearch.Infer.Distributions.Bernoulli)ie.Infer(rain);
                var p2 = (MicrosoftResearch.Infer.Distributions.Bernoulli)ie.Infer(sprinkler);

                
                cloudy.ObservedValue = false;
                var p3 = (MicrosoftResearch.Infer.Distributions.Bernoulli)ie.Infer(rain);
                var p4 = (MicrosoftResearch.Infer.Distributions.Bernoulli)ie.Infer(sprinkler);


                cloudy.ClearObservedValue();
                sprinkler.ObservedValue = true;
                var p5 = (MicrosoftResearch.Infer.Distributions.Bernoulli)ie.Infer(rain);


                cloudy.ObservedValue = false;
                var p6 = (MicrosoftResearch.Infer.Distributions.Bernoulli)ie.Infer(rain);



                string formatString = "0.000";
                stopwatch.Stop();
                return string.Format(rFormat, stopwatch.ElapsedMilliseconds+" ms",
                                              p1.GetProbTrue().ToString(formatString),
                                              p2.GetProbTrue().ToString(formatString),
                                              p3.GetProbTrue().ToString(formatString),
                                              p4.GetProbTrue().ToString(formatString),
                                              p5.GetProbTrue().ToString(formatString),   
                                              p6.GetProbTrue().ToString(formatString)
                                              );
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PicSchema picSchema = new PicSchema();
            picSchema.ShowDialog();
        }




    }
}
