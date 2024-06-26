/* this is generated by nino */
namespace TaoTie
{
    public partial class LevelCategory
    {
        public static LevelCategory.SerializationHelper NinoSerializationHelper = new LevelCategory.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<LevelCategory>
        {
            #region NINO_CODEGEN
            public override void Serialize(LevelCategory value, Nino.Serialization.Writer writer)
            {
                if(value == null)
                {
                    writer.Write(false);
                    return;
                }
                writer.Write(true);
                if(value.list != null)
                {
                    writer.Write(true);
                    writer.CompressAndWrite(value.list.Count);
                    foreach (var entry in value.list)
                    {
                        TaoTie.Level.NinoSerializationHelper.Serialize(entry, writer);
                    }
                }
                else
                {
                    writer.Write(false);
                }
            }

            public override LevelCategory Deserialize(Nino.Serialization.Reader reader)
            {
                if(!reader.ReadBool())
                    return null;
                LevelCategory value = new LevelCategory();
                if(reader.ReadBool()){value.list = new System.Collections.Generic.List<TaoTie.Level>(reader.ReadLength());
                for(int i = 0, cnt = value.list.Capacity; i < cnt; i++)
                {
                    var value_list_i = TaoTie.Level.NinoSerializationHelper.Deserialize(reader);
                    value.list.Add(value_list_i);
                }}
                return value;
            }
            #endregion
        }
    }
}