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
    public class Range
    {

        public int Start { get; set; }
        public int End { get; set; }
        public string ColorGrouping { get; set; }

    }
    //account for brown
    public class ColorCalculator
    {
        //if S and V are 100
        List<Range> hueRanges = new List<Range> {
            new Range { Start = 325, End = 360, ColorGrouping = "red" },
            new Range { Start = 290, End = 325, ColorGrouping = "pink" },
            new Range { Start = 255, End = 290, ColorGrouping = "purple" },
            new Range { Start = 220, End = 255, ColorGrouping = "blue" },
            new Range { Start = 175, End = 320, ColorGrouping = "cyan" },
            new Range { Start = 75, End = 175, ColorGrouping = "green" },
            new Range { Start = 35, End = 75, ColorGrouping = "yellow" },
            new Range { Start = 5, End = 35, ColorGrouping = "orange" },
            new Range { Start = 0, End = 5, ColorGrouping = "red" }
        };

        //need something to account for nulls
        public string FindHueRange(double? hueToCheck)
        {
            string hueGroup = "";

            for (int i = 0; i < hueRanges.Count; i++)
            {
                if (hueToCheck >= hueRanges[i].Start && hueToCheck < hueRanges[i].End)
                {
                    hueGroup = hueRanges[i].ColorGrouping;
                    break;
                }
            }
            return hueGroup;
        }
        public bool CheckIfBrown(ColorHSV hsv)
        {
            return (hsv.Value >= 20 && hsv.Value <= 70 && hsv.Saturation > 20);
        }
        public string DetermineSaturationValue(string hueRange, ColorHSV hsv)
        {
            string color = "";
            double? value = hsv.Value;
            double? saturation = hsv.Saturation;
            if (value > 75 && saturation < 70 && saturation > 15)
            {
                color = "light" + hueRange;
            }
            else if (value <= 60 && value >= 30 && saturation > 20)
            {
                color = "dark" + hueRange;
            }
            else if (value > 60 && saturation >= 70)
            {
                color = hueRange;
            }
            else
            {
                //check if all work out
            }

            return color;
        }
        public string FindColor(ColorHSV hsv)
        {
            string color = "";
            string hueRange;
            double? value = hsv.Value;
            double? hue = hsv.Hue;
            double? saturation = hsv.Saturation;

            if (value != null && hue != null && saturation != null)
            {
                if (value < 30)
                {
                    color = "black";
                }
                else
                {
                    hueRange = FindHueRange(hue);

                    if (value > 80 && saturation < 15)
                    {
                        color = "white";
                    }
                    else if (value <= 80 && value >= 30 && saturation <= 20)
                    {
                        hueRange = "grey";
                        color = DetermineSaturationValue(hueRange, hsv);
                    }
                    else
                    {
                        if (hueRange == "orange")
                        {
                            if (CheckIfBrown(hsv))
                            {
                                hueRange = "brown";
                            }
                        }

                        color = DetermineSaturationValue(hueRange, hsv);
                    }
                }

            }

            return color;
        }
    }
}
