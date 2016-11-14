using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using ClosetInventory.Models;

namespace ClosetInventory.WorkerClasses
{
    public class ImageManager
    {

        public System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
           
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }



        public FinalImageResult HandleIncomingImage(string fileName)
        {
            FinalImageResult final = new FinalImageResult();
            // find the files and put into an array of files

            //1.specifying file types 
            //var ext = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".png", ".jpg", ".gif" };
            ////2.specifying file folder to look in
            //var dir = @"C:\Users\berig\OneDrive\Documents\Visual Studio 2015\Projects\GoogleTrends\GoogleTrends\Images";
            ////3. find all files and put in an array
            //var files =
            //    Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
            //        .Where(f => ext.Contains(Path.GetExtension(f)))
            //        .Select(f => new FileInfo(f))
            //        .ToArray();

            FileInfo file = new FileInfo(fileName);
            //create service\
            var credentials = VisionAPIManager.CreateCredentials("C:\\Users\\berig\\OneDrive\\DevCodeCamp Projects\\ClosetInventory\\ClosetInventory\\Closet Inventory-1aad30b533e0.json");
            var service = VisionAPIManager.CreateService("MyApplication", credentials);

            //process each file
            //foreach (var file in files)
            //{
            //string f = file.FullName;
            //Console.WriteLine("Reading " + f + ":");

            var imageResult = service.AnnotateAsync(file, "LABEL_DETECTION", "IMAGE_PROPERTIES", "FACE_DETECTION");

            final = SplitImageResults(imageResult);
            ;
            //return typeSuggestion;
            //f += ".keywords.txt";
            //File.WriteAllText(f, words);
            //}

            return final;
        }


        public FinalImageResult SplitImageResults(Task<AnnotateImageResponse> imageResult)
        {
            FinalImageResult final = new FinalImageResult();

            var lResults = imageResult.Result.LabelAnnotations;
            var iResults = imageResult.Result.ImagePropertiesAnnotation;
            //var fResults = imageResult.Result.FaceAnnotations;

            ResultsHandler handler = new ResultsHandler();
            final.ImageLabels = handler.HandleLabelResults(lResults);
            //final.FaceResults = handler.handleFaceResults(fResults);
            final.ImageProps = handler.HandlePropertiesResults(iResults);

            return final;

        }
    }
        public class FinalImageResult
        {
            //public FaceResults FaceResults { get; set; }
            public ImageLabels ImageLabels { get; set; }
            public ImageProps ImageProps { get; set; }

        }   
}

    public class LabelResult
    {
        public string Description { get; set; }
        public float? Score { get; set; }
        public List<Vertices> FacePoints { get; set; }
        public List<Vertices> HeadPoints { get; set; }
    }
    public class Vertices
    {
        public List<Point> FourPoints { get; set; }
    }

    public class Point
    {
        public float? XCoord { get; set; }
        public float? YCoord { get; set; }
    }
    public class Position
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }