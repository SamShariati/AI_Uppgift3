using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN : MonoBehaviour
{

    public int[] networkShape = { 5, 32, 2 };
    public Layer[] layers;
    //public Layer hidden1;
    //public Layer hidden2;
    //public Layer output;


    public class Layer
    {
        //vilken nod vi vill ha, vilken vikt vi vill ha:
        public float[,] weightsArray;


        public float[] biasesArray;
        public float[] nodeArray;

        private int n_nodes;
        private int n_inputs;

        public Layer(int n_inputs, int n_nodes)
        {

            this.n_nodes = n_nodes;
            this.n_inputs = n_inputs;

            weightsArray = new float[n_nodes, n_inputs];
            biasesArray = new float[n_nodes];
            nodeArray = new float[n_nodes];

        }


        public void forward(float[] inputArray)
        {

            nodeArray = new float[n_nodes];

            for(int i = 0; i < n_nodes; i++)
            {
                //summan av vikter * inputs
                for(int j=0; j < n_inputs; j++)
                {
                    nodeArray[i] += weightsArray[i, j] * inputArray[j];
                }

                //bias

                nodeArray[i] += biasesArray[i];

            }
              
        }

        public void Activation()
        {
            for(int i =0;i<n_nodes; i++)
            {

                if (nodeArray[i] < 0)
                {
                    nodeArray[i] = 0;
                }
            }
        }


        public void MutateLayer(float mutationChance, float mutationAmount)
        {
            for(int i = 0; i < n_nodes; i++)
            {
                for(int j = 0; j < n_inputs; j++)
                {
                    if(Random.value < mutationChance)
                    {
                        weightsArray[i, j] += Random.Range(-1.0f, 1.0f) * mutationAmount;
                    }
                }

                if(Random.value < mutationChance)
                {
                    biasesArray[i] += Random.Range(-1.0f, 1.0f) * mutationAmount;
                }
            }
        }
    }

    public void Awake()
    {
        layers = new Layer[networkShape.Length - 1];

        for(int i=0; i<layers.Length; i++)
        {
            layers[i] = new Layer(networkShape[i], networkShape[i + 1]);
        }
        //hidden1 = new Layer(2, 4);
        //hidden2 = new Layer(4, 4);
        //output = new Layer(4, 2);
    }

    public float[] Brain(float[] inputs)
    {

        for(int i=0;i<layers.Length; i++)
        {

            if(i == 0)
            {
                layers[i].forward(inputs);
                layers[i].Activation();
            }

            else if(i== layers.Length - 1)
            {
                layers[i].forward(layers[i - 1].nodeArray);
            }
            else
            {
                layers[i].forward(layers[i-1].nodeArray);
                layers[i].Activation();
            }
        }

        return (layers[layers.Length - 1].nodeArray);


    }

    public Layer[] CopyLayers()
    {
        Layer[] tempLayers = new Layer[networkShape.Length - 1];
        for(int i=0; i< layers.Length; i++)
        {
            tempLayers[i] = new Layer(networkShape[i], networkShape[i + 1]);
            System.Array.Copy(layers[i].weightsArray, tempLayers[i].weightsArray, layers[i].weightsArray.GetLength(0) * layers[i].weightsArray.GetLength(1));
            System.Array.Copy(layers[i].biasesArray, tempLayers[i].biasesArray, layers[i].biasesArray.GetLength(0) * layers[i].biasesArray.GetLength(1));
        }
        return (tempLayers);
    }

    public void MutateNetwork(float mutationChance, float mutationAmount)
    {
        for(int i=0; i<layers.Length; i++)
        {
            layers[i].MutateLayer(mutationChance, mutationAmount);
        }
    }
}
