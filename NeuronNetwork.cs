using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCardANN
{
    public class NeuralNetwork
    {
        private double[,] weights1; // weights between input layer and first hidden layer
        private double[,] weights2; // weights between first hidden layer and second hidden layer
        private double[] weights3; // weights between second hidden layer and output layer
        private double[] biases1; // biases for first hidden layer
        private double[] biases2; // biases for second hidden layer
        private double bias3; // bias for output layer

        public NeuralNetwork()
        {
            // Initialize weights and biases with random values
            weights1 = InitializeWeights(12, 3);
            weights2 = InitializeWeights(3, 3);
            weights3 = InitializeWeights(3);
            biases1 = InitializeBiases(3);
            biases2 = InitializeBiases(3);
            bias3 = InitializeBiases(1)[0];
        }

        // Initialize weights with random values
        private double[,] InitializeWeights(int rows, int columns)
        {
            var random = new Random();
            var weights = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    weights[i, j] = random.NextDouble() * 2 - 1; // Random value between -1 and 1
                }
            }

            return weights;
        }

        private double[] InitializeWeights(int rows) {
            var random = new Random();
            var weights = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                weights[i] = random.NextDouble() * 2 - 1; // Random value between -1 and 1
            }

            return weights;
        }

        // Initialize biases with random values
        private double[] InitializeBiases(int size)
        {
            var random = new Random();
            var biases = new double[size];

            for (int i = 0; i < size; i++)
            {
                biases[i] = random.NextDouble() * 2 - 1; // Random value between -1 and 1
            }

            return biases;
        }

        // Activation function (sigmoid)
        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        // Derivative of the sigmoid function
        private double SigmoidDerivative(double x)
        {
            double sigmoid = Sigmoid(x);
            return sigmoid * (1 - sigmoid);
        }

        // Feedforward propagation
        public double FeedForward(double[] input)
        {
            double[] hiddenLayer1 = new double[3];
            double[] hiddenLayer2 = new double[3];
            double output = 0;

            // Calculate values for the first hidden layer
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;

                for (int j = 0; j < 12; j++)
                {
                    sum += input[j] * weights1[j, i];
                }

                hiddenLayer1[i] = Sigmoid(sum + biases1[i]);
            }

            // Calculate values for the second hidden layer
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;

                for (int j = 0; j < 3; j++)
                {
                    sum += hiddenLayer1[j] * weights2[j, i];
                }

                hiddenLayer2[i] = Sigmoid(sum + biases2[i]);
            }

            // Calculate output value
            double outputSum = 0;

            for (int i = 0; i < 3; i++)
            {
                outputSum += hiddenLayer2[i] * weights3[i];
            }

            output = Sigmoid(outputSum + bias3);

            return output;
        }

        // Backward propagation (training the network)
        public void Backpropagation(double[] input, double targetOutput, double learningRate)
        {
            // Perform feedforward to calculate intermediate values
            double[] hiddenLayer1 = new double[3];
            double[] hiddenLayer2 = new double[3];
            double output = 0;

            // Calculate values for the first hidden layer
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;

                for (int j = 0; j < 12; j++)
                {
                    sum += input[j] * weights1[j, i];
                }

                hiddenLayer1[i] = Sigmoid(sum + biases1[i]);
            }

            // Calculate values for the second hidden layer
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;

                for (int j = 0; j < 3; j++)
                {
                    sum += hiddenLayer1[j] * weights2[j, i];
                }

                hiddenLayer2[i] = Sigmoid(sum + biases2[i]);
            }

            // Calculate output value
            double outputSum = 0;

            for (int i = 0; i < 3; i++)
            {
                outputSum += hiddenLayer2[i] * weights3[i];
            }

            output = Sigmoid(outputSum + bias3);

            // Calculate output layer error
            double outputError = (targetOutput - output) * SigmoidDerivative(output);

            // Calculate second hidden layer error
            double[] hiddenLayer2Errors = new double[3];
            for (int i = 0; i < 3; i++)
            {
                hiddenLayer2Errors[i] = weights3[i] * outputError * SigmoidDerivative(hiddenLayer2[i]);
            }

            // Calculate first hidden layer error
            double[] hiddenLayer1Errors = new double[3];
            for (int i = 0; i < 3; i++)
            {
                double errorSum = 0;
                for (int j = 0; j < 3; j++)
                {
                    errorSum += weights2[i, j] * hiddenLayer2Errors[j];
                }
                hiddenLayer1Errors[i] = errorSum * SigmoidDerivative(hiddenLayer1[i]);
            }

            // Update weights and biases
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    weights2[i, j] += learningRate * hiddenLayer1[i] * hiddenLayer2Errors[j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    weights1[j, i] += learningRate * input[j] * hiddenLayer1Errors[i];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                weights3[i] += learningRate * hiddenLayer2[i] * outputError;
            }

            for (int i = 0; i < 3; i++)
            {
                biases2[i] += learningRate * hiddenLayer2Errors[i];
            }

            for (int i = 0; i < 3; i++)
            {
                biases1[i] += learningRate * hiddenLayer1Errors[i];
            }

            bias3 += learningRate * outputError;
        }

        public string PrintWeights()
        {
            string result = "Weights 1 ={";
            for (int i = 0; i < weights1.GetLength(0); i++)
            {
                for (int j = 0; j < weights1.GetLength(1); j++)
                {
                    result += "<" + i + "," + j + ">=" + weights1[i, j] + ",";
                }
                
            }
            result += "}, biases=";
            for(int i = 0; i<biases1.Length;i++)
            {
                result += biases1[i] + ",";
            }
            result += "\n";
            result += "Weights 2 ={";
            for (int i = 0; i < weights2.GetLength(0); i++)
            {
                for (int j = 0; j < weights2.GetLength(1); j++)
                {
                    result += "<" + i + "," + j + ">=" + weights2[i, j] + ",";
                }

            }
            result += "}, biases=";
            for (int i = 0; i < biases2.Length; i++)
            {
                result += biases2[i] + ",";
            }
            result += "\n";
            result += "Weights 3 ={";
            for (int i = 0; i < weights3.GetLength(0); i++)
            {
                result += weights3[i] + ",";

            }
            result += "}, biases="+bias3;
            result += "\n";
            return result;
        }
    }

}
