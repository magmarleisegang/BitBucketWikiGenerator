using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WikiGenerator
{
    public class WikiDetails
    {
        public WikiDetails(string bitbucketFolder, string projectName, string projectDescription)
        {
            BitBucketFolder = bitbucketFolder.Trim();
            ProjectName = projectName.Trim();
            ProjectDescription = projectDescription.Trim();
        }

        public WikiDetails() {
            PromptDescription = false;
        }

        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string BitBucketFolder { get; set; }
        public string OutputFolder { get; internal set; }
        public string TemplateFolder { get; internal set; }
        public bool PromptDescription { get; internal set; }

        internal static WikiDetails Empty()
        {
            return new WikiDetails();
        }        
    }
}
