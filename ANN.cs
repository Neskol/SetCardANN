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
            weights = new double[12];
            bias = rnd.NextDouble();
            // bias = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = rnd.NextDouble();
            }
        }

        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public static double DSigmoid(double x)
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        }

        public double forwardPropagate(double[] inputs)
        {
            // assert inputs.length == weights.length:"Input size does not match weight size";
            Array.Sort(inputs);
            double sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += (inputs[i] + bias) * weights[i];
            }
            return Sigmoid(sum);
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
                        double adjustment = error * inputs[j][i] * DSigmoid(output);
                        weights[i] += learningRate * adjustment;
                    }
                }
                Console.WriteLine(((double)iteration * 100 / numIterations) + "% with error of " + averageError / 4);

            }
        }

        public string printWeights()
        {
            string result = "{";
            for (int i = 0; i < weights.Length; i++)
            {
                result += this.weights[i] + ",";
            }
            result += "}, bias=" + this.bias;
            return result;
        }
    }
}
