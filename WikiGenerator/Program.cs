using System;
using System.Collections.Generic;
using MI.ConsoleArguments;

namespace WikiGenerator
{
    class Program
    {
        private static string defaultTemplateFolder = "WikiTemplates";

        static void Main(string[] args)
        {

            WikiDetails wikiDetails = WikiDetails.Empty();
            if (args.Length > 1)
            {
                wikiDetails = ParseArgs(args);
            }

            if (string.IsNullOrEmpty(wikiDetails.BitBucketFolder))
                wikiDetails.BitBucketFolder = ConsoleArgumentPrompter.PromptUser("Please enter the bitbucket folder:");

            if (wikiDetails.PromptDescription)
                wikiDetails.ProjectDescription = ConsoleArgumentPrompter.PromptUser("Please enter the project description:");

            if (args.Length < 2)
            {
                wikiDetails.ProjectName = ConsoleArgumentPrompter.PromptUser("Please enter the project name:");
                wikiDetails.ProjectDescription = ConsoleArgumentPrompter.PromptUser("Please enter the project description:");

                wikiDetails.OutputFolder = ConsoleArgumentPrompter.PromptUser("Please enter the output folder:");
                wikiDetails.TemplateFolder = ConsoleArgumentPrompter.PromptUser("Please enter the template folder:");
            }

            if (string.IsNullOrEmpty(wikiDetails.TemplateFolder))
                wikiDetails.TemplateFolder = defaultTemplateFolder;
            //var wikiDetails = new WikiDetails(bitbucket, projectName, projectDescription);

            Console.WriteLine("Starting to compile your wiki...");

            var wikiCompiler = new WikiCompiler(wikiDetails);
            wikiCompiler.ExportWiki(wikiDetails.OutputFolder);
            ConsoleArgumentPrompter.PromptUser($"Wiki skeleton compiled.\nPlease see {wikiDetails.OutputFolder} for you wiki.\nPress any key to exit.");
        }

        private static WikiDetails ParseArgs(string[] args)
        {
            var argsList = new Dictionary<string, Action<string, WikiDetails>>();
            argsList.Add("-bb", (val, det) => { det.BitBucketFolder = val; });
            argsList.Add("-proj", (val, det) => { det.ProjectName = val; });
            argsList.Add("-desc", (val, det) =>
            {
                if (val == "?") { det.PromptDescription = true; }
                else
                {
                    det.PromptDescription = false;
                    det.ProjectDescription = val;
                }
            });

            argsList.Add("-out", (val, det) => { det.OutputFolder = val; });
            argsList.Add("-template", (val, det) => { det.TemplateFolder = val; });

            return ConsoleArgumentParser.ParseArgs(args, argsList);
        }

    }
}
