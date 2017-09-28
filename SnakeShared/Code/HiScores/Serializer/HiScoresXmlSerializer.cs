using Snake.Common.Helpers;
using Snake.Game;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Snake.Shared
{
    public class HiScoresXmlSerializer : IHiScoresSerializer
    {
        public IHiScores FromXml (string xml)
        {
            var serializer = new DataContractSerializer (typeof (__HiScores));

            var stream = new MemoryStream (Encoding.UTF8.GetBytes (xml));
           
            var result = (__HiScores) serializer.ReadObject (stream);

            return result.ToHiScoresList ();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public string ToXml (IHiScores hiScores)
        {
            Verify.NotNull (hiScores, nameof (hiScores));

            var serializer = new DataContractSerializer (typeof (__HiScores));

            var stream = new MemoryStream ();
           
            var __hiScores = __HiScores.FromHiScores (hiScores);

            serializer.WriteObject (stream, __hiScores);

            stream.Position = 0;

            var streamReader = new StreamReader (stream, Encoding.UTF8);

            return streamReader.ReadToEnd ();
        }

        //====== IHiScoresSerializer =======================================================================================================

        public IHiScores Deserialize (byte[] data) => FromXml (Encoding.UTF8.GetString (data));

        public byte[] Serialize (IHiScores hiScores) => Encoding.UTF8.GetBytes (ToXml (hiScores));

        //====== helper classes ============================================================================================================

        [DataContract (Name = "HiScores")]

        private sealed class __HiScores
        {
            [DataMember (Name = "Players")] public List<__HiScoresEntry> List     { get; set; }
            [DataMember]                 public int                   Capacity { get; set; }

            //====== public methods ========================================================================================================

            public HiScores ToHiScoresList ()
            {
                var result = new HiScores (Capacity);

                List.ForEach (x => result.TryAdd (x.ToHiScoresEntry ()));

                return result;
            }

            //------------------------------------------------------------------------------------------------------------------------------

            public static __HiScores FromHiScores (IHiScores hiScoresList)
            {
                Verify.NotNull (hiScoresList, nameof (hiScoresList));

                List<__HiScoresEntry> entries = hiScoresList.GetList ().Select (x => __HiScoresEntry.FromHiScoresEntry (x)).ToList ();

                return new __HiScores
                {
                    Capacity = hiScoresList.Capacity,
                    List     = entries
                };
            }
        }

        //==================================================================================================================================

        [DataContract (Name = "Player")]

        private sealed class __HiScoresEntry
        {
            [DataMember] public string Name  { get; set; }
            [DataMember] public int    Score { get; set; }

            //====== public methods ========================================================================================================

            public HiScoresEntry ToHiScoresEntry ()
            {
                return new HiScoresEntry (Name, Score);
            }

            //------------------------------------------------------------------------------------------------------------------------------

            public static __HiScoresEntry FromHiScoresEntry (HiScoresEntry entry)
            {
                return new __HiScoresEntry { Name = entry.Name, Score = entry.Score };
            }
        }
    }
}
