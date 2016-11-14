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
        AccessKeyHolder aKH = new AccessKeyHolder();
        public AmazonItemResponse GetAmazonResults(string keywords, int index)
        {
            var authentication = new AmazonAuthentication();
            authentication.AccessKey = aKH.GetAccessKey();
            authentication.SecretKey = aKH.GetSecretKey();
            var num = (AmazonSearchIndex)index;

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.US);
            var result = wrapper.Search(keywords, num, AmazonResponseGroup.Large);

            return result;
        }
    }
}