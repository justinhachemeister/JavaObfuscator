using JavaObfuscator.Core.Logger;
using JavaObfuscator.Core.Utils;
using JavaResolver.Class;
using JavaResolver.Class.TypeSystem;
using System;
using System.IO;

namespace JavaObfuscator.Core
{
    public class Context
    {

        public string FilePath { get; private set; }
        public string OutputPath { get; private set; }

        public ConsoleLogger Logger { get; private set; }
        public StringGenerator StringGenerator { get; private set; }
        public Engine Engine { get; private set; }

        public MethodDefinition CurrentMethod { get; set; }
        public ClassDefinition Class { get; private set; }
        public JavaClassImage Image { get; private set; }

        public Context(string FilePath)
        {
            this.FilePath = FilePath;
            this.Engine = new Engine(this);
            this.StringGenerator = new StringGenerator();
            this.Image = JavaClassImage.FromFile(this.FilePath);
            this.Logger = new ConsoleLogger();
            this.Class = this.Image.RootClass;
            this.OutputPath = GetOutputPath;

        }


        public void Save()
        {
            Logger.Info("Trying to save the result");
            try
            {
                JavaClassFile classFile = Image.CreateClassFile();
                classFile.Write(this.OutputPath);
                Logger.Successed("File saved to: " + this.OutputPath);
            }
            catch(Exception ex)
            {
                Logger.Failed("Couldn't save the file for reasons: " + ex.Message);
            }
        }



        private string GetOutputPath
        {
            get
            {
                string basePath = Path.GetDirectoryName(this.FilePath);
                string newDirectoryName = Directory.CreateDirectory(Path.Combine(basePath, "JavaObfuscator\\")).FullName;
                return Path.Combine(newDirectoryName + Path.GetFileName(this.FilePath));
            }
        }

    }
}
