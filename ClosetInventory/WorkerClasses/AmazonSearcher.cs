using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosetInventory.WorkerClasses
{
    public class AmazonSearcher
    {
        public AmazonItemResponse GetAmazonResults(string keywords, int index)
        {
            var authentication = new AmazonAuthentication();
            authentication.AccessKey = "AKIAJ3HAD7WFOAKEB4MQ";
            authentication.SecretKey = "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";
            var num = (AmazonSearchIndex)index;

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.US);
            var result = wrapper.Search(keywords, num, AmazonResponseGroup.Large);

            return result;
        }
    }
}