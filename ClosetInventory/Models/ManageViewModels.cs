﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Nager.AmazonProductAdvertising.Model;

namespace ClosetInventory.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }
    
    public class UploadViewModel
    {
        
        public string SmallFile { get; set; }

        public string LargeFile { get; set; }

        public bool IsFavorite { get; set; }

        public int? DressinessRating { get; set; }

        public int? WarmthRating { get; set; }

        public string Color { get; set; }

        public string ColorType { get; set; }//dark, bright, neutral

        public bool IsTightFit { get; set; }

        public string Type { get; set; }

        public bool isLong { get; set; }

        public bool isCapri { get; set; }

        public bool IsHighWaist { get; set; }

        public bool IsSkinny { get; set; }

        public string SleeveLength { get; set; }

        public bool IsCropped { get; set; }

        public string ControllerName { get; set; }

        public List<string> Descriptions { get; set; }

        public ApplicationUser User { get; set; }
    }

    public class TotalViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Cover> Covers { get; set; }
        public List<Dress> Dresses { get; set; }
        public List<Skirt> Skirts { get; set; }
        public List<Shirt> Shirts { get; set; }
        public List<Pants> Pants { get; set; }
        public List<Shoe> Shoes { get; set; }        
        public Outfit Outfit { get; set; }
        public string Dressiness { get; set; }

    }

    public class AmazonSearchModel
    {

        public string KeywordSearch { get; set; }
        public int IndexSearch { get; set; }
        public AmazonItemResponse Response { get; set; }
        public Item Item { get; set; }
    }
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}