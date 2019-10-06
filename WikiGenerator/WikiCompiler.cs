using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WikiGenerator
{
    public class WikiCompiler
    {
        private readonly WikiDetails wikiDetails;
        private readonly string wikiTemplateFolder;

        public WikiCompiler(WikiDetails wikiDetails)
        {
            this.wikiDetails = wikiDetails;
            this.wikiTemplateFolder = wikiDetails.TemplateFolder;
        }

        public void ExportWiki(string outputFolder)
        {
            if (string.IsNullOrEmpty(outputFolder))
                outputFolder = Environment.CurrentDirectory;

            if (Directory.Exists(outputFolder) == false)
            {
                Directory.CreateDirectory(outputFolder);
            }

            var filesList = Directory.EnumerateFiles(wikiTemplateFolder);
            foreach (var file in filesList)
            {
                string filename = Path.GetFileName(file);
                string content = File.ReadAllText(file);
                string compiledContent = content.Replace("{{ProjectName}}", wikiDetails.ProjectName)
                    .Replace("{{ProjectDescription}}", wikiDetails.ProjectDescription)
                    .Replace("{{BitBucketFolder}}", wikiDetails.BitBucketFolder);

                var newFilePath = Path.Combine(outputFolder, filename);
                if(File.Exists(newFilePath))
                {
                    var renamed_originalFilePath = Path.Combine(outputFolder, Path.ChangeExtension(filename, "orig.md"));

                    File.Copy(newFilePath, renamed_originalFilePath);
                }
                File.WriteAllText(newFilePath, compiledContent);
            }
        }
    }
}
