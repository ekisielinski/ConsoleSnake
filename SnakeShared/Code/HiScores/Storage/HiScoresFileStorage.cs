using Snake.Common.Helpers;
using Snake.Common.Logging;
using Snake.Game;
using System;
using System.IO;

namespace Snake.Shared
{
    public sealed class HiScoresFileStorage : IHiScoresStorage
    {
        readonly IHiScoresSerializer serializer;
        readonly ILogger logger;
        
        //====== ctors =====================================================================================================================

        public HiScoresFileStorage (string fileName, IHiScoresSerializer serializer, ILogger logger)
        {
            FileName        = Verify.NotNull (fileName, nameof (fileName));

            this.serializer = Verify.NotNull (serializer, nameof (serializer));
            this.logger     = Verify.NotNull (logger, nameof (logger));
        }

        //====== public properties =========================================================================================================

        public string FileName { get; }

        //====== IHiScoresStorage ==========================================================================================================

        public IHiScores Load ()
        {
            try
            {
                var data = File.ReadAllBytes (FileName);

                logger.Log ("Loading Hi Scores from: " + FileName, false);

                return serializer.Deserialize (data);
            }
            catch (Exception ex)
            {
                logger.Log (ex);

                return new HiScores ();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public void Save (IHiScores hiScores)
        {
            Verify.NotNull (hiScores, nameof (hiScores));

            try
            {
                var data = serializer.Serialize (hiScores);

                logger.Log ("Saving Hi Scores to: " + FileName, false);

                File.WriteAllBytes (FileName, data);
            }
            catch (Exception ex)
            {
                logger.Log (ex);
            }
        }
    }
}
