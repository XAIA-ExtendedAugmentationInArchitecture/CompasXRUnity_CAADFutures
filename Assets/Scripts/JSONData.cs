using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

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