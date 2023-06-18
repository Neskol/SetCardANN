using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetCardANN;

using static System.Runtime.InteropServices.JavaScript.JSType;

CardDeck deck = new CardDeck();
var neuralNetwork = new NeuralNetwork();
// Training data

List<double[]> selectedInput = new List<double[]>();
List<double[]> selectedOutput = new List<double[]>();
selectedInput.AddRange(deck.SetInputOutput.Keys);
selectedOutput.AddRange(deck.SetInputOutput.Values);
Random rnd = new Random();
for (int i = 0; i < 200; i++)
{
    int index = rnd.Next(50000);
    selectedInput.Add(deck.InputOutputSet.Keys.ElementAt(index));
    selectedOutput.Add(deck.InputOutputSet.Values.ElementAt(index));
}


double[][] trainingDataInputs = selectedInput.ToArray();

double[] trainingDataOutputs = new double[selectedOutput.Count];
double[][] candidate = selectedOutput.ToArray();
for (int i = 0; i < trainingDataInputs.Length; i++)
{
    trainingDataOutputs[i] = candidate[i][0];
}

//double[][] trainingDataInputs = deck.SetInputOutput.Keys.ToArray();

//double[] trainingDataOutputs = new double[deck.SetInputOutput.Values.Count];
//double[][] candidate = deck.SetInputOutput.Values.ToArray();
//for (int i = 0; i < trainingDataInputs.Length; i++)
//{
//    trainingDataOutputs[i] = candidate[i][0];
//}

Console.WriteLine("Start Training");
//double[][] trainingDataInputs =
//{
//            new double[] {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0},
//            // Add more training inputs here
//        };

//double[] trainingDataOutputs =
//{
//            0.75,
//            // Add corresponding training outputs here
//        };

// Train the network for a specified number of epochs
int epochs = 100;
double learningRate = 0.05;

for (int epoch = 0; epoch < epochs; epoch++)
{
    Console.Write("At epoch {0}, ",epoch);
    double error = 0;
    for (int i = 0; i < trainingDataInputs.Length; i++)
    {
        double[] input = trainingDataInputs[i];
        double targetOutput = trainingDataOutputs[i];
        error += neuralNetwork.FeedForward(trainingDataInputs[i]) - targetOutput;
        neuralNetwork.Backpropagation(input, targetOutput, learningRate);
    }
    Console.WriteLine("the error is {0}", error / 4);
}

Console.WriteLine("Finished Training");

//// Test the trained network
//double[] inputToTest = new double[12] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 };
//double output = neuralNetwork.FeedForward(inputToTest);
//Console.WriteLine("Output: " + output);

Console.WriteLine("----------------TEST CASE---------------------");
double totalCases = deck.InputOutputSet.Count;
double correctCase = 0;
foreach (KeyValuePair<double[], double[]> pair in deck.InputOutputSet)
{
    double guess = neuralNetwork.FeedForward(pair.Key);
    //Console.WriteLine("Case: "+deck.SetToString(pair.Key));
    //guess = ANN.DSigmoid(guess);
    int normalizedGuess = guess > 0.5 ? 1 : 0;
    //Console.WriteLine(guess);
    //Console.WriteLine("Guess: " + normalizedGuess);
    int actualValue = (int)pair.Value[0];
    //Console.WriteLine("Actual: " + actualValue);
    if (normalizedGuess == actualValue) correctCase++;
    //    else
    //    {
    //        //Console.WriteLine("Case: " + deck.SetToString(pair.Key));
    //        Console.WriteLine("Guess: " + normalizedGuess);
    //        Console.WriteLine("Actual: " + actualValue);
    //    }
}

// Random random = new Random();
// int i;
// for (int c = 0; c < 1000; c++)
// {
//     i = random.Next(53000);
//     double[] key = deck.InputOutputSet.Keys.ElementAt(i);
//     double[] value = deck.InputOutputSet.Values.ElementAt(i);
//     double guess = ann.forwardPropagate(key);
//     //Console.WriteLine("Case: "+deck.SetToString(pair.Key));
//     int normalizedGuess = guess > 0.5 ? 1 : 0;
//     //Console.WriteLine("Guess: " + normalizedGuess);
//     int actualValue = (int)value[0];
//     //Console.WriteLine("Actual: " + actualValue);
//     if (normalizedGuess == actualValue) correctCase++;
//     else
//     {
//         Console.WriteLine("Case: "+i+", " + deck.SetToString(key));
//         Console.WriteLine("Guess: " + normalizedGuess);
//         Console.WriteLine("Actual: " + actualValue);
//     }
// }

Console.WriteLine("Correct Case: " + correctCase);
Console.WriteLine("Error Case: " + (totalCases - correctCase));
Console.WriteLine("Accuracy: " + (correctCase / (totalCases)));
Console.WriteLine(neuralNetwork.PrintWeights());

Console.WriteLine("Among them all, for a real set, the accuracy is");
double totalTrueCases = deck.SetInputOutput.Count;
double correctTrueCase = 0;
foreach (KeyValuePair<double[], double[]> pair in deck.SetInputOutput)
{
    double guess = neuralNetwork.FeedForward(pair.Key);
    //Console.WriteLine("Case: "+deck.SetToString(pair.Key));
    //guess = ANN.DSigmoid(guess);
    int normalizedGuess = guess > 0.5 ? 1 : 0;
    //Console.WriteLine(guess);
    //Console.WriteLine("Guess: " + normalizedGuess);
    int actualValue = (int)pair.Value[0];
    //Console.WriteLine("Actual: " + actualValue);
    if (normalizedGuess == actualValue) correctTrueCase++;
    //    else
    //    {
    //        //Console.WriteLine("Case: " + deck.SetToString(pair.Key));
    //        Console.WriteLine("Guess: " + normalizedGuess);
    //        Console.WriteLine("Actual: " + actualValue);
    //    }
}

Console.WriteLine("Correct Case: " + correctTrueCase);
Console.WriteLine("Error Case: " + (totalTrueCases - correctTrueCase));
Console.WriteLine("Accuracy: " + (correctTrueCase / (totalTrueCases)));
