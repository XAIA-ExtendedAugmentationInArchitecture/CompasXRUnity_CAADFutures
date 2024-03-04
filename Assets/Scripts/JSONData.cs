using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSON
{   
    
   /////////////Classes for Assembly Desearialization./////////////// 
    [System.Serializable]
    public class Node
    {
        public Part part { get; set; }
        public string type_data { get; set; }
        public string type_id { get; set; }
        public Attributes attributes { get; set; }
    }

    [System.Serializable]
    public class Part
    {
        public Frame frame { get; set; }
        public string dtype { get; set; }

    }

    [System.Serializable]
    public class Attributes
    {
        // public string device_id { get; set; }
        public bool is_built { get; set;}
        public bool is_planned { get; set;}
        public string placed_by { get; set; }
        public float length { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public string type { get; set; }
    } 

    [System.Serializable]
    public class Frame
    {
        public float[] point { get; set; }
        public float[] xaxis { get; set; }
        public float[] yaxis { get; set; }

        // Method to parse an instance of the class from a json string
        public static Frame Parse(Dictionary<string, object> frameDataDict)
        {            
            //Create a new instance of the class
            Frame frame = new Frame();

            Debug.Log($"Frame Data: {JsonConvert.SerializeObject(frameDataDict)}");
            Debug.Log($"Frame Data: {JsonConvert.SerializeObject(frameDataDict["point"])}");
            Debug.Log($"Frame Data: {JsonConvert.SerializeObject(frameDataDict["xaxis"])}");
            Debug.Log($"Frame Data: {JsonConvert.SerializeObject(frameDataDict["yaxis"])}");
            Debug.Log($"Frame Data type: {frameDataDict.GetType()}");
            Debug.Log($"Frame Data Dictionary: {JsonConvert.SerializeObject(frameDataDict)}");
            Debug.Log("Frame Data Dictionary: " + frameDataDict["point"].GetType() + " " + frameDataDict["xaxis"].GetType() + " " + frameDataDict["yaxis"].GetType() + " "); //TODO: THIS RETURNS AN ARRAY TYPE

            if (frameDataDict["point"] is List<object> && frameDataDict["xaxis"] is List<object> && frameDataDict["yaxis"] is List<object>)
            {
                //Convert System.double items to float for use in instantiation
                List<object> pointslist = frameDataDict["point"] as List<object>;
                List<object> xaxislist = frameDataDict["xaxis"] as List<object>;
                List<object> yaxislist = frameDataDict["yaxis"] as List<object>;

                if (pointslist != null && xaxislist != null && yaxislist != null)
                {
                    frame.point = pointslist.Select(Convert.ToSingle).ToArray();
                    frame.xaxis = xaxislist.Select(Convert.ToSingle).ToArray();
                    frame.yaxis = yaxislist.Select(Convert.ToSingle).ToArray();
                }
                else
                {
                    Debug.LogError("One of the Frame lists is null");
                }
            }
            else if(frameDataDict["point"] is float[] && frameDataDict["xaxis"] is float[] && frameDataDict["yaxis"] is float[])
            {
                frame.point = (float[])frameDataDict["point"];
                frame.xaxis = (float[])frameDataDict["xaxis"];
                frame.yaxis = (float[])frameDataDict["yaxis"];
            }
            else if(frameDataDict["point"] is JArray && frameDataDict["xaxis"] is JArray && frameDataDict["yaxis"] is JArray)
            {
                JArray pointsArray = frameDataDict["point"] as JArray;
                JArray xaxisArray = frameDataDict["xaxis"] as JArray;
                JArray yaxisArray = frameDataDict["yaxis"] as JArray;

                // Convert JArrays to arrays of floats
                float[] points = pointsArray.Select(token => (float)token).ToArray();
                float[] xaxis = xaxisArray.Select(token => (float)token).ToArray();
                float[] yaxis = yaxisArray.Select(token => (float)token).ToArray();

                frame.point = points;
                frame.xaxis = xaxis;
                frame.yaxis = yaxis;
            }
            else
            {
                Debug.LogError("FrameParse: Frame Data is not a List, Array, or JArray.");
            }
            return frame;
        }

        public Dictionary<string, object> GetData()
        {
            return new Dictionary<string, object>
            {
                { "point", point },
                { "xaxis", xaxis },
                { "yaxis", yaxis }
            };
        }
    }

    /////////////// Classes For Building Plan Desearialization///////////////////
    
    [System.Serializable]
    public class BuildingPlanData
    {
        public string LastBuiltIndex { get; set; }
        public Dictionary<string, Step> steps { get; set; }
    }
    
    [System.Serializable]
    public class Step
    {
        public Data data { get; set; }
        public string dtype { get; set; }
        public string guid { get; set; }
    }

    [System.Serializable]
    public class Data
    {
        public string device_id { get; set; }
        public string[] element_ids { get; set; }
        public string actor { get; set; }
        public Frame location { get; set; }
        public string geometry { get; set; }
        public string[] instructions { get; set; }
        public bool is_built { get; set; }
        public bool is_planned { get; set; }
        public int[] elements_held { get; set; }
        public int priority { get; set; }
    }

    ////////////////Classes for User Current Informatoin/////////////////////
    
    [System.Serializable]
    public class UserCurrentInfo
    {
        public string currentStep { get; set; }
        public string timeStamp { get; set; }
        
    }
}