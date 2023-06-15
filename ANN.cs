using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCardANN
{
    public class ANN
    {
        private double[] weights;
        private double bias;

        public ANN()
        {
            Random rnd = new Random();
            weights = new double[3];
            bias = rnd.NextDouble();
            // bias = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = rnd.NextDouble();
            }
        }

        private double sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        private double dSigmoid(double x)
        {
            return sigmoid(x) * (1 - sigmoid(x));
        }

        public double forwardPropagate(double[] inputs)
        {
            // assert inputs.length == weights.length:"Input size does not match weight size";
            double sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += (inputs[i] + bias) * weights[i];
            }
            return sigmoid(sum);
        }

        public void train(double[][] inputs, double[][] desiredOutput, double learningRate, int numIterations)
        {
            // assert inputs[0].length == weights.length : "Input size does not match weight size";

            for (int iteration = 0; iteration < numIterations; iteration++)
            {
                double averageError = 0.0;
                for (int j = 0; j < inputs.Length; j++)
                {
                    double output = forwardPropagate(inputs[j]);
                    double error = desiredOutput[j][0] - output;
                    averageError += error;
                    for (int i = 0; i < weights.Length; i++)
                    {
                        double adjustment = error * inputs[j][i] * dSigmoid(output);
                        weights[i] += learningRate * adjustment;
                    }
                }
                if (((int)((double)iteration * 100 / numIterations))%10==0)
                {
                    Console.WriteLine(((double)iteration * 100 / numIterations) + "% with error of " + averageError / 4);
                }
                
            }
        }

        public string printWeights()
        {
            return "{" + this.weights[0] + ", " + this.weights[1] + ", " + this.weights[2]+"}";
        }
    }
}
