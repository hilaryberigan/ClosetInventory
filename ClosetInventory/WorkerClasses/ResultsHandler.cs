using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ClosetInventory.WorkerClasses;

namespace ClosetInventory.WorkerClasses
{
    public class ResultsHandler
    {

        CategoryGuesser categoryGuesser = new CategoryGuesser();

        string[] badLabels =
        {
           "clothing",
            "color",
            "human body",
            "yellow",
            "green",
            "blue",
            "purple",
            "orange",
            "red",
            "abdomen"
        };
        //LABEL RESULTS
        public ImageLabels HandleLabelResults(IList<EntityAnnotation> results)
        {
            ImageLabels imageLabels = new ImageLabels();

            imageLabels.Labels = UpdateDescription(results);
            imageLabels.ControllerSuggestion = categoryGuesser.DetermineController(imageLabels.Labels);

            return imageLabels;
        }
        public IList<EntityAnnotation> UpdateDescription(IList<EntityAnnotation> lResults)
        {

            lResults.OrderByDescending(i => i.Score);

            for (int i = 0; i < lResults.Count; i++)
            {
                if (badLabels.Contains(lResults[i].Description))
                {
                    lResults.RemoveAt(i);
                }
            }
            return lResults;
        }



        //FACE RESULTS
        public FaceResults HandleFaceResults(IList<FaceAnnotation> results)
        {
            FaceResults faceResults = new FaceResults();
            return faceResults;

        }
        //public bool faceIsCentered(IList<FaceAnnotation> results)
        //{
        //   results[0].    
        //}

        //PROPERTIES RESULTS
        public ImageProps HandlePropertiesResults(ImageProperties results)
        {
            //need null value handling ****

            List<ColorProps> colorList = new List<ColorProps>();

            for (int i = 0; i < results.DominantColors.Colors.Count; i++)
            {
                ColorInfo currentColor = results.DominantColors.Colors[i];
                ColorCalculator colorCalculator = new ColorCalculator();
                ColorRGB RGB = new ColorRGB
                {
                    R = Convert.ToInt32(currentColor.Color.Red),
                    G = Convert.ToInt32(currentColor.Color.Green),
                    B = Convert.ToInt32(currentColor.Color.Blue),
                };
              
                System.Drawing.Color colorToConvert = System.Drawing.Color.FromArgb(RGB.R, RGB.G, RGB.B);
                ColorHSV hsv = ConvertRGBToHSV(colorToConvert);
                string color = colorCalculator.FindColor(hsv);

                ColorProps colorProps = new ColorProps { HSV = hsv, RGB = RGB, percentOfImage = currentColor.PixelFraction, colorName = color };
                colorList.Add(colorProps);
            }

            ImageProps imageProps = new ImageProps();
            imageProps.dominantColors = colorList;
            return imageProps;
        }

        public ColorHSV ConvertRGBToHSV(System.Drawing.Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            double hue = color.GetHue();
            double saturation = ((max == 0) ? 0 : 1d - (1d * min / max)) * 100 ;
            double value =( max / 255d ) * 100;

            return new ColorHSV { Hue = hue, Saturation = saturation, Value = value };
           
        }
    }


//Results Objects
            public class ImageProps
            {
                public List<ColorProps> dominantColors { get; set; }
            }
            public class ColorProps
            {
               public ColorRGB RGB { get; set; }
               public ColorHSV HSV { get; set; }
               public float? percentOfImage { get; set; }
               
               public string colorName { get; set; }

            }
            public class ColorRGB
            {
                public int R { get; set; }
                public int G { get; set; }
                public int B { get; set; }
            }
            
            public class ColorHSV
            {
                public double? Hue { get; set; }
                public double? Saturation { get; set; }
                public double? Value { get; set; }
            }

            public class ImageLabels
            {
               public string ControllerSuggestion { get; set; }

                public IList<EntityAnnotation> Labels { get; set; }
            }


            public class FaceResults
            {
                 
            }
}