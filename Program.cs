// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

using SetCardANN;
CardDeck deck = new CardDeck();
//Console.WriteLine(deck.InputOutputSet.Count);

//foreach (KeyValuePair<double[], double[]> pair in deck.InputOutputSet)
//{
//    if (pair.Value[0] == 1) Console.WriteLine("<{0},{1},{2}>,<{3}>", pair.Key[0].ToString(), pair.Key[1].ToString(), pair.Key[2].ToString(), pair.Value[0].ToString());
//}

//Console.ReadKey();
ANN ann = new ANN();
double[][] inputs = deck.InputOutputSet.Keys.ToArray();
double[][] outputs = deck.InputOutputSet.Values.ToArray();
const double learningRate = 0.05;
Console.WriteLine("----------------BEFORE TRAINING---------------------");
Console.WriteLine("Before training, weights is " + ann.printWeights());
Console.WriteLine("Initial input result: ");
//for (int i = 0; i < inputs.Length; i++)
//{
//    Console.WriteLine("Input entry [" + i + "]: " + ann.forwardPropagate(inputs[i]));
//    if (ann.forwardPropagate(inputs[i]) >= 0.5)
//    {
//        Console.WriteLine("The result of this case might be TRUE before training.");
//    }
//    else
//    {
//        Console.WriteLine("The result of this case might be FALSE before training.");
//    }
//}

ann.train(inputs, outputs, learningRate, 100);

//Console.WriteLine("----------------AFTER TRAINING---------------------");
//Console.WriteLine("After training, weights is " + ann.printWeights());
//Console.WriteLine("Post-training input result: ");
//for (int i = 0; i < inputs.Length; i++)
//{
//    Console.WriteLine("Input entry [" + i + "]: " + ann.forwardPropagate(inputs[i]));
//    if (ann.forwardPropagate(inputs[i]) >= 0.5)
//    {
//        Console.WriteLine("The result of this case might be TRUE after training.");
//    }
//    else
//    {
//        Console.WriteLine("The result of this case might be FALSE after training.");
//    }
//}
//double output = ann.forwardPropagate(testCase);
//Console.WriteLine("----------------TEST CASE---------------------");
//Console.WriteLine("Test case result: " + output);
//if (output >= 0.5)
//{
//    Console.WriteLine("The result of test case might be TRUE after training.");
//}
//else
//{
//    Console.WriteLine("The result of test case might be FALSE after training.");
//}

Console.WriteLine("----------------TEST CASE---------------------");
double totalCases = deck.CardInputOutputSet.Count;
double correctCase = 0;
//foreach (KeyValuePair<double[], double[]> pair in deck.InputOutputSet)
//{
//    double guess = ann.forwardPropagate(pair.Key);
//    //Console.WriteLine("Case: "+deck.SetToString(pair.Key));
//    int normalizedGuess = guess>0.5? 1: 0;
//    //Console.WriteLine("Guess: " + normalizedGuess);
//    int actualValue = (int)pair.Value[0]; 
//    //Console.WriteLine("Actual: " + actualValue);
//    if (normalizedGuess == actualValue) correctCase++;
//    else
//    {
//        Console.WriteLine("Case: " + deck.SetToString(pair.Key));
//        Console.WriteLine("Guess: " + normalizedGuess);
//        Console.WriteLine("Actual: " + actualValue);
//    }
//}

Random random = new Random();
int i;
for (int c = 0; c < 1000; c++)
{
    i = random.Next(53000);
    double[] key = deck.InputOutputSet.Keys.ElementAt(i);
    double[] value = deck.InputOutputSet.Values.ElementAt(i);
    double guess = ann.forwardPropagate(key);
    //Console.WriteLine("Case: "+deck.SetToString(pair.Key));
    int normalizedGuess = guess > 0.5 ? 1 : 0;
    //Console.WriteLine("Guess: " + normalizedGuess);
    int actualValue = (int)value[0];
    //Console.WriteLine("Actual: " + actualValue);
    if (normalizedGuess == actualValue) correctCase++;
    else
    {
        Console.WriteLine("Case: "+i+", " + deck.SetToString(key));
        Console.WriteLine("Guess: " + normalizedGuess);
        Console.WriteLine("Actual: " + actualValue);
    }
}

Console.WriteLine("Correct Case: " + correctCase);
Console.WriteLine("Error Case: " + (1000-correctCase));
Console.WriteLine("Accuracy: " + (correctCase / totalCases));
Console.WriteLine(ann.printWeights());

Console.WriteLine("NOTE: THE TEST CASE CONTAINS 3 ZEROS AND THUS RESULT IN CONSTANT VALUE IN SIGMOID(X)");
Console.WriteLine("A RANDOM BIAS IS GENERATED SO THE RESULT MAY VARY");
Console.WriteLine("-------------END OF RESULT----------------");