namespace SnakeGame.Properties
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute ("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute ()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute ()]

    internal class Resources
    {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;

        //----------------------------------------------------------------------------------------------------------------------------------

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute ("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]

        internal Resources () { }

        //----------------------------------------------------------------------------------------------------------------------------------

        [global::System.ComponentModel.EditorBrowsableAttribute (global::System.ComponentModel.EditorBrowsableState.Advanced)]

        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals (resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SnakeGame.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }

                return resourceMan;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [global::System.ComponentModel.EditorBrowsableAttribute (global::System.ComponentModel.EditorBrowsableState.Advanced)]

        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }

            set
            {
                resourceCulture = value;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        internal static string SnakeGameVersion
        {
            get
            {
                return ResourceManager.GetString ("SnakeGameVersion", resourceCulture);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        internal static string TextCredits
        {
            get
            {
                return ResourceManager.GetString ("TextCredits", resourceCulture);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        internal static string TextDigits
        {
            get
            {
                return ResourceManager.GetString ("TextDigits", resourceCulture);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        internal static string TextHiScores
        {
            get
            {
                return ResourceManager.GetString ("TextHiScores", resourceCulture);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        internal static string TextLogo
        {
            get
            {
                return ResourceManager.GetString ("TextLogo", resourceCulture);
            }
        }
    }
}
