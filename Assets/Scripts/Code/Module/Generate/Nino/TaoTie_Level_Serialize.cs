/* this is generated by nino */
namespace TaoTie
{
    public partial class Level
    {
        public static Level.SerializationHelper NinoSerializationHelper = new Level.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<Level>
        {
            #region NINO_CODEGEN
            public override void Serialize(Level value, Nino.Serialization.Writer writer)
            {
                if(value == null)
                {
                    writer.Write(false);
                    return;
                }
                writer.Write(true);
                writer.CompressAndWrite(value.Id);
                writer.CompressAndWrite(value.Layer);
            }

            public override Level Deserialize(Nino.Serialization.Reader reader)
            {
                if(!reader.ReadBool())
                    return null;
                Level value = new Level();
                value.Id = reader.DecompressAndReadNumber<System.Int32>();
                value.Layer = reader.DecompressAndReadNumber<System.Int32>();
                return value;
            }
            #endregion
        }
    }
}